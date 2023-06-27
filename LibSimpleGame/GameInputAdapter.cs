namespace LibSimpleGame
{
    public abstract class GameInputAdapter : IGameInput, ILinkCollectionItem<GameInput>
    {
        public GameInput? Input { get; internal set; }

        public abstract void Start();
        public abstract void Update();
        public abstract void Stop();

        public abstract float GetAxis(string axisName);
        public abstract float GetAxisRaw(string axisName);

        public abstract bool GetButton(string buttonName);
        public abstract bool GetButtonDown(string buttonName);
        public abstract bool GetButtonUp(string buttonName);

        public abstract Point GetCursorPosition();

        GameInput? ILinkCollectionItem<GameInput>.Owner { get => Input; set => Input = value ?? throw new ArgumentNullException(nameof(value)); }
    }
}
