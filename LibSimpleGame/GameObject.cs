namespace LibSimpleGame
{
    public partial class GameObject : ILinkCollectionItem<Game>
    {
        public string? Name { get; set; }
        public Game? Owner { get; internal set; }

        public Game Game => Owner ?? throw new InvalidOperationException("Game is null");
        public GameComponentCollection Components { get; }

        public PointF Position { get; set; }
        public PointF Scale { get; set; }

        public GameObject()
        {
            Components = new GameComponentCollection(this);
        }

        public TComponent? GetComponent<TComponent>()
        {
            return Components.OfType<TComponent>().FirstOrDefault();
        }

        public TComponent GetRequiredComponent<TComponent>()
        {
            return Components.OfType<TComponent>().FirstOrDefault() ??
                throw new InvalidOperationException("No component with specified type in game object");
        }

        public void AddComponent(GameComponent component)
        {
            Components.Add(component);
        }

        public void DestroySelf()
        {
            Owner?.DestroyObject(this);
        }

        Game? ILinkCollectionItem<Game>.Owner { get => Owner; set => Owner = value; }
    }
}
