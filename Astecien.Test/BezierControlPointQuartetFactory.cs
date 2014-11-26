using Astecien.Bezier.Portable;

namespace Astecien.Test
{
    public class BezierControlPointQuartetFactory
    {
        public static BezierControlPointQuartet CreateBezierControlPointQuartet()
        {
            var quartet = new BezierControlPointQuartet(
                39, -31,
                -94, -133,
                53, 104,
                90, 214);

            return quartet;
        }
    }
}