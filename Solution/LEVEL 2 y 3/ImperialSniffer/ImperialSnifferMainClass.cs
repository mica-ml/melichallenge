using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImperialSniffer.Models;
using ImperialSniffer.Controllers;
using System.IO;

namespace ImperialSniffer
{
    public class ImperialSnifferMainClass
    {
        public ImperialSnifferMainClass()
        {

        }

        /// <summary>
        /// This main method resolve the required conditions of the topsecret level 2 exercice.
        /// Ask for the methods created in exercice level 1 GetPosition and GetMessage
        /// </summary>
        /// <param name="allSatelites"></param>
        /// <returns></returns>
        public TopSecretResponseData GetImperialShipInformation(AllSatellitesData allSatelites)
        {
            //use the TopSecret exercice required response Data from the response.
            TopSecretResponseData topSecretResponseData = new TopSecretResponseData();
            
            //Get Location
            ShipLocator shipLocator = new ShipLocator();
            XYCoordinates shipLocation = shipLocator.GetLocation(allSatelites);
            topSecretResponseData.position.x = shipLocation.GetCoordinateX();
            topSecretResponseData.position.y = shipLocation.GetCoordinateY();

            ///Get Message
            MessageAssembler messageAssembler = new MessageAssembler();
            List<List<string>> messagesFromSatellites = messageAssembler.GetMessagesListFromSatelliteData(allSatelites);
            topSecretResponseData.message = messageAssembler.GetMessage(messagesFromSatellites);

            return topSecretResponseData;
        }
    }

    /// <summary>
    /// Simple Log Class, this must be better implemented. It is just for internal use.
    /// </summary>
    public class FileLogger 
    {
        public string filePath = "./ImperialSnifferLog.txt";
        public void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }
    }
}
