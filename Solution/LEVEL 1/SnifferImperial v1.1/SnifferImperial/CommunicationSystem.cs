using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialSnifferSolution
{
    public class CommunicationSystem
    {
        List<float> _distancesFromPoints = new List<float>();
        ImperialSniffer imperialSnifferContext;
        
        public CommunicationSystem(ImperialSniffer imperialSniffer)
        {
            imperialSnifferContext = imperialSniffer;
        }
        public List<float> GetDistanceFromSatellites()
        {
            _distancesFromPoints.Clear();
            for (int i = 0; i < imperialSnifferContext.rebelSatellites.GetRebelSatellitesMinimumNumber(); i++)
            {
                Console.WriteLine("Distance of the Imperial Ship from the Rebels satellite " + imperialSnifferContext.rebelSatellites.rebelSatellitesPositionList.ElementAt(i).Key + "[in mts]:");
                _distancesFromPoints.Add(float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture.NumberFormat));            
            }
            return _distancesFromPoints;
        }

        public List<List<string>> GetMessagesFromSatellites()
        {
            List<List<string>> allMessages = new List<List<string>>();

            Console.WriteLine("Get number of words y every message:");
            float numberOfWords = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture.NumberFormat);

            for (int i = 0; i < imperialSnifferContext.rebelSatellites.GetRebelSatellitesMinimumNumber(); i++)
            {
                Console.WriteLine("Message received for Rebels satellite " + imperialSnifferContext.rebelSatellites.rebelSatellitesPositionList.ElementAt(i).Key + ":\n");
                List<string> satelliteMessage = new List<string>();
                for (int j = 0; j < numberOfWords; j++)
                {
                    Console.WriteLine("Word position " + j + "received for Rebels satellite " + imperialSnifferContext.rebelSatellites.rebelSatellitesPositionList.ElementAt(i).Key + "(Doble Enter to not enter a word):");
                    string word = Console.ReadLine();
                    satelliteMessage.Add(Console.ReadLine());
                }
                allMessages.Add(satelliteMessage);
            }
            return allMessages;
        }
    }
}
