using System.Text.RegularExpressions;

namespace AOC2024.Day03
{
    internal class Day03
    {
        string path = @"2024\day03\Input.txt";

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
            string patternMuls = @"mul\((\d{3}|\d{2}|\d{1}),(\d{3}|\d{2}|\d{1})\)";
            string patternMul = @"mul\(|\)";
            int total = 0;
            foreach (Match match in Regex.Matches(ReadFile(), patternMuls))
            {
                int[] numbers = Regex.Replace(match.ToString(), patternMul, "")
                                        .Split(",")
                                        .Select(d => int.Parse(d)).ToArray();
                total += numbers.Aggregate(1, 
                                           (current, totalProduct) => 
                                            current *= totalProduct);
            }

            return total;
        }

        public int Part2()
        {
            string patternMuls = @"don't\(\)|do\(\)|mul\((\d{3}|\d{2}|\d{1}),(\d{3}|\d{2}|\d{1})\)";
            string patternMul = @"mul\(|\)";
            int total = 0;
            bool dont = false;
            foreach (Match match in Regex.Matches(ReadFile(), patternMuls))
            {
                if (match.ToString().Equals("don't()"))
                {
                    dont = true;
                }
                else if (match.ToString().Equals("do()"))
                {
                    dont = false;
                    continue;
                }

                if (dont) continue;

                int[] numbers = Regex.Replace(match.ToString(), patternMul, "")
                                        .Split(",")
                                        .Select(d => int.Parse(d)).ToArray();
                total += numbers.Aggregate(1,
                                           (current, totalProduct) =>
                                            current *= totalProduct);
            }

            return total;
        }
    }
}
