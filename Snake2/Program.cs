namespace Snake2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public static class Program
    {
        private static readonly Random RandomGenerator = new Random();

        public static void Main()
        {
            LoadSettings();
            GameStart();
        }

        public static void GameStart()
        {
            var gameSpeed = 80;
            bool isGameFinished = false;

            Position[] directions =
            {
                new Position(1, 0),     // 0: right -->
                new Position(-1, 0),    // 1: left <--
                new Position(0, -1),     // 2: up ^
                new Position(0, 1)     // 3: down v
            };

            const byte Right = 0;
            const byte Left = 1;
            const byte Up = 2;
            const byte Down = 3;

            var currentDirection = Right;

            var snake = new Snake(1, 1);
            snake.AddStartingElements(2);

            var apple = new Apple(
                RandomGenerator.Next(0, Console.WindowWidth - 1),
                RandomGenerator.Next(0, Console.WindowHeight - 1));

            var rocks = new List<Rock>();
            rocks.Fall(5);

            var gameScore = 0;

            while (!isGameFinished)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.LeftArrow:
                            currentDirection = Left;
                            break;
                        case ConsoleKey.RightArrow:
                            currentDirection = Right;
                            break;
                        case ConsoleKey.DownArrow:
                            currentDirection = Down;
                            break;
                        case ConsoleKey.UpArrow:
                            currentDirection = Up;
                            break;
                    }
                }

                var nextPosition = directions[currentDirection];

                Console.Clear();

                // get latest element (the head) from Queue
                var snakeHead = snake.Elements.Last(); 

                // check if the head's next position will hit something
                var newSnakeHead = new Position(snakeHead.X + nextPosition.X, snakeHead.Y + nextPosition.Y);

                if (newSnakeHead.X < 0)
                {
                    newSnakeHead.X = Console.WindowWidth - 1;
                }

                if (newSnakeHead.Y < 0)
                {
                    newSnakeHead.Y = Console.WindowHeight - 1;
                }

                if (newSnakeHead.Y >= Console.WindowHeight)
                {
                    newSnakeHead.Y = 0;
                }

                if (newSnakeHead.X >= Console.WindowWidth)
                {
                    newSnakeHead.X = 0;
                }

                if (apple.Timer <= 0)
                {
                    apple.ChangeCoordinations(
                        RandomGenerator.Next(0, Console.WindowWidth - 1),
                        RandomGenerator.Next(0, Console.WindowHeight - 1));
                    apple.ResetTimer();
                }

                if (newSnakeHead.X == apple.X && newSnakeHead.Y == apple.Y)
                {
                    gameScore++;
                    apple.ChangeCoordinations(
                        RandomGenerator.Next(0, Console.WindowWidth - 1),
                        RandomGenerator.Next(0, Console.WindowHeight - 1));
                    if (gameScore % 3 == 0)
                    {
                        rocks.Add(new Rock(
                            RandomGenerator.Next(0, Console.WindowWidth - 1),
                            RandomGenerator.Next(0, Console.WindowHeight - 1)));
                    }

                    apple.IsEaten = true;
                    apple.ResetTimer();

                    if (gameSpeed > 50)
                    {
                        gameSpeed = gameSpeed - 5;
                    }
                }

                var tempQ = new Queue<Position>();

                foreach (var snakeElement in snake.Elements)
                {
                    if (snakeElement.X == newSnakeHead.X && snakeElement.Y == newSnakeHead.Y)
                    {
                        isGameFinished = true;
                    }

                    foreach (var rock in rocks.Where(rock => newSnakeHead.X == rock.X && newSnakeHead.Y == rock.Y))
                    {
                        isGameFinished = true;
                    }

                    tempQ.Enqueue(snakeElement);
                }

                tempQ.Enqueue(newSnakeHead);
                tempQ.Dequeue();

                snake.Elements = tempQ;

                foreach (var snakeElement in snake.Elements)
                {
                    Console.SetCursorPosition(snakeElement.X, snakeElement.Y);
                    Console.ForegroundColor = snake.Color;
                    Console.Write("*");
                }

                if (apple.IsEaten)
                {
                    snake.Elements.Enqueue(newSnakeHead);
                    Console.SetCursorPosition(newSnakeHead.X, newSnakeHead.Y);
                    apple.IsEaten = false;
                }

                apple.Draw();
                apple.Timer--;

                foreach (var rock in rocks)
                {
                    rock.Draw();
                }

                Thread.Sleep(gameSpeed);
            }

            PrintEndGameMessage(gameScore);
        }

        public static void Fall(this List<Rock> rocks, int count)
        {
            for (int i = 0; i < count; i++)
            {
                rocks.Add(new Rock(
                        RandomGenerator.Next(0, Console.WindowWidth - 1),
                        RandomGenerator.Next(0, Console.WindowHeight - 1)));
            }
        }

        private static void PrintEndGameMessage(int gameScore)
        {
            const string EndGameMessage = "Game over! Total Score: ";

            Console.Clear();
            Console.SetCursorPosition(
                (Console.WindowWidth - EndGameMessage.Length) / 2,
                (Console.WindowHeight / 2) - 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game over! Total Score: {0}", gameScore);
            do
            {
                Thread.Sleep(5);
            } 
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        private static void LoadSettings()
        {
            Console.WindowWidth = 100;
            Console.WindowHeight = 40;

            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;

            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
