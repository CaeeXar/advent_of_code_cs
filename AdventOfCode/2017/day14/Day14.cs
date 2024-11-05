namespace AOC2017.Day14
{
    internal class Day14
    {      

        string path = @"2017\day14\Input.txt";

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
            List<string> hashes = new List<string>();
            string input = ReadFile().Trim();
            int sum = 0;
            for (int i = 0; i < 128; i++)
            {
                foreach (char c in Day10.KnotHash.GetKnotHash($"{input}-{i}"))
                {
                    sum += ConvertToBits(c.ToString()).Count(d => d == '1');
                }
            }
            
            return sum;
        }

        public int Part2()
        {
            return 0;
        }

        private string ConvertToBits(string knotHash)
        {
            return Convert.ToString(Convert.ToInt64(knotHash, 16), 2).PadLeft(4, '0');
        }
    }
}
