namespace Snake2.Core
{
    using System;
    using System.Collections.Generic;

    using Snake2.Core.Interfaces;

    public abstract class GameObject : IGameObject
    {
        public Queue<Position> Position { get; set; }
        
        public ConsoleColor Color { get; set; }

        public abstract void Draw();
    }
}
