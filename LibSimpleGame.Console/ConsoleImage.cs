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

        public void Set(Point position, char character, Color foreground, Color background) =>
            Set(position.X, position.Y, character, foreground, background);

        public void Get(Point position, out char character, out Color foreground, out Color background) =>
            Get(position.X, position.Y, out character, out foreground, out background);

        public void Set(int x, int y, char character, Color foreground, Color background)
        {
            if (character > 255 ||
                character <= 0)
                throw new ArgumentOutOfRangeException(nameof(character));

            buffer[x, y] = new ConsoleUnit(character, foreground, background);
        }

        public void Get(int x, int y, out char character, out Color foreground, out Color background)
        {
            var unit = buffer[x, y];

            character = unit.Character;
            foreground = unit.Foreground;
            background = unit.Background;
        }

        public void Clear()
        {
            var emptyUnit = new ConsoleUnit(' ', Color.White, Color.Black);
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    buffer[x, y] = emptyUnit;
        }

        public static ConsoleImage CreateEmpty() => new ConsoleImage(0, 0);
    }
}