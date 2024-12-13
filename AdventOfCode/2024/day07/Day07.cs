using System.Numerics;

namespace AOC2024.Day07
{
    internal class Day07
    {
        string path = @"2024\day07\Input.txt";

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
            string[] lines = ReadFile().Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            Dictionary<long, long[]> calibrations = new Dictionary<long, long[]>();
            foreach (string line in lines)
            {
                string[] data = line.Split(":").Select(d => d.Trim()).ToArray();
                long[] nums = data[1].Split(" ").Select(d => long.Parse(d.Trim())).ToArray();
                calibrations.Add(long.Parse(data[0].Trim()), nums);
            }

            long total = 0;
            foreach (var kvp in calibrations)
            {
                if (ValidEquation(kvp.Value, 0, kvp.Key)) total += kvp.Key;
            }

            return total;
        }

        public long Part2()
        {
            string[] lines = ReadFile().Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            Dictionary<long, BigInteger[]> calibrations = new Dictionary<long, BigInteger[]>();
            foreach (string line in lines)
            {
                string[] data = line.Split(":").Select(d => d.Trim()).ToArray();
                BigInteger[] nums = data[1].Split(" ").Select(d => BigInteger.Parse(d.Trim())).ToArray();
                calibrations.Add(long.Parse(data[0].Trim()), nums);
            }

            long total = 0;
            foreach (var kvp in calibrations)
            {
                if (ValidEquationWithConcat(kvp.Value, 0, kvp.Key)) total += kvp.Key;
            }

            return total;
        }

        private bool ValidEquation(long[] nums, long value, long result, long index = 0)
        {
            if (index >= nums.Length)
            {
                return value == result;
            }

            long next = nums[index];
            bool plus = ValidEquation(nums, value + next, result, index + 1),
                 mult = ValidEquation(nums, (value == 0 ? 1 : value) * next, result, index + 1);
            return plus || mult;            
        }

        private bool ValidEquationWithConcat(BigInteger[] nums, BigInteger value, BigInteger result, int index = 0)
        {

            if (index >= nums.Length)
            {
                return value == result;
            }

            BigInteger next = nums[index];
            bool plus = ValidEquationWithConcat(nums, value + next, result, index + 1),
                 mult = ValidEquationWithConcat(nums, (value == 0 ? 1 : value) * next, result, index + 1),
                 conc = ValidEquationWithConcat(nums, BigInteger.Parse(value.ToString() + next.ToString()), result, index + 1);
            return plus || mult || conc;
        }
    }
}
