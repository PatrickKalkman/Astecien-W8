namespace Astecien.Bezier.Portable
{
    public class BezierControlPoint
    {
        public BezierControlPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}