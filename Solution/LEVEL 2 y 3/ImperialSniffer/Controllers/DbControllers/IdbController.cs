using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialSniffer.Controllers
{



    public class IdbController
    {
        //This Rebels Satellites class contains the list of available satellites in this program, it is currently hard code, but it should be also dynamic. 
        protected RebelSatellites _rebelSatellites = new RebelSatellites();

        //public IdbController(Object context)
        //{
        //}

        /// <summary>
        /// This Method is going to include the new information from the specific satellite in the db.
        /// I know it should be also possible to know (and save) which ship has this message been arrived from, then you can have more than one Ship sending.
        /// </summary>
        /// <param name="satelliteData"></param>
        public virtual async void SaveNewSatelliteData(SatelliteData satelliteData)
        {
        }

        public virtual Object CreateDbItemWithSatelliteData(SatelliteData satelliteData)
        {
            Object genericVal = new Object();
            return genericVal;
        }

        public virtual async Task<AllSatellitesData> GetListOfValidatedSatellitesData()
        {
            AllSatellitesData allSatellitesData = new AllSatellitesData();
            return allSatellitesData;
        }

        public virtual async Task<SatelliteData> GetSatelliteDataFromName(string name)
        {

            SatelliteData satelliteData = new SatelliteData();
            return satelliteData;
        }

    }
}
