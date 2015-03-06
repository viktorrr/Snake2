namespace Snake2
{
    using Snake2.Core;

    public static class Program
    {
        public static void Main()
        {
            var snakeGame = new Game();

            snakeGame.Start();
            snakeGame.PrintEndGameMessage();
        }
    }
}
