using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2017.Day03
{
    internal class Day03
    {
        string path = @"2017\day03\Input.txt";

        public string ReadFile()
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }

        public long Part1()
        {
            int input;
            int.TryParse(ReadFile(), out input);
            Point origin = new Point(0, 0),
                  ulamCoords = GetUlamSpiralCoordinates(input);
            return GetManhattanDistance(origin, ulamCoords);
        }

        public long Part2()
        {
            long input;
            long.TryParse(ReadFile(), out input);
            return FindNextBiggerNumberInSpiral(input);
        }

        private long FindNextBiggerNumberInSpiral(long number)
        {
            long size = (long)Math.Ceiling(Math.Sqrt(number));
            long middle = (long)size / 2;
            long[,] numbers = new long[size, size];
            Point start = new Point(middle, middle);
            numbers[middle, middle] = 1;
            long bigger = 0;
            
            for (long i = 2; i <= number; i++)
            {
                start.X = middle;
                start.Y = middle;
                Point relCoords = GetUlamSpiralCoordinates(i);
                start.X += relCoords.X;
                start.Y -= relCoords.Y;

                long sum = 0;
                for(long x = -1; x <= 1; x++)
                {
                    for (long y = -1; y <= 1; y++)
                    {
                        if(start.X + x < numbers.GetLength(1) && start.X + x >= 0 &&
                           start.Y + y < numbers.GetLength(0) && start.Y + y >= 0 &&
                           numbers[start.Y + y, start.X + x] != 0)
                        {
                            sum += numbers[start.Y + y, start.X + x];
                        }
                    }
                }

                if (sum > number) 
                { 
                    bigger = sum;
                    break; 
                }

                if (start.X < numbers.GetLength(1) && start.X >= 0 &&
                    start.Y < numbers.GetLength(0) && start.Y >= 0)
                {
                    numbers[start.Y, start.X] = sum;
                }
                
            }

            //for(int i = 0; i < numbers.GetLength(0) && i < 7; i++)
            //{
            //    for (int j = 0; j < numbers.GetLength(1) && j < 7; j++)
            //    {

            //        System.Console.Write(numbers[i,j] + "\t");
            //    }
            //    System.Console.WriteLine();
            //}

            return bigger;
        }

        private Point GetUlamSpiralCoordinates(long number)
        {
            if(number == 1)
            {
                return new Point(0, 0);
            }

            int radius = (int)Math.Ceiling((Math.Sqrt(number) - 1) / 2);
            int length = radius * 2 + 1;
            int total = (int)Math.Pow(length, 2);

            if (number >= (total - length + 1)) // number in bottom row (Y)
                                                // (between "total - length + 1" and "total")
            {
                return new Point(radius - (total - number), -radius);
            }
            else if (number >= (total - 2 * length + 2)) // number in left row (X)
                                                         // (between "total - 2 * length + 2" and "total - length + 1")
            {
                return new Point(-radius, (total - length + 1 - number) - radius);
            }
            else if (number >= (total - 3 * length + 3)) // number in top row (Y)
                                                         // (between "total - 3 * length + 3" and "total - 2 * length + 2")
            {
                return new Point((total - 2 * length + 2 - number) - radius, radius);
            }
            else // number in right row (X)
            {
                return new Point(radius, radius - (total - 3 * length + 3 - number));
            }
        }

        private long GetManhattanDistance(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }
    }
}
