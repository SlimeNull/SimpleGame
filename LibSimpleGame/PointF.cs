namespace LibSimpleGame
{
    public record struct PointF(float X, float Y)
    {
        public Point ToPoint() => new Point((int)X, (int)Y);


        public SizeF ToSizeF() => new SizeF(X, Y);

        public static PointF operator +(PointF point, SizeF offset)
        {
            return new PointF(point.X + offset.Width, point.Y + offset.Height);
        }

        public static PointF operator -(PointF point, SizeF offset)
        {
            return new PointF(point.X - offset.Width, point.Y - offset.Height);
        }

        public static implicit operator PointF(Point point)
        {
            return new PointF(point.X, point.Y);
        }
    }

    public record struct SizeF(float Width, float Height)
    {
        public static SizeF operator -(SizeF origin)
        {
            return new SizeF(-origin.Width, -origin.Height);
        }


        public static implicit operator SizeF(Size size)
        {
            return new SizeF(size.Width, size.Height);
        }

        public static SizeF operator *(SizeF origin, float factor)
        {
            return new SizeF(origin.Width * factor, origin.Height * factor);
        }

        public static SizeF operator /(SizeF origin, float factor)
        {
            return new SizeF(origin.Width / factor, origin.Height / factor);
        }
    }
}