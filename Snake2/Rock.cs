namespace Snake2
{
    using System;

    public class Rock : GameObject
    {
        public Rock(int x, int y) : base(x, y)
        {
            this.Body = 'x';
            this.Color = ConsoleColor.Red;
        }

        public override void Draw()
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.ForegroundColor = this.Color;
            Console.Write(this.Body);
        }
    }
}
