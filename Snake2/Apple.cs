namespace Snake2
{
    using System;

    public class Apple : GameObject
    {
        private bool isEaten;
        private int timer;

        public Apple(int x, int y)
            : base(x, y)
        {
            this.Body = 'o';
            this.Color = ConsoleColor.Yellow;
            this.IsEaten = false;
            this.Timer = 180;
        }

        public bool IsEaten
        {
            get { return this.isEaten; }
            set { this.isEaten = value; }
        }

        public int Timer
        {
            get { return this.timer; }
            set { this.timer = value; }
        }

        public void ResetTimer()
        {
            this.Timer = 50;
        }

        public override void Draw()
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.ForegroundColor = this.Color;
            Console.Write(this.Body);
        }
    }
}
