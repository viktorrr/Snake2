namespace Snake2.GameObjects
{
    using System;

    using Snake2.Core;

    public class Apple : GameObject
    {
        private const char DefaultBodyValue = 'o';
        private const ConsoleColor DefaultBodyColor = ConsoleColor.Yellow;
        private const int DefaultTimer = 195;

        public Apple(int x, int y)
            : base(x, y)
        {
            this.Body = DefaultBodyValue;
            this.Color = DefaultBodyColor;
            this.Timer = DefaultTimer;
            this.IsEaten = false;
        }

        public bool IsEaten { get; set; }

        public int Timer { get; set; }

        public void ResetTimer()
        {
            this.Timer = 50;
        }

        public override void Draw()
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.ForegroundColor = this.Color;
            Console.Write(this.Body);
            this.Timer--;
        }
    }
}
