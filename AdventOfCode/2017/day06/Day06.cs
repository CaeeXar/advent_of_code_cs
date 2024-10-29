using System;

namespace AOC2017.Day06
{
    internal class Day06
    {
        string path = @"2017\day06\Input.txt";

        public string ReadFile()
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public int Part1()
        {
            int[] instructions = ParseInput(ReadFile());
            return FindLoops(instructions).FirstLoop;
        }

        public int Part2()
        {
            int[] instructions = ParseInput(ReadFile());
            return FindLoops(instructions).SecondLoop;
        }

        private Banks FindLoops(int[] banks)
        {
            int currentCycle = 1;
            string id = string.Empty;

            Dictionary<string, Banks> seen = new Dictionary<string, Banks>();
            Banks current = new Banks(banks);
            seen.Add(current.GetID(), current);

            while(true)
            {
                Banks next = current.GetNext();
                Banks? value;
                if (seen.ContainsKey(next.GetID()) && seen.TryGetValue(next.GetID(), out value))
                {
                    if(id == string.Empty)
                    {
                        value.FirstLoop = currentCycle; // first loop detected here
                        id = value.GetID(); // save id of first bank to loop for later comparison
                    }
                    else if(id == value.GetID())
                    {
                        value.SecondLoop= currentCycle - value.FirstLoop; // second loop detected here
                        return value;
                    }
                }
                else
                {
                    seen.Add(next.GetID(), next);
                }

                currentCycle++;
                current = next;
            }
        }

        private int[] ParseInput(string input)
        {
            return input.Split('\t')
                        .Select(line => line.Trim())
                        .ToArray()
                        .Select(int.Parse)
                        .ToArray();
        }
    }
}
