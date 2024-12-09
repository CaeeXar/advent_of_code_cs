namespace AOC2024.Day06
{
    internal class Day06
    {
        string path = @"2024\day06\Input.txt";

        private class Point
        {
            public Point (int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }

            public override string ToString()
            {
                return $"({this.X}, {this.Y})";
            }

            public override int GetHashCode()
            {
                return this.X + this.Y;
            }
        
            public override bool Equals(object? obj)
            {
                if (obj == null) return false;
                Point other = (Point)obj;
                return (other.X == this.X && other.Y == this.Y);
            }
        }

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
                                     .Select(d => d.Trim().ToCharArray())
                                     .ToArray();
            return CountGuardSteps(map);
        }

        public int Part2()
        {
            char[][] origMap = ReadFile().Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None)
                                     .Select(d => d.Trim().ToCharArray())
                                     .ToArray();
            char[][] map = origMap.Select(d => d.Select(c => c).ToArray()).ToArray();

            int loops = 0;
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] != '#' && map[row][col] != '^')
                    {
                        map[row][col] = '#';
                        if (CountGuardSteps(map) == -1) loops++;
                        map = origMap.Select(d => d.Select(c => c).ToArray()).ToArray();
                    }
                }
            }

            return loops;
        }

        private int CountGuardSteps(char[][] map)
        {
            Point current = FindStartPosition(map);
            Point direction = new Point(0, -1);
            Dictionary<Point, int> edges = new Dictionary<Point, int>();
            int steps = 1;
            while (true)
            {
                if (edges.ContainsKey(current) && edges[current] >= 3) return -1;
                
                map[current.Y][current.X] = 'X';
                Point next = new Point(current.X + direction.X, current.Y + direction.Y);

                if (next.X < 0 || next.X >= map[0].Length ||
                    next.Y < 0 || next.Y >= map.Length)
                {
                    return steps;
                }
                else if (map[next.Y][next.X] == '#')
                {
                    if (edges.ContainsKey(current)) edges[current]++;
                    else edges.Add(new Point(current.X, current.Y), 1);
                    ChangeDirection(ref direction);
                    continue;
                }
                else if (map[next.Y][next.X] != 'X')
                {
                    steps++;
                }

                current.X = next.X;
                current.Y = next.Y;
            }
        }

        private void ChangeDirection(ref Point direction)
        {
            if (direction.Y == -1)
            {
                direction.X = 1;
                direction.Y = 0;
            }
            else if (direction.X == 1)
            {
                direction.X = 0;
                direction.Y = 1;
            }
            else if (direction.Y == 1)
            {
                direction.X = -1;
                direction.Y = 0;
            }
            else if (direction.X == -1)
            {
                direction.X = 0;
                direction.Y = -1;
            }
        }

        private Point FindStartPosition(char[][] map)
        {
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] == '^') return new Point(col, row);
                }
            }

            return null;
        }
    }
}
