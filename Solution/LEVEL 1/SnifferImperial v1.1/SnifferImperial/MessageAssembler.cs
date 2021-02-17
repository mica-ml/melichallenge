using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialSnifferSolution
{

    public class MessageAssembler
    {
        RebelSatellites _rebelSatellites = new RebelSatellites();

        /// <summary>
        /// This methods was taken from the exercice level 1 GetMessage. And use a BaseTool method. 
        /// TODO: search for a best matrix value solver. 
        /// </summary>
        /// <param name="receivedMessages"></param>
        /// <returns></returns>
        public string GetMessage(List<List<string>> receivedMessages)
        {
            string retVal = "";

            //first check consistency of the received Messages.
            CheckReceivedMessagesConsistency(receivedMessages);

            //Get the final Message from the Matrix of words
            retVal = BaseTools.GetStringFromStringMatrix(receivedMessages);

            return retVal;
        }


        void CheckReceivedMessagesConsistency(List<List<string>> receivedMessages)
        {
            //in this point I could check more than those conditions.
            if (receivedMessages.Count != _rebelSatellites.GetRebelSatellitesMinimumNumber())
            {
                throw new Exception("Error, There is no enough messages received from satellites. It must be at least 3");
            }
        }

        public List<List<string>> GetMessagesListFromSatelliteData(AllSatellitesData allSatellitesData)
        {
            List<List<string>> messages = new List<List<string>>();

            if (allSatellitesData.satellites.Count != _rebelSatellites.rebelSatellitesPositionList.Count)
            {
                throw new Exception("Error, the number of satellites must be at least " + _rebelSatellites.rebelSatellitesPositionList.Count);
            }

            foreach (SatelliteData satelliteData in allSatellitesData.satellites)
            {
                messages.Add(satelliteData.message);
            }

            return messages;
        }
    }
}
