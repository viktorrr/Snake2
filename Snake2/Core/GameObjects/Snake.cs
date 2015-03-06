namespace Snake2.Core.GameObjects
{
    using System;
    using System.Collections.Generic;

    using Snake2.Core;
    using Snake2.Core.Interfaces;

    public class Snake : GameObject, IMoveableGameObject
    {
        private const ConsoleColor DefaultBodyColor = ConsoleColor.Green;
        private const char DefaultBodyValue = '*';

        public Snake(Position position)
        {
            this.Color = DefaultBodyColor;

            position.Value = DefaultBodyValue;

            this.Position = new Queue<Position>();
            this.Position.Enqueue(position);

            this.AddStartingElements(2);
        }

        public int MovementSpeed { get; set; }

        public void AddStartingElements(int totalElementsCount)
        {
            for (int i = 0; i < totalElementsCount; i++)
            {
                this.Position.Enqueue(new Position(i, 0, DefaultBodyValue));
            }
        }

        public override void Draw()
        {
            Console.ForegroundColor = this.Color;

            foreach (var snakeElement in this.Position)
            {
                Console.SetCursorPosition(snakeElement.X, snakeElement.Y);
                Console.Write(snakeElement.Value);
            }
        }
    }
}
