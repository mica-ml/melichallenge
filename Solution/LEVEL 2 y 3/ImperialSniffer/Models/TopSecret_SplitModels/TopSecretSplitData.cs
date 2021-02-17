using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialSniffer.Models
{
    [Serializable]
    public class TopSecretSplitRequestData
    {
        public float distance;
        public List<string> message = new List<string>();
    }

    [Serializable]
    public class TopSecretSplitResponsetData
    {
        public Position position = new Position();
        public string message;
    }

    /// <summary>
    /// Item used for the data base.
    /// </summary>
    [Serializable]
    public class TopSecretSplitItem : BaseItem
    {
        public long id { get; set; }
        public string name { get; set; }
        public float distance { get; set; }
        public string message { get; set; }

    }

    [Serializable]
    public class BaseItem
    {
        public DateTime createdDate { get; set; }
    }
}
