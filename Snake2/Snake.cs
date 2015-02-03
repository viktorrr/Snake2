namespace Snake2
{
    using System;
    using System.Collections.Generic;

    public class Snake : GameObject
    {
        private const ConsoleColor DefaultBodyColor = ConsoleColor.Green;

        public Snake(int x, int y)
            : base(x, y)
        {
            this.Elements = new Queue<Position>();
            this.Color = DefaultBodyColor;
        }

        public new ConsoleColor Color { get; set; }

        public Queue<Position> Elements { get; set; }

        public void AddStartingElements(int totalElementsCount)
        {
            for (int i = 0; i < totalElementsCount; i++)
            {
                this.Elements.Enqueue(new Position(i, 0));
            }
        }

        public void Draw(Position newSnakeHead, ref bool isGameFinished, List<Rock> rocks)
        {
            var tempDrawingSnakeQueue = new Queue<Position>();

            foreach (var snakeElement in this.Elements)
            {
                if (snakeElement.X == newSnakeHead.X && snakeElement.Y == newSnakeHead.Y)
                {
                    isGameFinished = true;
                }

                if (rocks.Exists(rock => rock.X == newSnakeHead.X && rock.Y == newSnakeHead.Y))
                {
                    isGameFinished = true;
                }

                tempDrawingSnakeQueue.Enqueue(snakeElement);
            }

            tempDrawingSnakeQueue.Enqueue(newSnakeHead);
            tempDrawingSnakeQueue.Dequeue();

            this.Elements = tempDrawingSnakeQueue;

            foreach (var snakeElement in this.Elements)
            {
                Console.SetCursorPosition(snakeElement.X, snakeElement.Y);
                Console.ForegroundColor = this.Color;
                Console.Write("*");
            }
        }
    }
}
