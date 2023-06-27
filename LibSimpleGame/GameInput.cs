namespace LibSimpleGame
{
    public class GameInput
    {
        public Game Game { get; }
        public GameInputAdapterCollection Adapters { get; }

        internal GameInput(Game game)
        {
            Game = game;
            Adapters = new GameInputAdapterCollection(this);
        }


        public float GetAxis(string axisName)
        {
            foreach (var adapter in Adapters)
            {
                var value = adapter.GetAxis(axisName);
                if (value != 0)
                    return value;
            }

            return 0;
        }


        public float GetAxisRaw(string axisName)
        {
            foreach (var adapter in Adapters)
            {
                var value = adapter.GetAxisRaw(axisName);
                if (value != 0)
                    return value;
            }

            return 0;
        }

        public bool GetButton(string buttonName)
        {
            foreach (var adapter in Adapters)
            {
                var value = adapter.GetButton(buttonName);
                if (value)
                    return true;
            }

            return false;
        }

        public bool GetButtonDown(string buttonName)
        {
            foreach (var adapter in Adapters)
            {
                var value = adapter.GetButtonDown(buttonName);
                if (value)
                    return true;
            }

            return false;
        }

        public bool GetButtonUp(string buttonName)
        {
            foreach (var adapter in Adapters)
            {
                var value = adapter.GetButtonUp(buttonName);
                if (value)
                    return true;
            }

            return false;
        }

        public Point GetCursorPosition()
        {
            foreach (var adapter in Adapters)
                return adapter.GetCursorPosition();

            return Point.Empty;
        }

        public class GameInputAdapterCollection : LinkCollection<GameInput, GameInputAdapter>
        {
            public GameInputAdapterCollection(GameInput owner) : base(owner)
            {
            }
        }
    }
}
