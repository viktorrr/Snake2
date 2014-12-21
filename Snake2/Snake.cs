
namespace Snake2
{
    using System;
    using System.Collections.Generic;

    public class Snake
    {
        private int x;
        private int y;
        private Queue<Position> elements = new Queue<Position>();
        private ConsoleColor color = ConsoleColor.Green;
        public void AddStartingElements(int totalElementsCount)
        {
            for (int i = 0; i < totalElementsCount; i++)
            {
                elements.Enqueue(new Position(i, 0));
            }
        }

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public ConsoleColor Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public Snake(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Queue<Position> Elements
        {
            get { return this.elements; }
            set { this.elements = value; }
        }
    }
}
