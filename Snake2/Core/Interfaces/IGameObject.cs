namespace Snake2.Core.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IGameObject
    {
        Queue<Position> Position { get; set; }

        ConsoleColor Color { get; set; }

        void Draw();
    }
}
