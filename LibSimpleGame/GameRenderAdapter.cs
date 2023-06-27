namespace LibSimpleGame
{
    public abstract class GameRenderAdapter : IGameRenderer, ILinkCollectionItem<GameRenderer>
    {
        public GameRenderer? Renderer { get; internal set; }

        public abstract void Start();
        public abstract void Render();
        public abstract void Stop();

        GameRenderer? ILinkCollectionItem<GameRenderer>.Owner { get => Renderer; set => Renderer = value ?? throw new ArgumentNullException(nameof(value)); }
    }
}
