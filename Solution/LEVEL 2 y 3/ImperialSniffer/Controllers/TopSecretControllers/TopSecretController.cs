using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ImperialSniffer.Models;

namespace ImperialSniffer.Controllers
{
    [Route("imperialsniffer/topsecret")]
    [ApiController]
    public class TopSecretController : ControllerBase
    {
        public ImperialSnifferMainClass imperialSniffer = new ImperialSnifferMainClass();
        [HttpGet]
        public string GetTopSecret()
        {
            return "GET is not allowed in topsecret";
        }

        [HttpPost]
        public ActionResult<string> PostTopSecret(dynamic data)
        {
            try
            {
                //Request and Response data definitions
                string requestData = data.ToString();
                string responseData = "";
                AllSatellitesData satellitesInformation = JsonConvert.DeserializeObject<AllSatellitesData>(requestData);
                TopSecretResponseData topSecretResponsetData = imperialSniffer.GetImperialShipInformation(satellitesInformation);

                responseData = JsonConvert.SerializeObject(topSecretResponsetData);
                return responseData;
            }
            catch (Exception ex)
            {
                //TODO: Log exception Message.
                return NotFound();
            }

        }
    }
}