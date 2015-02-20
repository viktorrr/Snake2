namespace Snake2.GameObjects
{
    using System;

    using Snake2.Core;

    public class Rock : GameObject
    {
        private const char DefaultBodyValue = 'x';
        private const ConsoleColor DefaultBodyColor = ConsoleColor.Red;

        public Rock(int x, int y) : base(x, y)
        {
            this.Body = DefaultBodyValue;
            this.Color = DefaultBodyColor;
        }

        public override void Draw()
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.ForegroundColor = this.Color;
            Console.Write(this.Body);
        }
    }
}
