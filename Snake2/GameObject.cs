namespace Snake2
{
    using System;

    public abstract class GameObject : Position
    {
        private char body;
        private ConsoleColor color;

        protected GameObject(int x, int y)
            : base(x, y)
        {
            this.X = x;
            this.Y = y;
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
