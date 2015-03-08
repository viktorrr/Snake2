namespace Snake2.Core.GameObjects
{
    using System;
    using System.Collections.Generic;

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
    }
}
