using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImperialSniffer
{
    /// <summary>
    /// This class has the tools used to resolve base algorithms. They can be improved here, so there is no need to change the main logic.
    /// All theese data processing methods can be improved or either use some external API to be resolved.
    /// </summary>
    public static class BaseTools
    {
        /// <summary>
        /// This Method determine the position of a point in a 2D Dimension from the 3 distances to 3 known points (trilateration algorithm).
        /// TODO: Look for a better solution. It can not determine when distances and known points given are incorrect. The results from wrong given points will be not correct.
        /// </summary>
        /// <param name="knownPoint1"></param>
        /// <param name="distance1"></param>
        /// <param name="knownPoint2"></param>
        /// <param name="distance2"></param>
        /// <param name="knownPoint3"></param>
        /// <param name="distance3"></param>
        /// <returns></returns>
        public static XYCoordinates TrilaterationSolver(
                XYCoordinates knownPoint1, float distance1,
                XYCoordinates knownPoint2, float distance2,
                XYCoordinates knownPoint3, float distance3)
        {

            float[] point1 = new float[2];
            float[] point2 = new float[2];
            float[] point3 = new float[2];
            float[] auxX = new float[2];
            float[] auxY = new float[2];
            float[] p3p1 = new float[2];
            float temp = 0;
            float temp2;
            float p3p1i = 0;
            float xvalue;
            float yvalue;
            float jvalue = 0;
            float ivalue = 0;
            float d;
            float finalPointX;
            float finalPointY;

            //Get vectors from points
            point1[0] = knownPoint1.GetCoordinateX();
            point1[1] = knownPoint1.GetCoordinateY();
            point2[0] = knownPoint2.GetCoordinateX();
            point2[1] = knownPoint2.GetCoordinateY();
            point3[0] = knownPoint3.GetCoordinateX();
            point3[1] = knownPoint3.GetCoordinateY();


            for (int i = 0; i < point1.Length; i++)
            {
                temp2 = point2[i] - point1[i];
                temp += (temp2 * temp2);
            }
            d = (float)Math.Sqrt(temp);
            for (int i = 0; i < point1.Length; i++)
            {
                auxX[i] = (float)(point2[i] - point1[i]) / (float)(Math.Sqrt(temp)); ;
            }
            for (int i = 0; i < point3.Length; i++)
            {
                p3p1[i] = point3[i] - point1[i];
            }
            for (int i = 0; i < auxX.Length; i++)
            {
                ivalue += (auxX[i] * p3p1[i]);
            }
            for (int i = 0; i < point3.Length; i++)
            {
                temp2 = point3[i] - point1[i] - (auxX[i] * ivalue);
                p3p1i += (temp2 * temp2);
            }
            for (int i = 0; i < point3.Length; i++)
            {  
                auxY[i] = (point3[i] - point1[i] - (auxX[i] * ivalue)) / (float)Math.Sqrt(p3p1i); ;
            }
            for (int i = 0; i < auxY.Length; i++)
            {
                jvalue += (auxY[i] * p3p1[i]);
            }
            xvalue = (float)(Math.Pow(distance1, 2) - Math.Pow(distance2, 2) + Math.Pow(d, 2)) / (2 * d);
            yvalue = (float)((Math.Pow(distance1, 2) - Math.Pow(distance3, 2) + Math.Pow(ivalue, 2) + Math.Pow(jvalue, 2)) / (2 * jvalue)) - ((ivalue / jvalue) * xvalue);

            finalPointX = knownPoint1.GetCoordinateX() + (auxX[0] * xvalue) + (auxY[0] * yvalue);
            finalPointY = knownPoint1.GetCoordinateY() + (auxX[1] * xvalue) + (auxY[1] * yvalue);

            //TODO: Here I clould at least check if the 2D point resulted match with the others points distances.

            XYCoordinates location = new XYCoordinates(finalPointX, finalPointY);
            return location;
        }

        /// <summary>
        /// Returns the message composition generated from the first not empty word at every position in the List of String List.
        /// </summary>
        /// <param name="receivedMessages"></param>
        /// <returns></returns>
        public static string GetStringFromStringMatrix(List<List<string>> receivedMessages)
        {

            string retVal = "";
            string[] finalMessage = new string[receivedMessages[0].Count];
            finalMessage = finalMessage.Select(i => "").ToArray();

            for (int k = 0; k < receivedMessages[0].Count; k++)
            {
                string word = "";
                for (int i = 0; i < receivedMessages.Count; i++)
                {
                    if (receivedMessages[i].Count != receivedMessages[0].Count)
                    {
                        throw new Exception("Error, messages number of every list must have the same length");
                    }
                    if (!receivedMessages[i][k].Equals(""))
                    {
                        //The first not empty string found is load in its message position.
                        word = receivedMessages[i][k];
                        break;
                    }
                }
                if (word.Equals(""))
                {
                    throw new Exception("Error, Imperial Ship Message can not be deciphered, at least one of the word is not readable");
                }

                retVal += word + " ";
            }
            retVal = retVal.Substring(0, retVal.Length - 1);
            return retVal;
        }

    }
}