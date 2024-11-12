namespace AOC2017.Day15_1
{
    internal class Day15
    {      

        string path = @"2017\day15_1\Input.txt";

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
            int pairs = 0;
            string[] lines = ReadFile().Split("\n");
            long A = long.Parse(lines[0].Trim());
            long B = long.Parse(lines[1].Trim());
            long aFac = 16807;
            long bFac = 48271;
            for (long i = 1; i <= 40e6; i++)
            {
                A = GetNext(A, aFac);
                B = GetNext(B, bFac);
                if (GetLast16Binary(A) == GetLast16Binary(B)) pairs++;
            }

            return pairs;
        }

        public int Part2()
        {
            int pairs = 0;
            string[] lines = ReadFile().Split("\n");
            long A = long.Parse(lines[0].Trim());
            long B = long.Parse(lines[1].Trim());
            long aFac = 16807;
            long bFac = 48271;
            for (long i = 1; i <= 5e6; i++)
            {
                A = GetNextMultiple(A, aFac, 4);
                B = GetNextMultiple(B, bFac, 8);
                if (GetLast16Binary(A) == GetLast16Binary(B)) pairs++;
            }

            return pairs;
        }

        private string GetLast16Binary(long num)
        {
            string binary = Convert.ToString(num, 2);
            return binary.Substring(Math.Max(0, binary.Length - 16));
        }

        private long GetNext(long num, long fac)
        {
            long div = 2147483647;
            return (fac * num) % div;
        }

        private long GetNextMultiple(long num, long fac, long multiple)
        {
            long div = 2147483647;
            long next = (fac * num) % div;
            if (next % multiple == 0) return next;
            else return GetNextMultiple(next, fac, multiple);
        }
    }
}
