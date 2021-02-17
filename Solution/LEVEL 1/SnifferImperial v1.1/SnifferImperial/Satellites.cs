using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialSnifferSolution
{
    public class RebelSatellites
    {
        public Dictionary<string, XYCoordinates> rebelSatellitesPositionList = new Dictionary<string, XYCoordinates>();
        public RebelSatellites()
        {
            rebelSatellitesPositionList.Add("kenobi", new XYCoordinates(-500, -200));
            rebelSatellitesPositionList.Add("skywalker", new XYCoordinates(100, -100));
            rebelSatellitesPositionList.Add("sato", new XYCoordinates(500, 100));
        }
        public class RebelSatellitesNames
        {
            public static string Kenobi = "kenobi";
            public static string Skywalker = "skywalker";
            public static string Sato = "sato";
        }
        public class RebelSatellitesLocations
        {
            public static XYCoordinates Kenobi = new XYCoordinates(-500, -200);
            public static XYCoordinates Skywalker = new XYCoordinates(100, -100);
            public static XYCoordinates Sato = new XYCoordinates(500, 100);
        }

        public int GetRebelSatellitesMinimumNumber()
        {
            return 3;  //To calculate a position in a 2D at least 3 points should be known.
        }

        public long GetIndexOfSatellite(string name)
        {
            return (long)Array.IndexOf(rebelSatellitesPositionList.Keys.ToArray(), name);
        }
    }

    [Serializable]
    public class SatelliteData
    {
        public SatelliteData(string pName, float pDistance, List<string> pMessage)
        {
            name = pName;
            distance = pDistance;
            message = pMessage;
        }

        public SatelliteData()
        {

        }

        public string name;
        public float distance;
        public List<string> message = new List<string>();
    }

    public class AllSatellitesData
    {
        public List<SatelliteData> satellites = new List<SatelliteData>();
    }
}
