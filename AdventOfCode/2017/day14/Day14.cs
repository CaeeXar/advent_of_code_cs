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
            string input = ReadFile().Trim();
            string[] hashes = GetStrings(input);
            return hashes.Aggregate(
                0,
                (total, next) => total + next.Count(d => d == '1')
            );
        }

        public int Part2()
        {
            string input = ReadFile().Trim();
            string[] hashes = GetStrings(input);
            int groups = 0;
            for (int row = 0; row < hashes.Length; row++)
            {
                for (int col = 0; col < hashes[row].Length; col++)
                {
                    if (hashes[row].ElementAt(col) == '0')
                    {
                        continue;
                    }

                    Point current = new Point(col, row);
                    DeleteGroup(ref hashes, current);
                    groups++;
                }
            }

            return groups;
        }

        private void DeleteGroup(ref string[] hashes, Point start)
        {
            List<Point> queue = new List<Point>() { start };
            ReplaceAt(ref hashes[start.Y], start.X);
            while (queue.Count > 0)
            {
                Point current = queue[0];
                foreach (Point adj in new Point[] { new Point(0, -1),
                                                    new Point(1,  0),
                                                    new Point(0,  1),
                                                    new Point(-1, 0) })
                {
                    Point neighbour = current + adj;
                    if (neighbour.Y < 0 || neighbour.Y >= hashes.Length ||
                        neighbour.X < 0 || neighbour.X >= hashes[0].Length)
                    {
                        continue;
                    }

                    if (hashes[neighbour.Y].ElementAt(neighbour.X) == '1' && !queue.Contains(neighbour))
                    {
                        ReplaceAt(ref hashes[neighbour.Y], neighbour.X);
                        queue.Add(neighbour);
                    }
                }

                queue.RemoveAt(0);
            }
        }

        private void ReplaceAt(ref string input, int index, string newValue = "0")
        {
            input = input.Remove(index, 1).Insert(index, "0");
        }

        private string[] GetStrings(string input)
        {
            string[] hashes = new string[128];
            for (int i = 0; i < 128; i++)
            {
                foreach (char c in Day10.KnotHash.GetKnotHash($"{input}-{i}"))
                {
                    hashes[i] += ConvertToBitString(c.ToString());
                }
            }

            return hashes;
        }

        private string ConvertToBitString(string knotHash)
        {
            return Convert.ToString(Convert.ToInt64(knotHash, 16), 2).PadLeft(4, '0');
        }
    }
}
