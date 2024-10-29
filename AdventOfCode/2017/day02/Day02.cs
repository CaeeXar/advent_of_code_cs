using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2017.Day02
{
    internal class Day02
    {
        string path = @"2017\day02\Input.txt";

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

        public int Part1()
        {
            string text = ReadFile();
            int sum = 0;

            foreach (var item in text.Split('\n'))
            {
                int[] sorted = Array.ConvertAll(item.Split(' '), int.Parse);
                Array.Sort(sorted);
                sum += (sorted[sorted.Length - 1] - sorted[0]);
            }

            return sum;
        }

        public int Part2()
        {
            string text = ReadFile();
            int sum = 0;

            foreach (var item in text.Split('\n'))
            {
                int[] sorted = Array.ConvertAll(item.Split(' '), int.Parse);
                sum += GetDivisionValue(sorted);
            }

            return sum;
        }

        private int GetDivisionValue(int[] values)
        {
            for(int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    if (values[i] != values[j] && values[i] % values[j] == 0)
                    {
                        return values[i] / values[j]; 
                    }
                }
            }

            return 0;
        }
    }
}
