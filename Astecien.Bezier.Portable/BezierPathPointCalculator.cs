namespace Astecien.Bezier.Portable
{
    /// <summary>
    /// This class is responsible for calculating a point on a bezier curve based on a given time.
    /// </summary>
    public class BezierPathPointCalculator
    {
        public BezierPathPoint CalculatePathPoint(BezierControlPointQuartet bezierControlPointKwartet, float timeToCalculatePoint)
        {
            float time = timeToCalculatePoint - (int)timeToCalculatePoint;

            float cx = 3 * (bezierControlPointKwartet.XPoint1 - bezierControlPointKwartet.XPoint0);
            float cy = 3 * (bezierControlPointKwartet.YPoint1 - bezierControlPointKwartet.YPoint0);

            float bx = 3 * (bezierControlPointKwartet.XPoint2 - bezierControlPointKwartet.XPoint1) - cx;
            float by = 3 * (bezierControlPointKwartet.YPoint2 - bezierControlPointKwartet.YPoint1) - cy;

            float ax = bezierControlPointKwartet.XPoint3 - bezierControlPointKwartet.XPoint0 - cx - bx;
            float ay = bezierControlPointKwartet.YPoint3 - bezierControlPointKwartet.YPoint0 - cy - by;

            float cube = time * time * time;

            float square = time * time;

            float resX = (ax * cube) + (bx * square) + (cx * time) + bezierControlPointKwartet.XPoint0;
            float resY = (ay * cube) + (by * square) + (cy * time) + bezierControlPointKwartet.YPoint0;

            return new BezierPathPoint(resX, resY);
        }
    }
}