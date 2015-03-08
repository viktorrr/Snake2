namespace Snake2.Core.GameObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Snake2.Core;

    public class Apple : GameObject
    {
        private const char DefaultBodyValue = 'o';
        private const ConsoleColor DefaultBodyColor = ConsoleColor.Yellow;
        private const int DefaultTimer = 195;

        public Apple(Position position)
        {
            this.IsEaten = false;

            this.Color = DefaultBodyColor;
            this.Timer = DefaultTimer;

            position.Value = DefaultBodyValue;

            this.Position = new Queue<Position>();
            this.Position.Enqueue(position);
        }

        public bool IsEaten { get; set; }

        public int Timer { get; set; }

        public void ResetTimer()
        {
            this.Timer = 50;
        }
    }
}
