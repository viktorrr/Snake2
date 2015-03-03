namespace Snake2.Core.GameObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Snake2.Core;

    public class Rock : GameObject
    {
        private const char DefaultBodyValue = 'x';
        private const ConsoleColor DefaultBodyColor = ConsoleColor.Red;

        public Rock(Position position)
        {
            this.Color = DefaultBodyColor;
            position.Value = DefaultBodyValue;

            this.Position = new Queue<Position>();
            this.Position.Enqueue(position);
        }
        
        public override void Draw()
        {
            Console.SetCursorPosition(this.Position.First().X, this.Position.First().Y);
            Console.ForegroundColor = this.Color;
            Console.Write(this.Position.First().Value);
        }
    }
}
