namespace LibSimpleGame
{
    public class GameRenderer : IGameRenderer
    {
        public Game Game { get; }

        public GameRendererAdapterCollection Adapters { get; }

        internal GameRenderer(Game game)
        {
            Game = game;
            Adapters = new GameRendererAdapterCollection(this);
        }


        public void Start()
        {
            foreach (var adapter in Adapters)
                adapter.Start();
        }

        public void Render()
        {
            foreach (var adapter in Adapters)
                adapter.Render();
        }

        public void Stop()
        {
            foreach (var adapter in Adapters)
                adapter.Stop();
        }

        public TAdapter? GetAdapter<TAdapter>() where TAdapter : GameRenderAdapter
        {
            return Adapters.OfType<TAdapter>().FirstOrDefault();
        }

        public TAdapter? GetRequiredAdapter<TAdapter>() where TAdapter : GameRenderAdapter
        {
            return GetAdapter<TAdapter>() ?? throw new InvalidOperationException("No adapter found");
        }

        public class GameRendererAdapterCollection : LinkCollection<GameRenderer, GameRenderAdapter>
        {
            public GameRendererAdapterCollection(GameRenderer owner) : base(owner)
            {
            }
        }
    }
}
