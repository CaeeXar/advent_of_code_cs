namespace AOC2017.Day14
{
    class Point
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Point))
                return false;
            else
                return this.X == ((Point)obj).X && this.Y == ((Point)obj).Y;
        }

        public override int GetHashCode()
        {
            return (this.X + this.Y).GetHashCode();
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public override string ToString()
        {
            return $"({this.X}, {this.Y})";
        }
    }
}
