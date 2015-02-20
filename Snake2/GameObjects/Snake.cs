namespace Snake2.GameObjects
{
    using System;
    using System.Collections.Generic;

    using Snake2.Core;

    public class Snake : GameObject
    {
        private const ConsoleColor DefaultBodyColor = ConsoleColor.Green;

        public Snake(int x, int y)
            : base(x, y)
        {
            this.Elements = new Queue<Position>();
            this.Color = DefaultBodyColor;
        }
        
        public Queue<Position> Elements { get; set; }

        public void AddStartingElements(int totalElementsCount)
        {
            for (int i = 0; i < totalElementsCount; i++)
            {
                this.Elements.Enqueue(new Position(i, 0));
            }
        }

        public override void Draw()
        {
            foreach (var snakeElement in this.Elements)
            {
                Console.SetCursorPosition(snakeElement.X, snakeElement.Y);
                Console.ForegroundColor = this.Color;
                Console.Write("*");
            }
        }
    }
}
