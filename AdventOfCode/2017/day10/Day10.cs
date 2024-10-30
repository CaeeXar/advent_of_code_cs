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
            int[] list = null;
            GenerateArray(ref list, 256);
            int[] lengths = ReadFile()
                            .Split(",")
                            .Select(d => int.Parse(d.Trim()))
                            .ToArray();
            KnotHash(lengths, list);
            return list[0] * list[1];
        }

        public int Part2()
        {
            return 0;
        }

        private void KnotHash(int[] lengths, int[] list)
        {
            int pos = 0, sz = 0;
            foreach (int length in lengths)
            {
                int[] sub = CopySubset(list, pos, length);
                for (int i = 0; i < length; i++)
                {
                    list[(pos + i) % list.Length] = sub[i];
                }

                pos = (pos + length + sz) % list.Length;
                sz++;
            }
        }

        private int[] CopySubset(int[] list, int pos, int length)
        {
            int[] sub = new int[length];
            int index = 0;
            for(int i = pos; i < pos+length; i++)
            {
                sub[index++] = list[i % list.Length];
            }

            Array.Reverse(sub);
            return sub;
        }

        private void GenerateArray(ref int[] emptyList, int length = 5)
        {
            emptyList = new int[length];
            for(int i = 0; i < length; i++)
            {
                emptyList[i] = i;
            }
        }
    }
}
