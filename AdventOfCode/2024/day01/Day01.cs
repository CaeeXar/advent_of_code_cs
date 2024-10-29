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
            return 0;
        }

        public int Part2()
        {
            return 0;
        }
    }
}
