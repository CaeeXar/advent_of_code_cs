namespace AOC2017.Day11
{
    internal class Day11
    {
        string path = @"2017\day11\Input.txt";
        private enum Direction
        {
            N,
            NE,
            SE,
            S,
            SW,
            NW,
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
            string[] steps = ReadFile().Split(",").Select(d => d.Trim()).ToArray();
            Point start = new Point();
            start.X = 0;
            start.Y = 0;

            Point child = new Point();
            foreach (string step in steps)
            {
                Direction dir;
                Enum.TryParse(step.ToUpper(), out dir);
                
                switch(dir)
                {
                    case Direction.N:
                        child.Y += 2;
                        break;
                    case Direction.NE:
                        child.X += 1;
                        child.Y += 1;
                        break;
                    case Direction.SE:
                        child.X += 1;
                        child.Y -= 1;
                        break;
                    case Direction.S:
                        child.Y -= 2;
                        break;
                    case Direction.SW:
                        child.X -= 1;
                        child.Y -= 1;
                        break;
                    case Direction.NW:
                        child.X -= 1;
                        child.Y += 1;
                        break;
                    default:
                        throw new NotSupportedException("Invalid direction.");
                }
            }

            return (int)HexgridDistance(start, child);
        }

        public int Part2()
        {
            string[] steps = ReadFile().Split(",").Select(d => d.Trim()).ToArray();
            Point start = new Point();
            start.X = 0;
            start.Y = 0;

            Point child = new Point();
            int furthest = int.MinValue;
            foreach (string step in steps)
            {
                Direction dir;
                Enum.TryParse(step.ToUpper(), out dir);

                switch (dir)
                {
                    case Direction.N:
                        child.Y += 2;
                        break;
                    case Direction.NE:
                        child.X += 1;
                        child.Y += 1;
                        break;
                    case Direction.SE:
                        child.X += 1;
                        child.Y -= 1;
                        break;
                    case Direction.S:
                        child.Y -= 2;
                        break;
                    case Direction.SW:
                        child.X -= 1;
                        child.Y -= 1;
                        break;
                    case Direction.NW:
                        child.X -= 1;
                        child.Y += 1;
                        break;
                    default:
                        throw new NotSupportedException("Invalid direction.");
                }

                int d = (int)HexgridDistance(start, child);
                if (furthest < d)
                {
                    furthest = d;
                }
                
            }

            return furthest;
        }

        private double HexgridDistance(Point p1, Point p2)
        {
            double dx = (Math.Abs(p1.X - p2.X) / 2);
            double dy = (Math.Abs(p1.Y - p2.Y) / 2);
            return Math.Round(dx + dy); 
        }

        private struct Point
        {
            public double X { get; set; }
            public double Y { get; set; }
            public override string ToString()
            {
                return $"({this.X}, {this.Y})";
            }
        }
    }

}
