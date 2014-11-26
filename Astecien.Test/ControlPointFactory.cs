using Astecien.Bezier.Portable;

namespace Astecien.Test
{
    public class ControlPointFactory
    {
        public static BezierControlPointQuartetCollection CreateBezierControlPointQuartetCollection()
        {
            var bezierControlPointQuartetCollection = new BezierControlPointQuartetCollection();
            bezierControlPointQuartetCollection.Add(CreateFirstBezierControlPointQuartet());
            bezierControlPointQuartetCollection.Add(CreateSecondBezierControlPointQuartet());
            return bezierControlPointQuartetCollection;
        }

        private static BezierControlPointQuartet CreateFirstBezierControlPointQuartet()
        {
            return new BezierControlPointQuartet(0, 1, 10, 11, 20, 21, 30, 31);
        }

        private static BezierControlPointQuartet CreateSecondBezierControlPointQuartet()
        {
            return new BezierControlPointQuartet(0, 1, 10, 11, 20, 21, 0, 1);
        }
    }
}
