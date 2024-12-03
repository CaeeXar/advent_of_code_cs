namespace AOC2024.Day02
{
    internal class Day02
    {
        private int total { get; set; }
        string path = @"2024\day02\Input.txt";

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
            List<List<int>> reports = new List<List<int>>();
            foreach (var item in ReadFile().Split("\n"))
            {
                reports.Add(item.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(d => int.Parse(d.Trim())).ToList());
            }

            int sgn = 0, sum = 0;
            bool safe = false;
            foreach (var item in reports)
            {
                sgn = Math.Sign(item[0] - item[1]);
                safe = true;
                for (int i = 0; i < item.Count - 1; i++)
                {
                    int d = Math.Abs(item[i] - item[i + 1]);
                    if (sgn != Math.Sign(item[i] - item[i + 1]) || 
                        d > 3 || d < 1)
                    {
                        safe = false;
                        break;
                    }
                }

                if (safe) sum++;
            }

            return sum;
        }

        public int Part2()
        {
            List<List<int>> reports = new List<List<int>>();
            foreach (var item in ReadFile().Split("\n"))
            {
                reports.Add(item.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(d => int.Parse(d.Trim())).ToList());
            }
            List<List<int>> copy = new List<List<int>>(reports);

            int sgn = 0, sum = 0;
            bool safe = true;
            foreach (List<int> report in reports)
            {
                for (int i = -1; i < report.Count; i++)
                {
                    List<int> removed = new List<int>(report);
                    if (i >= 0) removed.RemoveAt(i);
                    sgn = Math.Sign(removed[0] - removed[1]);
                    safe = true;

                    for (int j = 0; j < removed.Count - 1; j++)
                    {
                        int d = Math.Abs(removed[j] - removed[j + 1]);
                        if (sgn != Math.Sign(removed[j] - removed[j + 1]) ||
                            d > 3 || d < 1)
                        {
                            safe = false;
                            break;
                        }
                    }

                    if (safe)
                    {
                        sum++;
                        break;
                    }
                }
            }

            return sum;
        }
    }
}
