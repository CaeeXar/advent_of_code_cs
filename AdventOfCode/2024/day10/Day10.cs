namespace AOC2024.Day10
{
    using System.Linq;
    using Point = (int x, int y);

    internal class Day10
    {
        string path = @"2024\day10\Input.txt";

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
            int[][] map = ReadFile().Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None)
                                     .Select(d => d.Trim()
                                                   .ToCharArray()
                                                   .Select(n => n == '.' ? -1 : int.Parse(n.ToString()))
                                                   .ToArray())
                                     .ToArray();
            Stack<Point> trailheads = FindTrailheads(map);
            int total = 0;
            while (trailheads.Count > 0)
            {
                Point trailhead = trailheads.Pop();
                total += CaluclateTrailheadScore(trailhead, map);
            }

            return total;
        }

        public int Part2()
        {
            return 0;
        }

        private int CaluclateTrailheadScore(Point trailhead, int[][] map)
        {
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(trailhead);
            HashSet<Point> visited = new HashSet<Point>();
            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };
            int total = 0;
            
            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();
                if (map[current.y][current.x] == 9 && !visited.Contains(current))
                {
                    total++;
                    visited.Add(new Point(current.x, current.y));
                }

                for (int i = 0; i < 4; i++)
                {
                    int x = current.x + dx[i], y = current.y + dy[i];
                    Point next = new Point(x, y);
                    if (WithinBoundary(next, map) && 
                        (map[current.y][current.x] + 1) == map[next.y][next.x])
                    {
                        queue.Enqueue(next);
                    }

                }
            }
            return total;
        }

        private bool WithinBoundary(Point p, int[][] map)
        {
            return p.x >= 0 && p.x < map[0].Length &&
                   p.y >= 0 && p.y < map.Length;
        }

        private Stack<Point> FindTrailheads(int[][] map)
        {
            Stack<Point> trailheads = new Stack<Point>();
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] == 0) trailheads.Push(new Point(col, row));
                }
            }

            return trailheads;
        }
    }
}
