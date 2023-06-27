namespace LibSimpleGame
{
    public class GameComponent : ILinkCollectionItem<GameObject>
    {
        public GameObject? Owner { get; internal set; }

        public Game Game => GameObject.Game;
        public GameObject GameObject => Owner ?? throw new InvalidOperationException("GameObject is null");

        public virtual void Awake() { }

        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void LateUpdate() { }

        public virtual void FixedUpdate() { }

        public virtual void Stop() { }


        public virtual void OnDestroy() { }


        GameObject? ILinkCollectionItem<GameObject>.Owner { get => Owner; set => Owner = value; }
    }
}
