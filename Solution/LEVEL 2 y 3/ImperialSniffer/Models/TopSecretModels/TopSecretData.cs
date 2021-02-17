using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialSniffer.Models
{
    [Serializable]
    public class TopSecretRequestData
    {
        public List<SatelliteData> satellites = new List<SatelliteData>();
    }

    [Serializable]
    public class TopSecretResponseData
    {
        public Position position = new Position();
        public string message;
    }

    public class Position
    {
        public float x;
        public float y;
    }

    public class Message
    {
        public List<string> wordsOfMessage = new List<string>();
    }
}
