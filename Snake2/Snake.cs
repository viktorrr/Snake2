namespace Snake2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Snake : GameObject
    {
        private Queue<Position> elements;
        private ConsoleColor color;

        public Snake(int x, int y)
            : base(x, y)
        {
            this.elements = new Queue<Position>();
            this.Color = ConsoleColor.Green;
        }

        public ConsoleColor Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public Queue<Position> Elements
        {
            get { return this.elements; }
            set { this.elements = value; }
        }

        public override void Draw()
        {
        }
        
        public void AddStartingElements(int totalElementsCount)
        {
            for (int i = 0; i < totalElementsCount; i++)
            {
                this.elements.Enqueue(new Position(i, 0));
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

                foreach (var rock in rocks.Where(rock => newSnakeHead.X == rock.X && newSnakeHead.Y == rock.Y))
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
