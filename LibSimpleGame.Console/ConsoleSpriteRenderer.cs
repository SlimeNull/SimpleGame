namespace LibSimpleGame.Console
{
    public class ConsoleSpriteRenderer : GameComponent
    {
        private ConsoleGameRenderAdapter? adapter;

        public override void Awake()
        {
            adapter = Game.Renderer.GetRequiredAdapter<ConsoleGameRenderAdapter>();
        }

        public override void LateUpdate()
        {
            if (adapter == null)
                return;
            if (Owner == null)
                return;
            if (Owner.GetComponent<ConsoleSprite>() is not ConsoleSprite sprite)
                return;

            var position = Owner.Position.ToPoint();

            var buffer = adapter.Buffer;
            var image = sprite.Image;
            var center = sprite.CenterPoint;

            var startPoint = position - center.ToSize();
            var endPoint = position + image.Size;

            int yStart = 0;
            int xStart = 0;
            int yEnd = image.Height;
            int xEnd = image.Width;

            if (startPoint.Y < 0)
                yStart = -(startPoint.Y);
            if (startPoint.X < 0)
                xStart = -(startPoint.X);
            if (endPoint.Y > buffer.Height)
                yEnd = image.Height - (endPoint.Y - buffer.Height);
            if (endPoint.X > buffer.Width)
                xEnd = image.Width - (endPoint.X - buffer.Width);

            for (int y = yStart; y < yEnd; y++)
            {
                for (int x = xStart; x < xEnd; x++)
                {
                    image.Get(x, y, out char c, out Color fg, out Color bg);
                    buffer.Set(startPoint.X + x, startPoint.Y + y, c, fg, bg);
                }
            }
        }
    }
}
