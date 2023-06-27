namespace LibSimpleGame.Console
{

    public class ConsoleSprite : GameComponent
    {
        private ConsoleImage image;

        public ConsoleImage Image 
        {
            get => image; 
            set => image = value ?? throw new ArgumentNullException(nameof(value)); 
        }

        public Point CenterPoint { get; set; }

        public ConsoleSprite() : this(ConsoleImage.CreateEmpty(), Point.Empty)
        { }

        public ConsoleSprite(ConsoleImage image) : this(image, Point.Empty)
        { }

        public ConsoleSprite(ConsoleImage image, Point centerPoint)
        {
            this.image = image;
            CenterPoint = centerPoint;
        }
    }

    public class ConsoleText : GameComponent
    {

    }
}