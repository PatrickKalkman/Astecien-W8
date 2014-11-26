using System.Collections.Generic;

namespace Astecien.Bezier.Portable
{
    /// <summary>
    /// This class represents four bezier control points which are used
    /// to create a bezier curve, a part of the bezier circle.
    /// </summary>
    public class BezierControlPointQuartet
    {
        public readonly static int NumberOfPointsPerQuartet = 4;

        readonly List<BezierControlPoint> points = new List<BezierControlPoint>(NumberOfPointsPerQuartet);

        public BezierControlPointQuartet(
            int xPoint0, int yPoint0,
            int xPoint1, int yPoint1,
            int xPoint2, int yPoint2,
            int xPoint3, int yPoint3)
        {
            points.Add(new BezierControlPoint(xPoint0, yPoint0));
            points.Add(new BezierControlPoint(xPoint1, yPoint1));
            points.Add(new BezierControlPoint(xPoint2, yPoint2));
            points.Add(new BezierControlPoint(xPoint3, yPoint3));
        }

        public int XPoint0
        {
            get { return points[0].X; }
        }

        public int YPoint0
        {
            get { return points[0].Y; }
        }

        public int XPoint1
        {
            get { return points[1].X; }
        }

        public int YPoint1
        {
            get { return points[1].Y; }
        }

        public int XPoint2
        {
            get { return points[2].X; }
        }

        public int YPoint2
        {
            get { return points[2].Y; }
        }

        public int XPoint3
        {
            get { return points[3].X; }
        }

        public int YPoint3
        {
            get { return points[3].Y; }
        }

        // (x - center_x)^2 + (y - center_y)^2 < radius^2
        public bool IsInControlPoint(int xOfPointToCheck, int yOfPointToCheck, int radius, out int controlPointIndex)
        {
            int pointIndex = 0;
            foreach (var bezierControlPoint in points)
            {
                int left = (bezierControlPoint.X - xOfPointToCheck) * (bezierControlPoint.X - xOfPointToCheck) +
                           (bezierControlPoint.Y - yOfPointToCheck) * (bezierControlPoint.Y - yOfPointToCheck);

                if (left < (radius * radius))
                {
                    controlPointIndex = pointIndex;
                    return true;
                }

                pointIndex++;
            }

            controlPointIndex = -1;
            return false;
        }

        public BezierControlPoint GetBezierControlPoint(int controlPointIndex)
        {
            return points[controlPointIndex];
        }
    }
}