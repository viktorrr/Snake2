namespace Snake2.Core
{
    public class Position
    {
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Position(int x, int y, char value) : this(x, y)
        {
            this.Value = value;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public char Value { get; set; }
    }
}
