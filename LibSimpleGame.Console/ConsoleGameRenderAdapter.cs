using System.Text;

namespace LibSimpleGame.Console
{
    public class ConsoleGameRenderAdapter : GameRenderAdapter
    {
        static ConsoleGameRenderAdapter()
        {
            NativeApi.EnableANSI();
        }

        private ConsoleBuffer buffer;
        private StringBuilder renderStringBuilder = new StringBuilder();

        public ConsoleBuffer Buffer => buffer;

        public ConsoleGameRenderAdapter() : this(100, 50)
        { }

        public ConsoleGameRenderAdapter(ConsoleBuffer origin)
        {
            buffer = origin.Clone();
        }

        public ConsoleGameRenderAdapter(int width, int height)
        {
            buffer = new ConsoleBuffer(width, height);
        }

        public void RenderCore()
        {
            Color? currentForeground = null;
            Color? currentBackground = null;

            renderStringBuilder.Clear();
            renderStringBuilder.Append("\x1b[;H");
            for (int y = 0; y < buffer.Height; y++)
            {
                for (int x = 0; x < buffer.Width; x++)
                {
                    buffer.Get(x, y, out char character, out Color foreground, out Color background);

                    if (foreground != currentForeground)
                    {
                        renderStringBuilder.Append("\u001b[38;2;");
                        renderStringBuilder.Append(foreground.R);
                        renderStringBuilder.Append(';');
                        renderStringBuilder.Append(foreground.G);
                        renderStringBuilder.Append(';');
                        renderStringBuilder.Append(foreground.B);
                        renderStringBuilder.Append('m');
                        currentForeground = foreground;
                    }
                    if (background != currentBackground)
                    {
                        renderStringBuilder.Append("\u001b[48;2;");
                        renderStringBuilder.Append(background.R);
                        renderStringBuilder.Append(';');
                        renderStringBuilder.Append(background.G);
                        renderStringBuilder.Append(';');
                        renderStringBuilder.Append(background.B);
                        renderStringBuilder.Append('m');
                        currentBackground = background;
                    }

                    renderStringBuilder.Append(character);
                }

                renderStringBuilder.AppendLine();
            }

            renderStringBuilder.Append("\x1b[0m");
            System.Console.Write(renderStringBuilder);
        }

        public override void Render()
        {
            RenderCore();
            Buffer.Clear();
        }

        public override void Start()
        {
            // nothing to do
        }

        public override void Stop()
        {
            // nothing to do
        }

        public void Resize(int width, int height)
        {
            buffer = new ConsoleBuffer(width, height);
        }
    }
}