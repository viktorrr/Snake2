namespace Snake2
{
    using System;

    public abstract class GameObject
    {
        protected GameObject(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public char Body { get; set; }

        public ConsoleColor Color { get; set; }

        public void ChangeCoordinations(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
