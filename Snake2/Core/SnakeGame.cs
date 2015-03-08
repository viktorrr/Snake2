namespace Snake2.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Snake2.Core.GameObjects;
    using Snake2.Core.Interfaces;

    public class SnakeGame : ConsoleGame
    {
        private const byte LeftMovementDirection = 1;

        private const byte RightMovementDirection = 0;

        private const byte UpMovementDirection = 2;

        private const byte DownMovementDirection = 3;

        private readonly Random randomGenerator;

        private readonly IMoveableGameObject snake;

        private readonly ICollection<Rock> rocks;
        
        private byte currentSnakeMovementDirection;

        private byte lastSnakeMovementDirection;

        private Apple apple;

        private Position nextPosition;
        
        public SnakeGame()
        {
            this.LoadSettings();

            this.randomGenerator = new Random();
            
            this.Directions = new[]
            {
                new Position(1, 0),     // 0: right -->
                new Position(-1, 0),    // 1: left <--
                new Position(0, -1),    // 2: up ^
                new Position(0, 1)      // 3: down v
            };

            this.currentSnakeMovementDirection = RightMovementDirection;

            this.lastSnakeMovementDirection = RightMovementDirection;

            this.Score = 0;

            this.rocks = new List<Rock>();
            this.AddRocks(10);

            // 1,1 - default start position, top left
            var defaultSnakePositon = new Position(1, 1);
            this.snake = new Snake(defaultSnakePositon) { MovementSpeed = 80 };

            var randomPosition = this.GenerateRandomPosition();
            this.apple = new Apple(randomPosition);
        }

        public void Play()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    this.GetLastSnakeMovementDirection();

                    // used in order to not end the game if the user tries to go the oposite direction
                    this.GetCurrentMovementDirection();
                }

                // In order to re-draw the elements
                Console.Clear();

                // if the apple isn't eaten in time, change it's coordinates and reset the timer
                if (this.apple.Timer <= 0)
                {
                    var randomPosition = this.GenerateRandomPosition();
                    this.apple = new Apple(randomPosition);
                }

                this.apple.Draw();
                this.apple.Timer--;

                this.nextPosition = this.Directions[this.currentSnakeMovementDirection];

                var newSnakeHead = this.CalculateNewSnakeStartPosition();

                // check if the new snake is eating itself
                if (this.snake.Position.Any(s => s.X == newSnakeHead.X && s.Y == newSnakeHead.Y))
                {
                    break;
                }

                this.snake.Position.Enqueue(newSnakeHead);
                this.snake.Position.Dequeue(); // remove the last "head"

                this.snake.Draw();

                // check if the new snake is hitting a rock
                if (this.rocks.Any(r => r.Position.First().X == newSnakeHead.X
                                       && r.Position.First().Y == newSnakeHead.Y))
                {
                    break;
                }

                if (this.IsSnakeEatingApple())
                {
                    // spawn a new apple and reset the timer
                    var snakeRandomPosition = this.GenerateRandomPosition();
                    this.apple = new Apple(snakeRandomPosition);

                    // the snake is getting fat
                    this.snake.Position.Enqueue(newSnakeHead);

                    // accelerate the game a bit
                    if (this.snake.MovementSpeed > 50)
                    {
                        this.snake.MovementSpeed -= 5;
                    }

                    // update the score
                    this.Score++;

                    // spawn a new rock every third rock
                    if (this.Score % 3 == 0)
                    {
                        var rockRandomPosition = this.GenerateRandomPosition();
                        this.rocks.Add(new Rock(rockRandomPosition));
                    }
                }

                foreach (var rock in this.rocks)
                {
                    rock.Draw();
                }

                Thread.Sleep(this.snake.MovementSpeed);
            }

            this.PrintEndGameMessage();
        }

        private void PrintEndGameMessage()
        {
            const string EndGameMessage = "Game over! Total Score: {0}";

            Console.Clear();

            int consoleCenterX = (Console.WindowWidth - EndGameMessage.Length) / 2;
            int consoleCenterY = (Console.WindowHeight / 2) - 2;

            Console.SetCursorPosition(consoleCenterX, consoleCenterY);

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(EndGameMessage, this.Score);
        }

        private void LoadSettings()
        {
            Console.WindowWidth = 100;
            Console.WindowHeight = 40;

            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;

            Console.ForegroundColor = ConsoleColor.Green;
        }

        private void GetCurrentMovementDirection()
        {
            switch (this.currentSnakeMovementDirection)
            {
                case UpMovementDirection:
                    this.currentSnakeMovementDirection =
                        this.lastSnakeMovementDirection == DownMovementDirection ?
                            UpMovementDirection : this.lastSnakeMovementDirection;
                    break;
                case DownMovementDirection:
                    this.currentSnakeMovementDirection =
                        this.lastSnakeMovementDirection == UpMovementDirection ?
                            DownMovementDirection : this.lastSnakeMovementDirection;
                    break;
                case RightMovementDirection:
                    this.currentSnakeMovementDirection =
                        this.lastSnakeMovementDirection == LeftMovementDirection ?
                            RightMovementDirection : this.lastSnakeMovementDirection;
                    break;
                case LeftMovementDirection:
                    this.currentSnakeMovementDirection =
                        this.lastSnakeMovementDirection == RightMovementDirection ?
                            LeftMovementDirection : this.lastSnakeMovementDirection;
                    break;
            }
        }

        private void GetLastSnakeMovementDirection()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.LeftArrow:
                    this.lastSnakeMovementDirection = LeftMovementDirection;
                    break;
                case ConsoleKey.RightArrow:
                    this.lastSnakeMovementDirection = RightMovementDirection;
                    break;
                case ConsoleKey.DownArrow:
                    this.lastSnakeMovementDirection = DownMovementDirection;
                    break;
                case ConsoleKey.UpArrow:
                    this.lastSnakeMovementDirection = UpMovementDirection;
                    break;
            }
        }

        private void AddRocks(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var rockRandomPosition = this.GenerateRandomPosition();
                this.rocks.Add(new Rock(rockRandomPosition));
            }
        }

        private bool IsSnakeEatingApple()
        {
            var newSnakeHead = this.snake.Position.Peek();

            if (newSnakeHead.X == this.apple.Position.Last().X
                && newSnakeHead.Y == this.apple.Position.First().Y)
            {
                this.apple.IsEaten = true;
            }

            return this.apple.IsEaten;
        }

        private Position CalculateNewSnakeStartPosition()
        {
            // get latest element (the head) from Queue
            var snakeHead = this.snake.Position.Last();

            int newSnakeHeadX = snakeHead.X + this.nextPosition.X;
            int newSnakeHeadY = snakeHead.Y + this.nextPosition.Y;

            var result = new Position(newSnakeHeadX, newSnakeHeadY, snakeHead.Value); 

            // check if the head's next position will hit something -
            // if it reaches a wall, start drawing from the opposite one
            if (result.X < 0)
            {
                result.X = Console.WindowWidth - 1;
            }

            if (result.Y < 0)
            {
                result.Y = Console.WindowHeight - 1;
            }

            if (result.Y >= Console.WindowHeight)
            {
                result.Y = 0;
            }

            if (result.X >= Console.WindowWidth)
            {
                result.X = 0;
            }

            return result;
        }

        private Position GenerateRandomPosition()
        {
            int randomX = this.randomGenerator.Next(0, Console.WindowWidth - 1);
            int randomY = this.randomGenerator.Next(0, Console.WindowHeight - 1);

            return new Position(randomX, randomY);
        }
    }
}
