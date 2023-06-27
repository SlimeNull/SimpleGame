namespace LibSimpleGame.Console
{
    public class ConsoleImage
    {
        ConsoleUnit[,] buffer;

        public int Width { get; }
        public int Height { get; }
        public Size Size { get; }

        public ConsoleImage(int width, int height)
        {
            Width = width;
            Height = height;
            Size = new Size(width, height);
            buffer = new ConsoleUnit[width, height];
        }

        public void Set(Point position, char character, Color foreground, Color background)
        {
            if (character > 255 ||
                character < 0)
                throw new ArgumentOutOfRangeException(nameof(character));

            buffer[position.X, position.Y] = new ConsoleUnit(character, foreground, background);
        }

        public void Get(Point position, out char character, out Color foreground, out Color background)
        {
            var unit = buffer[position.X, position.Y];

            character = unit.Character;
            foreground = unit.Foreground;
            background = unit.Background;
        }

        public static ConsoleImage CreateEmpty() => new ConsoleImage(0, 0);
    }
}