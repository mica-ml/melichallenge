using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialSnifferSolution
{
    public class ImperialSniffer
    {
        public RebelSatellites rebelSatellites = new RebelSatellites();
        ShipLocator shipLocator = new ShipLocator();
        CommunicationSystem _communicationSystem;

        public ImperialSniffer()
        {
            //Initialization
            _communicationSystem = new CommunicationSystem(this);
            ExecuteLocator();
            ExecuteMessageAssembler();
        }

        public void ExecuteLocator()
        {
            List<float> distanceFromSatellites = new List<float>();
            string finishDesition = "";
            distanceFromSatellites = _communicationSystem.GetDistanceFromSatellites();

            XYCoordinates shipLocation = shipLocator.GetLocation(distanceFromSatellites);
            Console.WriteLine("Ship Location: (" + shipLocation.GetCoordinateX() + ";" + shipLocation.GetCoordinateY() + ")");
        }

        void ExecuteMessageAssembler()
        {
            Console.WriteLine("\n Test GetMessage");

            List<List<string>> allMessages = _communicationSystem.GetMessagesFromSatellites();
            try
            {
                MessageAssembler messageAssembler = new MessageAssembler();
                string message = messageAssembler.GetMessage(allMessages);
                Console.WriteLine("\n GetMessage response = " + message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }

            Console.WriteLine("\n\n Enter to finish");
            Console.ReadLine();
        }
    }
}

