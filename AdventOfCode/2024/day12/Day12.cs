namespace AOC2024.Day12
{
    using System;
    using Point = (int x, int y);

    internal class Day12
    {
        string path = @"2024\day12\Input.txt";

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
            char[][] map = ReadFile().Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None)
                                     .Select(d => d.Trim()
                                                   .ToCharArray())
                                     .ToArray();
            List<List<Point>> regions;
            FindAreas(map, out regions);
            return CalculateCost(regions);
        }

        public long Part2()
        {
            char[][] map = ReadFile().Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None)
                                     .Select(d => d.Trim()
                                                   .ToCharArray())
                                     .ToArray();
            List<List<Point>> regions;
            FindAreas(map, out regions);
            return CalculateCostBulk(regions);
        }

        private int CalculateCostBulk(List<List<Point>> regions)
        {
            int cost = 0;
            Point[][] corners = {
                new[] { new Point(-1,  0), new Point(-1, -1), new Point( 0, -1) },
                new[] { new Point(0, -1), new Point(1, -1), new Point(1, 0) },
                new[] { new Point(0, 1), new Point(-1, 1), new Point(-1, 0) },
                new[] { new Point(1, 0), new Point(1, 1), new Point(0, 1) },
            };
            foreach (List<Point> region in regions)
            {
                int sides = 0;
                foreach (Point area in region)
                {
                    foreach (Point[] corner in corners)
                    {
                        Point n1 = new Point(area.x + corner[0].x, area.y + corner[0].y),
                              n2 = new Point(area.x + corner[1].x, area.y + corner[1].y),
                              n3 = new Point(area.x + corner[2].x, area.y + corner[2].y);

                        // internal, external, cross corners - checks
                        // ##  A#  #A
                        // A#, AA, A#
                        if (!region.Contains(n1) && !region.Contains(n2) && !region.Contains(n3)) sides++;
                        if (region.Contains(n1) && !region.Contains(n2) && region.Contains(n3)) sides++;
                        if (!region.Contains(n1) && region.Contains(n2) && !region.Contains(n3)) sides++;
                    }
                }

                cost += (region.Count * sides);
            }

            return cost;
        }

        private int CalculateCost(List<List<Point>> regions)
        {
            int cost = 0;
            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };
            foreach (List<Point> region in regions)
            {
                int perimeter = 0;
                foreach (Point area in region)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int x = dx[i], y = dy[i];
                        Point side = new Point(area.x + x, area.y + y);
                        if (!region.Contains(side)) perimeter++;
                    }
                }

                cost += (region.Count * perimeter);
            }

            return cost;
        }

        private void FindAreas(char[][] map, out List<List<Point>> regions)
        {
            regions = new List<List<Point>>();
            Queue<Point> regionsQ = new Queue<Point>(new[] { new Point(0, 0) });
            HashSet<Point> visited = new HashSet<Point>();
            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };
            while (regionsQ.Count > 0)
            {
                Point p = regionsQ.Dequeue();
                Queue<Point> queue = new Queue<Point>(new[] { p });
                if (visited.Contains(p)) continue;

                List<Point> region = new List<Point>();
                while (queue.Count > 0)
                {
                    Point currentPos = queue.Dequeue();
                    char currentRegion = map[currentPos.y][currentPos.x];
                    visited.Add(currentPos);

                    if (!region.Contains(currentPos))
                    {
                        region.Add(currentPos);
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        int x = dx[i], y = dy[i];
                        Point neighbour = new Point(currentPos.x + x, currentPos.y + y);
                        if (!WithinBounds(neighbour, map[0].Length - 1, map.Length - 1))
                        {
                            continue;
                        }

                        bool contains = visited.Contains(neighbour);
                        if (map[neighbour.y][neighbour.x] == currentRegion && !contains)
                        {
                            if (!queue.Contains(neighbour)) queue.Enqueue(neighbour);
                        }
                        else if (!contains)
                        {
                            if (!regionsQ.Contains(neighbour)) regionsQ.Enqueue(neighbour);
                        }
                    }
                }

                regions.Add(region);
            }
        }

        private bool WithinBounds(Point p, int maxX, int maxY)
        {
            return p.x >= 0 && p.x <= maxX &&
                   p.y >= 0 && p.y <= maxY;
        }
    }
}
