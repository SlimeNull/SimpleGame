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
        Timer? runningTimer;

        DateTime deltaTimeLast;
        DateTime fixedDeltaTimeLast;

        public TimeSpan Delta { get; private set; }
        public TimeSpan FixedDelta { get; private set; }

        public void Start()
        {
            stop = false;

            foreach (var inputAdapter in Game.Input.Adapters)
                inputAdapter.Start();

            foreach (var obj in Game.Objects)
            {
                foreach (var component in obj.Components)
                    component.Start();
            }

            deltaTimeLast = DateTime.Now;
            fixedDeltaTimeLast = DateTime.Now;

            // update loop
            Task.Run(() =>
            {
                while (!stop)
                {
                    DateTime deltaTimeCurrent = DateTime.Now;
                    Delta = deltaTimeCurrent - deltaTimeLast;

                    foreach (var obj in Game.Objects)
                    {
                        foreach (var component in obj.Components)
                            component.Update();

                        foreach (var component in obj.Components)
                            component.LateUpdate();
                    }

                    foreach (var inputAdapter in Game.Input.Adapters)
                        inputAdapter.Update();

                    deltaTimeLast = deltaTimeCurrent;
                }
            });

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
        }

        public void Stop()
        {
            stop = true;
            runningTimer?.Dispose();

            foreach (var inputAdapter in Game.Input.Adapters)
                inputAdapter.Stop();
        }
    }
}
