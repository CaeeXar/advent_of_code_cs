namespace AOC2017.Day10
{
    internal class Day10
    {
        private int total { get; set; }
        string path = @"2017\day10\Input.txt";

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
            return 0;
        }

        public int Part2()
        {
            return 0;
        }
    }
}
