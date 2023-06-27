using LibSimpleGame.Console;

namespace LibSimpleGame
{

    internal class Program
    {
        public static ConsoleImage SpriteImage = new ConsoleImage(10, 10);

        static void Main(string[] args)
        {
            for (int x = 0; x < 10; x++)
            {
                SpriteImage.Set(new Point(x, 0), 'M', Color.White, new Color(211, 237, 253));
                SpriteImage.Set(new Point(x, 9), 'M', Color.White, new Color(211, 237, 253));
            }

            Game game = new Game()
            {
                Objects =
                {
                    new GameObject()
                    {
                        Components =
                        {
                            new ConsoleRenderer(),
                            new ConsoleSprite(SpriteImage),
                            new ConsoleSpriteRenderer(),
                            new MoveFollowCursor(),
                        }
                    }
                }
            };

            game.Input.AddConsoleInputAdapter();

            game.Start();
            System.Console.CursorVisible = false;
            while (true)
            {
                System.Console.ReadKey();
            }
        }
    }

    class MoveFollowCursor : GameComponent
    {
        public override void Update()
        {
            GameObject.Position += new SizeF(
                Game.Input.GetAxisRaw("Horizontal"),
                Game.Input.GetAxisRaw("Vertical")) * (float)Game.Time.Delta.TotalSeconds * 10;

            System.Console.Title = $"Position: {GameObject.Position} X Axis: {Game.Input.GetAxis("Horizontal")}, Delta time: {Game.Time.Delta}";
        }
    }
}