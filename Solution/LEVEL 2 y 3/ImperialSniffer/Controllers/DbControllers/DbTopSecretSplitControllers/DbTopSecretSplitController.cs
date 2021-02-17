using ImperialSniffer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialSniffer.Controllers
{
    /// <summary>
    /// This class is created to manage the "Data Base" of the TopSecret_Split controller.
    /// For simplisity it was done with the Entity Framework Core from .Net, but it could be a complete SQL Database with more powerfull tools.
    /// </summary>
    public class DbTopSecretSplitController : IdbController
    {
        //this will be used as a splitter for the message string, to be saved in the DB Item. I know it could be possible to receive this character in the satellite message.
        string _wordsSeparator = "|"; 
        
        //TODO:This maximum time between received messages from satellites should be configurable!!
        int _MaxTimeoutReceivedMessageInSeconds = 15; //It is the max period of time where all the message from satellites should be received.
         
        //Receive the Entity Context. 
        private TopSecretSplitContext _context;
        public DbTopSecretSplitController(TopSecretSplitContext context)
        {
            _context = (TopSecretSplitContext)context;  //Receive here the database Context to be manage in this class.
        }

        /// <summary>
        /// This Method is going to include the new information from the specific satellite in the db.
        /// I know it should be also possible to know (and save) which ship has this message been arrived from, then you can have more than one Ship sending.
        /// </summary>
        /// <param name="satelliteData"></param>
        public override async void SaveNewSatelliteData(SatelliteData satelliteData)
        {
            TopSecretSplitItem topSecretSplitItem = CreateDbItemWithSatelliteData(satelliteData);

            var topSecretSplitItemRequest = await _context.TopSecretSplitItems.FindAsync(topSecretSplitItem.id);

            if (topSecretSplitItemRequest != null)
            {
                _context.TopSecretSplitItems.Remove(topSecretSplitItemRequest);
            }
            topSecretSplitItem.createdDate = DateTime.Now;
            _context.TopSecretSplitItems.Add(topSecretSplitItem);

            await _context.SaveChangesAsync();
        }

        public TopSecretSplitItem CreateDbItemWithSatelliteData(SatelliteData satelliteData)
        {
            TopSecretSplitItem topSecretSplitItem = new TopSecretSplitItem();
            topSecretSplitItem.id = (_rebelSatellites.GetIndexOfSatellite(satelliteData.name) + 1); //Add 1 to this id value to be sure it never reach de 0 value.;
            topSecretSplitItem.name = satelliteData.name;
            topSecretSplitItem.distance = satelliteData.distance;

            //Merge the 
            var result = String.Join(_wordsSeparator, satelliteData.message.ToArray());
            topSecretSplitItem.message = result;

            return topSecretSplitItem;
        }

        public override async Task<AllSatellitesData> GetListOfValidatedSatellitesData()
        {
            AllSatellitesData allSatellitesData = new AllSatellitesData();

            //Here I should ask for the information received from every satellite which has actually received a message
            List<TopSecretSplitItem> topSecretSplitItems = _context.TopSecretSplitItems.ToList(); //At this moment I get all the table because I have only one Imperial Ship to track. But it could be asked as a Query with some particular request (i.e: name of imperial Ship).

            //Here I should validate Date and Time from the last 3 messages.
            ValidateSatellitesData(topSecretSplitItems);

            foreach (TopSecretSplitItem item in topSecretSplitItems)
            {
                //Split the message string that came from the DB message, it was merge in one single string in order to be saved in one register.
                List<string> wordsOfMessage = new List<string>(item.message.Split(_wordsSeparator));

                SatelliteData satellitData = new SatelliteData(item.name, item.distance, wordsOfMessage);
                allSatellitesData.satellites.Add(satellitData);
            }

            return allSatellitesData;
        }

        /// <summary>
        /// This method validates some particular conditions of the informations received.
        /// At first information received must not overpass a valid period of time between them, remember they come from a Ship in movement.
        /// </summary>
        /// <param name="topSecretSplitItems"></param>
        void ValidateSatellitesData(List<TopSecretSplitItem> topSecretSplitItems)
        {
            //It validates the minimun number of needed satellites information
            if (topSecretSplitItems.Count != _rebelSatellites.GetRebelSatellitesMinimumNumber())
            {
                throw new Exception("Not enough satellites information");
            }

            //Check if the difference between satellites message does not exceed the received message timeout; 
            //TODO:This validation could be also done by the Data base query request, it is required to check the latency time between signals from the Ship!!
            DateTime biggest = topSecretSplitItems.Max(r => r.createdDate);
            DateTime now = DateTime.Now;
            foreach(TopSecretSplitItem item in topSecretSplitItems)
            {
                if(DateTime.Compare(item.createdDate.AddSeconds(_MaxTimeoutReceivedMessageInSeconds), now) < 0)
                {
                    throw new Exception("The timeout between received message was exceeded.");
                }
            }
        }

        public override async Task<SatelliteData> GetSatelliteDataFromName(string name)
        {
            TopSecretSplitItem topSecretSplitItem = new TopSecretSplitItem();
            topSecretSplitItem.id = (_rebelSatellites.GetIndexOfSatellite(name) + 1); //Add 1 to this id value to be sure it never reach de 0 value.;
            var topSecretSplitItemRequest = await _context.TopSecretSplitItems.FindAsync(topSecretSplitItem.id);

            if (topSecretSplitItemRequest == null)
            {
                throw new Exception("There is not information for satellite" + name);
            }
            List<string> wordsSeparated = new List<string>(topSecretSplitItemRequest.message.Split(_wordsSeparator));

            SatelliteData satelliteData = new SatelliteData(topSecretSplitItemRequest.name, topSecretSplitItemRequest.distance, wordsSeparated);
            return satelliteData;
        }

    }
}
