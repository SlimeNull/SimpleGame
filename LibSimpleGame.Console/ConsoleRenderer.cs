using System.Text;

namespace LibSimpleGame.Console
{
    public class ConsoleRenderer : GameComponent
    {
        static ConsoleRenderer()
        {
            NativeApi.EnableANSI();
        }

        private ConsoleBuffer buffer;

        public ConsoleBuffer Buffer
        {
            get => buffer; 
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                buffer = value;
            }
        }

        public ConsoleRenderer() : this(100, 50)
        { }

        public ConsoleRenderer(ConsoleBuffer origin)
        {
            buffer = origin.Clone();
        }

        public ConsoleRenderer(int width, int height)
        {
            buffer = new ConsoleBuffer(width, height);
        }

        public void Render()
        {
            StringBuilder sb = new StringBuilder();
            Color? currentForeground = null;
            Color? currentBackground = null;

            for (int y = 0; y < Buffer.Height; y++)
            {
                for (int x = 0; x < Buffer.Width; x++)
                {
                    Buffer.Get(new Point(x, y), out char character, out Color foreground, out Color background);

                    if (foreground != currentForeground ||
                        background != currentForeground)
                    {
                        sb.Append($"\x1B[38;2;{foreground.R};{foreground.G};{foreground.B}m\x1b[48;2;{background.R};{background.G};{background.B}m");
                    }

                    currentForeground = foreground;
                    currentBackground = background;
                    sb.Append(character);
                }

                sb.AppendLine();
            }

            sb.Append("\x1b[0m");

            System.Console.SetCursorPosition(0, 0);
            System.Console.Write(sb.ToString());
        }

        public override void LateUpdate()
        {
            Render();
            Buffer.Clear();
        }
    }
}