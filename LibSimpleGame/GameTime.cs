namespace LibSimpleGame
{
    public partial class GameTime
    {
        public Game Game { get; }

        public GameTime(Game game)
        {
            Game = game;
        }

        bool stop;
        bool started;
        Timer? runningTimer;

        DateTime deltaTimeLast;
        DateTime fixedDeltaTimeLast;
        DateTime renderDeltaTimeLast;

        public TimeSpan Delta { get; private set; }
        public TimeSpan FixedDelta { get; private set; }
        public TimeSpan RenderDelta { get; private set; }

        public void Start()
        {
            stop = false;
            deltaTimeLast = fixedDeltaTimeLast = renderDeltaTimeLast = DateTime.Now;

            Game.Input.Start();
            Game.Renderer.Start();

            if (!started)
            {
                foreach (var obj in Game.Objects)
                {
                    foreach (var component in obj.Components)
                        component.Awake();
                }
            }

            // update loop
            Task.Run(() =>
            {
                foreach (var obj in Game.Objects)
                {
                    foreach (var component in obj.Components)
                        component.Start();
                }

                while (!stop)
                {
                    DateTime deltaTimeCurrent = DateTime.Now;
                    Delta = deltaTimeCurrent - deltaTimeLast;

                    Game.Input.Update();

                    foreach (var obj in Game.Objects)
                    {
                        foreach (var component in obj.Components)
                            component.Update();

                        foreach (var component in obj.Components)
                            component.LateUpdate();
                    }

                    Game.Renderer.Render();

                    deltaTimeLast = deltaTimeCurrent;
                }

                foreach (var obj in Game.Objects)
                {
                    foreach (var component in obj.Components)
                        component.Stop();
                }

                Game.Input.Stop();
                Game.Renderer.Stop();
            });

            // render loop
            //Task.Run(() =>
            //{
            //    while (!stop)
            //    {
            //        DateTime renderDeltaTimeCurrent = DateTime.Now;
            //        RenderDelta = renderDeltaTimeCurrent - renderDeltaTimeLast;

            //        foreach (var obj in Game.Objects)
            //        {
            //            foreach (var component in obj.Components)
            //                component.Render();
            //        }

            //        renderDeltaTimeLast = renderDeltaTimeCurrent;
            //    }
            //});

            // fixed update loop
            runningTimer = new Timer(state =>
            {
                DateTime fixedDeltaTimeCurrent = DateTime.Now;
                FixedDelta = fixedDeltaTimeCurrent - fixedDeltaTimeLast;

                foreach (var obj in Game.Objects)
                {
                    foreach (var component in obj.Components)
                        component.FixedUpdate();
                }

                deltaTimeLast = fixedDeltaTimeCurrent;
            }, null, 0, 1000 / 30);

            started = true;
        }

        public void Stop()
        {
            stop = true;
            runningTimer?.Dispose();

            foreach (var inputAdapter in Game.Input.Adapters)
                inputAdapter.Stop();

            Delta = FixedDelta = RenderDelta = TimeSpan.Zero;
        }
    }
}
