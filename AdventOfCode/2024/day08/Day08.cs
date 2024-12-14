namespace AOC2024.Day08
{
    using Point = (int x, int y);

    internal class Day08
    {
        string path = @"2024\day08\Input.txt";

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
            string[] map = ReadFile().Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            Dictionary<char, List<Point>> antennas;
            FindAnteannasInMap(map, out antennas);

            HashSet<Point> distinctAntinodes;
            CreateDistinctAntinodes(antennas, 
                                    out distinctAntinodes, 
                                    new Point(0, 0), 
                                    new Point(map[0].Length - 1, map.Length - 1));
            
            return distinctAntinodes.Count;
        }

        public int Part2()
        {
            string[] map = ReadFile().Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            Dictionary<char, List<Point>> antennas;
            FindAnteannasInMap(map, out antennas);

            HashSet<Point> distinctAntinodes;
            CreateMaxDistinctAntinodes(antennas,
                                    out distinctAntinodes,
                                    new Point(0, 0),
                                    new Point(map[0].Length - 1, map.Length - 1));

            // add anteannas as antinodes if not already
            foreach (var kvp in antennas)
            {
                HashSet<Point> distinct = kvp.Value.ToHashSet();
                distinctAntinodes.UnionWith(distinct);
            }

            return distinctAntinodes.Count;
        }

        private void CreateMaxDistinctAntinodes(Dictionary<char, List<Point>> antennas,
                                                out HashSet<Point> distinctAntinodes,
                                                Point boundaryMin,
                                                Point boundaryMax)
        {
            distinctAntinodes = new HashSet<Point>();
            foreach (var kvp in antennas)
            {
                List<Point> _anteannas = kvp.Value;

                // binom coeff
                for (int i = 0; i < _anteannas.Count - 1; i++)
                {
                    for (int j = i + 1; j < _anteannas.Count; j++)
                    {
                        int dx = (_anteannas[i].x - _anteannas[j].x),
                            dy = (_anteannas[i].y - _anteannas[j].y);

                        Point newAntinode1 = new Point(_anteannas[i].x + dx, _anteannas[i].y + dy),
                              newAntinode2 = new Point(_anteannas[j].x - dx, _anteannas[j].y - dy);

                        while (WithinBoundary(newAntinode1, boundaryMin, boundaryMax))
                        {
                            distinctAntinodes.Add(newAntinode1);
                            newAntinode1 = new Point(newAntinode1.x + dx, newAntinode1.y + dy);
                        }

                        while (WithinBoundary(newAntinode2, boundaryMin, boundaryMax))
                        {
                            distinctAntinodes.Add(newAntinode2);
                            newAntinode2 = new Point(newAntinode2.x - dx, newAntinode2.y - dy);
                        }
                    }
                }
            }
        }

        private void CreateDistinctAntinodes(Dictionary<char, List<Point>> antennas, 
                                             out HashSet<Point> distinctAntinodes,
                                             Point boundaryMin, 
                                             Point boundaryMax)
        {
            distinctAntinodes = new HashSet<Point>();
            foreach (var kvp in antennas)
            {
                List<Point> _anteannas = kvp.Value;
                // binom coeff
                for (int i = 0; i < _anteannas.Count - 1; i++)
                {
                    for (int j = i + 1; j < _anteannas.Count; j++)
                    {
                        int dx = (_anteannas[i].x - _anteannas[j].x),
                            dy = (_anteannas[i].y - _anteannas[j].y);

                        Point newAntinode1 = new Point(_anteannas[i].x + dx, _anteannas[i].y + dy),
                              newAntinode2 = new Point(_anteannas[j].x - dx, _anteannas[j].y - dy);

                        if (WithinBoundary(newAntinode1, boundaryMin, boundaryMax))
                        {
                            distinctAntinodes.Add(newAntinode1);
                        }

                        if (WithinBoundary(newAntinode2, boundaryMin, boundaryMax))
                        {
                            distinctAntinodes.Add(newAntinode2);
                        }
                    }
                }
            }
        }

        private bool WithinBoundary(Point p, Point min, Point max)
        {
            return p.x >= min.x && p.x <= max.x &&
                   p.y >= min.y && p.y <= max.y;
        }

        private void FindAnteannasInMap(string[] map, out Dictionary<char, List<Point>> antennas)
        {
            antennas = new Dictionary<char, List<Point>>();
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    char key = map[row][col];
                    if (key != '.')
                    {
                        Point newAntenna = new Point(col, row);
                        if (!antennas.ContainsKey(key))
                        {
                            antennas.Add(key, new List<Point> { newAntenna });
                        }
                        else
                        {
                            antennas[key].Add(newAntenna);
                        }
                    }
                }
            }
        }
    }
}
