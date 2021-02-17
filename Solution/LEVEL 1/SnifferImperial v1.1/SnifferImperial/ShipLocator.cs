using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialSnifferSolution
{

    public class ShipLocator
    {
        XYCoordinates _shipLocation;

        public XYCoordinates GetLocation(List<float> distancesFromSatellites)
        {
            XYCoordinates Kenobi = new XYCoordinates(-500, -200);
            XYCoordinates Skywalker = new XYCoordinates(100, -100);
            XYCoordinates Sato = new XYCoordinates(500, 100);

            return BaseTools.TrilaterationSolver(Kenobi, distancesFromSatellites[0], Skywalker, distancesFromSatellites[1], Sato, distancesFromSatellites[2]);
        }

    }
    public class XYCoordinates
    {
        public float xCoord;
        public float yCoord;
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

}
