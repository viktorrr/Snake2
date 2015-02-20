namespace Snake2.Core
{
    using System;

    using Snake2.Interfaces;

    public abstract class GameObject : IGameObject
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

        public abstract void Draw();

        public void ChangeCoordinations(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
