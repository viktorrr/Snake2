namespace Snake2.Core
{
    using System;
    using System.Collections.Generic;

    using Snake2.Core.Interfaces;

    public abstract class GameObject : IGameObject
    {
        public Queue<Position> Position { get; set; }
        
        public ConsoleColor Color { get; set; }

        public void Draw()
        {
            Console.ForegroundColor = this.Color;

            foreach (var position in this.Position)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write(position.Value);
            }
        }
    }
}
