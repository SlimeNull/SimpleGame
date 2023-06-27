namespace LibSimpleGame
{
    public record struct Point(int X, int Y)
    {
        public static readonly Point Empty = new Point(0, 0);

        public Size ToSize() => new Size(X, Y);

        public static Point operator +(Point point, Size offset)
        {
            return new Point(point.X + offset.Width, point.Y + offset.Height);
        }

        public static Point operator -(Point point, Size offset)
        {
            return new Point(point.X - offset.Width, point.Y - offset.Height);
        }

        public static explicit operator Point(PointF point)
        {
            return point.ToPoint();
        }
    }

    public record struct Size(int Width, int Height)
    {
        public static Size operator -(Size origin)
        {
            return new Size(-origin.Width, -origin.Height);
        }

        public static Size operator *(Size origin, int factor)
        {
            return new Size(origin.Width * factor, origin.Height * factor);
        }

        public static Size operator /(Size origin, int factor)
        {
            return new Size(origin.Width / factor, origin.Height / factor);
        }
    }
}