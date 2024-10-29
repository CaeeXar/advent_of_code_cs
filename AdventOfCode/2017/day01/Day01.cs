namespace AOC2017.Day01
{
    internal class Day01
    {
        private int total { get; set; }
        string path = @"2017\day01\Input.txt";

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

            for(int i = 0; i < text.Length - 1; i++)
            {
                if (text[i] == text[i + 1])
                {
                    sum += int.Parse(text[i].ToString()); 
                }
            }

            if (text[0] == text[text.Length - 1])
            {
                sum += int.Parse(text[0].ToString());
            }

            return sum;
        }

        public int Part2()
        {
            string text = ReadFile();
            int sum = 0,
                half = text.Length / 2;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == text[(i + half) % text.Length])
                {
                    sum += int.Parse(text[i].ToString());
                }
            }

            return sum;
        }
    }
}
