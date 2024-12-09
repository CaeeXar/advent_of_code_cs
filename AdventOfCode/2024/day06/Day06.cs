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
        }

        private enum Map
        {
            PLAYER = '^',
            OBSTACLE = '#',
            FREE = '.',
            MARKED = 'X',
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
            Point current = FindStartPosition(map);
            Point direction = new Point(0, -1);
            int steps = 1;
            while (true)
            {
                map[current.Y][current.X] = (char)Map.MARKED;
                Point next = new Point(current.X + direction.X, current.Y + direction.Y);
                if (next.X < 0 || next.X >= map[0].Length ||
                    next.Y < 0 || next.Y >= map.Length)
                {
                    return steps;
                }
                else if (map[next.Y][next.X] == (char)Map.OBSTACLE)
                {
                    ChangeDirection(ref direction);
                    continue;
                }
                else if (map[next.Y][next.X] != (char)Map.MARKED)
                {
                    steps++;
                }

                current.X = next.X;
                current.Y = next.Y;
            }
        }

        public int Part2()
        {
            return 0;
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
                    if (map[row][col] == (char)Map.PLAYER) return new Point(col, row);
                }
            }

            return null;
        }
    }
}
