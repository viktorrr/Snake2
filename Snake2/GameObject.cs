namespace Snake2
{
    using System;

    public abstract class GameObject
    {
        private int x;
        private int y;

        private char body;
        private ConsoleColor color;

        protected GameObject(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public char Body
        {
            get { return this.body; }
            set { this.body = value; }
        }

        public ConsoleColor Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public void ChangeCoordinations(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public abstract void Draw();
    }
}
