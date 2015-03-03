﻿namespace Snake2.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Snake2.Core.GameObjects;

    public class Game
    {
        private const byte RightMovementDirection = 0;
        private const byte LeftMovementDirection = 1;
        private const byte UpMovementDirection = 2;
        private const byte DownMovementDirection = 3;

        private readonly Random randomGenerator = new Random();

        private readonly Position[] directions =
            {
                new Position(1, 0),     // 0: right -->
                new Position(-1, 0),    // 1: left <--
                new Position(0, -1),    // 2: up ^
                new Position(0, 1)      // 3: down v
            };

        private byte currentSnakeMovementDirection = RightMovementDirection;
        private byte lastSnakeMovementDirection = RightMovementDirection;

        private Position nextPosition;

        public Game()
        {
            this.IsOver = false;
            this.Score = 0;
            this.Speed = 80;

            this.LoadSettings();

            this.Rocks = new List<Rock>();
            this.AddRocks(10);

            this.Snake = new Snake(new Position(1, 1)); // 1,1 - default start position, top left

            this.Apple = this.CreateGameObject(GameObjectTypes.Apple) as Apple;
        }

        private int Speed { get; set; }

        private int Score { get; set; }

        private bool IsOver { get; set; }

        private Snake Snake { get; set; }

        private Apple Apple { get; set; }

        private List<Rock> Rocks { get; set; }

        public void GameStart()
        {
            while (!this.IsOver)
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
                if (this.Apple.Timer <= 0)
                {
                    this.Apple = this.CreateGameObject(GameObjectTypes.Apple) as Apple;
                }

                this.Apple.Draw();

                this.nextPosition = this.directions[this.currentSnakeMovementDirection];

                var newSnakeHead = this.GenerateNewSnakeHead();

                // check if the new snake is eating itself
                if (this.Snake.Position.Any(s => s.X == newSnakeHead.X && s.Y == newSnakeHead.Y))
                {
                    break;
                }

                this.Snake.Position.Enqueue(newSnakeHead);
                this.Snake.Position.Dequeue(); // remove the last "head"

                this.Snake.Draw();

                // check if the new snake is hitting a rock
                if (this.Rocks.Exists(r => r.Position.First().X == newSnakeHead.X
                                       && r.Position.First().Y == newSnakeHead.Y))
                {
                    break;
                }

                if (this.IsSnakeEatingApple())
                {
                    // spawn a new apple and reset the timer
                    this.Apple = this.CreateGameObject(GameObjectTypes.Apple) as Apple;

                    // the snake is getting fat
                    this.Snake.Position.Enqueue(newSnakeHead);

                    // accelerate the game a bit
                    if (this.Speed > 50)
                    {
                        this.Speed = this.Speed - 5;
                    }

                    // update the score
                    this.Score++;

                    // spawn a new rock every third rock
                    if (this.Score % 3 == 0)
                    {
                        this.Rocks.Add(this.CreateGameObject(GameObjectTypes.Rock) as Rock);
                    }
                }
                
                foreach (var rock in this.Rocks)
                {
                    rock.Draw();
                }

                Thread.Sleep(this.Speed);
            }

            this.PrintEndGameMessage(this.Score);
        }

        private GameObject CreateGameObject(GameObjectTypes type)
        {
            int randomX = this.randomGenerator.Next(0, Console.WindowWidth - 1);
            int randomY = this.randomGenerator.Next(0, Console.WindowHeight - 1);

            var position = new Position(randomX, randomY);

            switch (type)
            {
                case GameObjectTypes.Apple:
                    return new Apple(position);
                case GameObjectTypes.Rock:
                    return new Rock(position);
               default:
                    throw new ArgumentException("Cannot create game object - unknown type");
            }
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
                this.Rocks.Add(this.CreateGameObject(GameObjectTypes.Rock) as Rock);
            }
        }

        private Position GenerateNewSnakeHead()
        {
            // get latest element (the head) from Queue
            var snakeHead = this.Snake.Position.Last();

            var result = new Position(
                    snakeHead.X + this.nextPosition.X,
                    snakeHead.Y + this.nextPosition.Y,
                    '*'); // Default symbol for the snake's body

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

        private bool IsSnakeEatingApple()
        {
            var newSnakeHead = this.Snake.Position.Peek();

            if (newSnakeHead.X == this.Apple.Position.Last().X
                && newSnakeHead.Y == this.Apple.Position.First().Y)
            {
                this.Apple.IsEaten = true;
            }

            return this.Apple.IsEaten;
        }

        private void PrintEndGameMessage(int gameScore)
        {
            const string EndGameMessage = "Game over! Total Score: ";

            Console.Clear();

            int consoleCenterX = (Console.WindowWidth - EndGameMessage.Length) / 2;
            int consoleCenterY = (Console.WindowHeight / 2) - 2;

            Console.SetCursorPosition(consoleCenterX, consoleCenterY);

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("Game over! Total Score: {0}", gameScore);
        }
        
        private void LoadSettings()
        {
            Console.WindowWidth = 100;
            Console.WindowHeight = 40;

            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;

            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
