using System.Collections.Immutable;

namespace AOC2024.Day01
{
    internal class Day01
    {
        private int total { get; set; }
        string path = @"2024\day01\Input.txt";

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
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            foreach(string line in ReadFile().Split("\n"))
            {
                int[] numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(d => int.Parse(d.Trim())).ToArray();
                left.Add(numbers[0]);
                right.Add(numbers[1]);
            }

            left.Sort();
            right.Sort();

            int sum = 0;
            for (int i = 0; i < left.Count; i++)
            {
                sum += Math.Abs(left[i] - right[i]);
            }

            return sum;
        }

        public int Part2()
        {
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            foreach (string line in ReadFile().Split("\n"))
            {
                int[] numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(d => int.Parse(d.Trim())).ToArray();
                left.Add(numbers[0]);
                right.Add(numbers[1]);
            }

            left.Sort();
            right.Sort();

            int sum = 0;
            for (int i = 0; i < left.Count; i++)
            {
                sum += left[i] * right.Count(d => d == left[i]);
            }

            return sum;
        }
    }
}
