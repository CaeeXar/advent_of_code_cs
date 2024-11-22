namespace AOC2017.Day19
{
    internal class Day19
    {      
        struct Point
        {
            public Point(int x, int y)
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
        };

        enum Direction
        {
            UP,
            RIGHT,
            DOWN,
            LEFT,
        }

        string path = @"2017\day19\Input.txt";

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

        public string Part1()
        {
            string[] lines = ReadFile().Split("\n");
            char[][] map = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                map[i] = lines[i].ToCharArray();
            }

            Direction direction = Direction.DOWN;
            Point pos = new Point(Array.IndexOf(map[0], '|') , 0);

            string letters = "";
            while (true)
            {
                if (pos.Y >= map.Length || pos.X >= map[pos.Y].Length || 
                    pos.Y < 0 || pos.X < 0 || map[pos.Y][pos.X] == ' ') return letters;

                if (map[pos.Y][pos.X] >= 'A' && map[pos.Y][pos.X] <= 'Z') letters += map[pos.Y][pos.X];

                Point next = GetNextPosition(pos, direction, map);
                if (next.Y - pos.Y < 0) direction = Direction.UP;
                else if (next.Y - pos.Y > 0) direction = Direction.DOWN;
                else if (next.X - pos.X < 0) direction = Direction.LEFT;
                else if (next.X - pos.X > 0) direction = Direction.RIGHT;
                pos = next;
            }
        }

        public int Part2()
        {
            string[] lines = ReadFile().Split("\n");
            char[][] map = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                map[i] = lines[i].ToCharArray();
            }

            Direction direction = Direction.DOWN;
            Point pos = new Point(Array.IndexOf(map[0], '|'), 0);

            int steps = 0;
            while (true)
            {
                if (pos.Y >= map.Length || pos.X >= map[pos.Y].Length ||
                    pos.Y < 0 || pos.X < 0 || map[pos.Y][pos.X] == ' ') return steps;

                Point next = GetNextPosition(pos, direction, map);
                if (next.Y - pos.Y < 0) direction = Direction.UP;
                else if (next.Y - pos.Y > 0) direction = Direction.DOWN;
                else if (next.X - pos.X < 0) direction = Direction.LEFT;
                else if (next.X - pos.X > 0) direction = Direction.RIGHT;
                pos = next;
                steps++;
            }
        }

        private Point GetNextPosition(Point current, Direction direction, char[][] map)
        {
            Point next = new Point();
            char cur = map[current.Y][current.X];
            if (cur != '+')
            {
                if (direction == Direction.DOWN) 
                    next = new Point(current.X, current.Y + 1);
                else if (direction == Direction.UP) 
                    next = new Point(current.X, current.Y - 1);
                else if (direction == Direction.LEFT) 
                    next = new Point(current.X - 1, current.Y);
                else if (direction == Direction.RIGHT) 
                    next = new Point(current.X + 1, current.Y);
            }
            else 
            {
                if (direction != Direction.DOWN &&
                    current.Y - 1 >= 0 &&
                    map[current.Y - 1][current.X] != ' ')
                    next = new Point(current.X, current.Y - 1);
                else if (direction != Direction.UP && 
                         current.Y + 1 < map.Length &&
                         map[current.Y + 1][current.X] != ' ')
                    next = new Point(current.X, current.Y + 1);
                else if (direction != Direction.LEFT &&
                         current.X + 1 < map[current.Y].Length &&
                         map[current.Y][current.X + 1] != ' ')
                    next = new Point(current.X + 1, current.Y);
                else if (direction != Direction.RIGHT &&
                         current.X - 1 >= 0 &&
                         map[current.Y][current.X - 1] != ' ')
                    next = new Point(current.X - 1, current.Y);
            }

            return next;
        }
    }
}
