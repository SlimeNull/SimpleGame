using System.Text;

namespace LibSimpleGame.Console
{
    public class ConsoleBuffer : ICloneable
    {
        ConsoleUnit[,] buffer;

        public int Width { get; }
        public int Height { get; }

        public ConsoleBuffer(int width, int height)
        {
            Width = width;
            Height = height;
            buffer = new ConsoleUnit[width, height];

            Clear();
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
        public void Clear()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    buffer[x, y] = new ConsoleUnit(' ', Color.White, Color.Black);
        }

        public ConsoleBuffer Clone()
        {
            ConsoleBuffer newBuffer = new ConsoleBuffer(Width, Height);
            Array.Copy(buffer, newBuffer.buffer, buffer.Length);

            return newBuffer;
        }

        object ICloneable.Clone() => Clone();
    }
}