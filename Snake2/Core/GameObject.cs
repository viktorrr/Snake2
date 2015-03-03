namespace Snake2.Core
{
    using System;
    using System.Collections.Generic;
    
    public abstract class GameObject
    {
        public Queue<Position> Position { get; internal set; }
        
        public ConsoleColor Color { get; internal set; }

        public abstract void Draw();
    }
}
