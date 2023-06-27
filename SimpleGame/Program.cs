using System.ComponentModel;
using LibSimpleGame.Console;

namespace LibSimpleGame
{

    internal class Program
    {
        public static ConsoleImage SpriteImage = new ConsoleImage(1, 1);

        static void Main(string[] args)
        {
            int width = 100;
            int height = 30;

            SpriteImage.Set(new Point(0, 0), 'M', Color.White, new Color(211, 237, 253));

            Game game = new Game()
            {
                Objects =
                {
                    new GameObject()
                    {
                        Components =
                        {
                            new InfoShow()
                        }
                    },
                    new GameObject()
                    {
                        Components =
                        {
                            new ConsoleSprite(SpriteImage),
                            new ConsoleSpriteRenderer(),
                            new MoveFollowCursor()
                        }
                    },
                    new GameObject()
                    {
                        Components =
                        {
                            new ConsoleSprite(SpriteImage),
                            new ConsoleSpriteRenderer(),
                            new RepeatMoving()
                        } 
                        
                    }
                }
            };

            game.AddConsoleAdapters(adapters =>
            {
                adapters.RenderAdapter.Resize(width, height);
            });

            game.Start();

            System.Console.CursorVisible = false;
            System.Console.SetWindowSize(width + 1, height + 1);
            System.Console.SetBufferSize(width + 1, height + 1);


            while (true)
            {
                Thread.Sleep(1);
            }
        }
    }

    class InfoShow : GameComponent
    {
        Timer? timer;
        int frameRenderCount;

        public override void Start()
        {
            timer = new Timer(state =>
            {
                System.Console.Title = $"FPS: {frameRenderCount / 0.5:00000}";
                frameRenderCount = 0;
            }, null, 0, 500);
        }

        public override void Update()
        {
            frameRenderCount++;
        }

        public override void Stop()
        {
            timer?.Dispose();
            timer = null;
        }
    }

    class MoveFollowCursor : GameComponent
    {
        public override void Update()
        {
            GameObject.Position = Game.Input.GetCursorPosition();
        }
    }

    class RepeatMoving : GameComponent
    {

        int i = 0;

        public override void Update()
        {
            if (i >= 10)
                i = 0;

            GameObject.Position = GameObject.Position with
            {
                X = i
            };

            i++;
        }
    }
}