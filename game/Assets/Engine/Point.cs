namespace Engine
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        ///     Создать новый экземпляр точки.
        /// </summary>
        /// <param name="x">Координата по X</param>
        /// <param name="y">Координата по Y</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Создать новый экземпляр точки на основе существующей.
        /// </summary>
        /// <param name="other">Существующая точка.</param>
        public Point(Point other)
        {
            X = other.X;
            Y = other.Y;
        }

        public override bool Equals(object other)
        {
            if (!(other is Point)) return false;

            var otherPoint = (Point) other;
            return X == otherPoint.X && Y == otherPoint.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString()
        {
            return string.Format(
                "X: {0}, Y: {1}", 
                X, 
                Y
            );
        }
    }
}