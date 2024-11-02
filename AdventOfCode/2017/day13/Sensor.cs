namespace AOC2017.Day13
{
    internal class Sensor
    {
        public int Depth { get; set; }
        public int Range { get; set; }
        public int Position { get; set; }

        private Direction direction;
        private enum Direction 
        { 
            UP,
            DOWN,
        }

        public Sensor(int depth, int range)
        {
            this.Depth = depth;
            this.Range = range;
            this.Position = 0;
            this.direction = Direction.DOWN;
        }

        public void Move()
        {
            if (this.Position == this.Range - 1)
            {
                this.direction = Direction.UP;
            }
            else if (this.Position == 0)
            {
                this.direction = Direction.DOWN;
            }

            if (this.direction == Direction.UP)
            {
                this.Position--;
            }
            else
            {
                this.Position++;
            }
        }
    }
}
