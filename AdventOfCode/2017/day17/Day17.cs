using AOC2017.Day08;

namespace AOC2017.Day17
{
    internal class Day17
    {      

        string path = @"2017\day17\Input.txt";
        const int RUNS = (int)1e9;

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
            return GetValue2017(int.Parse(ReadFile().Trim()));
        }

        public int Part2()
        {
            return GetValue50e6(int.Parse(ReadFile().Trim()));
        }

        private int GetValue2017(int steps)
        {
            List<int> positions = new List<int>() { 0, 1 };
            int position = 1, value = 2;

            while (value <= 2017)
            {
                position = (position + steps) % positions.Count + 1;

                if (position >= positions.Count) positions.Add(value);
                else positions.Insert(position, value);

                value++;
            }

            int i2017 = positions.IndexOf(2017);
            return positions[i2017 + 1];
        }

        private int GetValue50e6(int steps)
        {
            int[] positions = { 0, 1 };
            int position = 1;
            for (int value = 2; value <= 50e6; value++)
            {
                position = (position + steps) % value + 1;
                if (position == 1) positions[1] = value; // only track value at pos 1
            }

            return positions[1];
        }
    }
}
