using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImperialSniffer.Models;
using Newtonsoft.Json;

namespace ImperialSniffer.Controllers.TopSecret_SplitControllers
{
    [Route("imperialsniffer/topsecret_split")]
    [ApiController]
    public class TopSecretSplitController : ControllerBase
    {
        FileLogger fileLogger = new FileLogger();
        private IdbController _dbTopSecretSplitController;
        private ImperialSnifferMainClass _ImperialSnifferMainClass = new ImperialSnifferMainClass();

        public TopSecretSplitController(TopSecretSplitContext context)
        {
            //TODO: here I can implement a dynamic interface creator which would depend on the type of db and dbContext.
            _dbTopSecretSplitController = new DbTopSecretSplitController(context);
        }

        /// <summary>
        /// This GET action call the get Location and get Message method and responds as was requested en 
        /// exercice level 3 from meli test.
        /// </summary>
        /// <returns></returns>
        // GET: api/topsecret_split
        [HttpGet]
        public ActionResult<string> GetTopSecretSplitAll()
        {
            string resVal = "";
            try
            {
                Task<AllSatellitesData> allSatellitesData = _dbTopSecretSplitController.GetListOfValidatedSatellitesData();
                TopSecretResponseData topSecretResponseData = _ImperialSnifferMainClass.GetImperialShipInformation(allSatellitesData.Result);

                resVal = JsonConvert.SerializeObject(topSecretResponseData);
                return resVal;
            }
            catch (Exception ex)
            {
                //fileLogger.Log(ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// This Method is not in the required exercice Level 3 list.
        /// It is used just to test the db information.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public ActionResult<string> GetTopSecretSplit(string name)
        {
            try
            {
                //This save methos is an interface, it does not depend directly on the database implementation.
                Task<SatelliteData> satelliteData = _dbTopSecretSplitController.GetSatelliteDataFromName(name);
                string resVal = JsonConvert.SerializeObject(satelliteData.Result);
                return resVal;
            }
            catch (Exception ex)
            {
                //fileLogger.Log(ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Receive every satellite information and save it in a permanent memory (can be local data base, file, etc).
        /// </summary>
        /// <param name="name"></param>
        /// <param name="topSecretSplitRequest"></param>
        /// <returns></returns>
        // POST: api/topsecret_split/{name}
        [HttpPost("{name}")]
        public string PostTopSecretSplit(string name, dynamic topSecretSplitRequest)
        {

            try
            {
                TopSecretSplitRequestData topSecretSplitRequestData = JsonConvert.DeserializeObject<TopSecretSplitRequestData>(topSecretSplitRequest.ToString());

                //Convert POST input information to the previously defined(in exercice Level 2 "topsecret") SatelliteData class
                SatelliteData satelliteData = new SatelliteData(name, topSecretSplitRequestData.distance, topSecretSplitRequestData.message);

                //This save methos is an interface, it does not depend directly on the database implementation.
                _dbTopSecretSplitController.SaveNewSatelliteData(satelliteData);
               
                string resVal = "Data received OK from satellite " + name;
                return resVal;
            }
            catch (Exception ex)
            {
                //fileLogger.Log(ex.Message);
                return ex.Message;
            }
        }
    }
}
