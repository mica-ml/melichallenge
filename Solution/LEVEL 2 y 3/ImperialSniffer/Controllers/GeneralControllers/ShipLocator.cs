using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImperialSniffer.Models;

namespace ImperialSniffer
{

    public class XYCoordinates
    {
        public float xCoord;
        public float yCoord;
        public XYCoordinates()
        {
        }
        public XYCoordinates(float xCoord, float yCoord)
        {
            this.xCoord = xCoord;
            this.yCoord = yCoord;
        }
        public float GetCoordinateX()
        {
            return xCoord;

        }
        public float GetCoordinateY()
        {
            return yCoord;
        }
    }

    public class ShipLocator
    {
        RebelSatellites _rebelSatellites = new RebelSatellites();

        /// <summary>
        /// This methods was taken from the exercice level 1 GetLocation. And use a BaseTool method named TrilaterationSolver. 
        /// TODO: search for a best Trilateration Algorithm. 
        /// </summary>
        /// <param name="allSatellites"></param>
        /// <returns></returns>
        public XYCoordinates GetLocation(AllSatellitesData allSatellites)  //I took this function from exercice Level 1 but I extend it passing more information in its parameter.
        {


            //At the moment it is only calling this BaseTools methods, but it could be an interface to a generic calls with diferents implementations and parameters.
            XYCoordinates xYCoordinates = BaseTools.TrilaterationSolver(_rebelSatellites.rebelSatellitesPositionList[allSatellites.satellites[0].name],
                                                 allSatellites.satellites[0].distance,
                                                 _rebelSatellites.rebelSatellitesPositionList[allSatellites.satellites[1].name],
                                                 allSatellites.satellites[1].distance,
                                                 _rebelSatellites.rebelSatellitesPositionList[allSatellites.satellites[2].name],
                                                 allSatellites.satellites[2].distance);

            //reformat GetLocation asnwer to match exercice reuirements. Just 1 decimal number.
            xYCoordinates.xCoord = (float)Math.Round(xYCoordinates.GetCoordinateX(), 1);
            xYCoordinates.yCoord = (float)Math.Round(xYCoordinates.GetCoordinateY(), 1);
            return xYCoordinates;
        }


        /// <summary>
        /// Take the list of distances from every satellite distance attribute given in the request Data
        /// </summary>
        /// <param name="topSecretRequestData"></param>
        /// <returns></returns>
        public List<float> GetDistancesListFromSatellitesData(AllSatellitesData topSecretRequestData)
        {
            List<float> distancesFromSatellites = new List<float>();

            if (topSecretRequestData.satellites.Count != _rebelSatellites.rebelSatellitesPositionList.Count)
            {
                throw new Exception("Error, the number of satellites must be at least " + _rebelSatellites.rebelSatellitesPositionList.Count);
            }

            foreach (SatelliteData satelliteData in topSecretRequestData.satellites)
            {
                distancesFromSatellites.Add(satelliteData.distance);
            }

            return distancesFromSatellites;
        }

    }
}
