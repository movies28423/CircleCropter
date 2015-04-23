using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CircleCropter
{
    class Program
    {
        protected static bool moreThanHalfInCircle(int xPos, int yPos, int centerOfCircleX, int centerOfCircleY, int radius) {
            return Math.Pow(xPos - centerOfCircleX, 2) + Math.Pow(yPos - centerOfCircleY, 2) <= Math.Pow(radius, 2);
        }

        protected static Dictionary<string, List<int>> circleCropter(int x, int y, int radius, string[,] xymap)
        {
            int xStart = radius;
            int xEnd = x - radius;

            int yStart = radius;
            int yEnd = y - radius;

            Dictionary<string,List<int>> highestCountPoints = new Dictionary<string,List<int>>();
            highestCountPoints.Add("x", new List<int>());
            highestCountPoints.Add("y", new List<int>());
            highestCountPoints.Add("Count", new List<int>());
            highestCountPoints["Count"].Add(0);

            for (int i = xStart; i < xEnd; i++) {
                for (int j = yStart; j < yEnd; j++) {
                    int ixStart = i - radius;
                    int jyStart = j - radius;

                    int currentCount = 0;
                    for (int k = ixStart; k < i + radius; k++)
                    {
                        for (int l = jyStart; l < j + radius; l++)
                        {
                            if (xymap[k,l] == "x" && moreThanHalfInCircle(k, l, i, j, radius))
                            {
                                currentCount++;
                            }
                        }
                    }

                    if (xymap[i, j] == "x")
                        currentCount--;

                    if (currentCount > highestCountPoints["Count"][0]) {
                        highestCountPoints["x"] = new List<int>(){i};
                        highestCountPoints["y"] = new List<int>() {j};
                        highestCountPoints["Count"][0] = currentCount;
                    }
                }
            }
            
                return highestCountPoints;
            }

        protected static string[,] ReadMap(string fileMap)
        {
            List<string> xMap = new List<string>();
            using(StreamReader sr = new StreamReader(fileMap))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    xMap.Add(line);
                }
                sr.Close();
            }
            int strLength = xMap.ElementAt(0).Length;
            string[,] xyGrid = new string[xMap.Count, xMap.ElementAt(0).Length];

            for(int i = 0; i < xMap.Count; i++)
            {
                for (int j = 0; j < xMap.ElementAt(0).Length; j++)
                {
                    xyGrid[i, j] = xMap[i][j].ToString();
                }
            }

            return xyGrid;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("X Length:");
            int getX = Int32.Parse(Console.ReadLine());
            Console.WriteLine("X Length:");
            int getY = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Radius:");
            int radius = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Enter Map Location:");
            string MapLocation = @Console.ReadLine();
            string[,] xyMap = ReadMap(MapLocation);

            Dictionary<string, List<int>> getOutput = circleCropter(getX, getY, radius, xyMap);

            foreach(string currentKey in getOutput.Keys)
            {
                Console.WriteLine(currentKey + ": " + getOutput[currentKey][0]);
            }
            Console.ReadLine();
        }
    }
}
