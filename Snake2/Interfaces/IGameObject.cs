namespace Snake2.Interfaces
{
    using System;

    public interface IGameObject
    {
        int X { get; set; }

        int Y { get; set; }

        char Body { get; set; }

        ConsoleColor Color { get; set; }

        void Draw();
    }
}
