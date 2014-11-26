using System;

using Astecien.Bezier.Portable;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Astecien.Test
{
    [TestClass]
    public class BezierPathPointCalculatorTests
    {
        [TestMethod]
        public void CalculatPathPoint_ZeroTime_ReturnsStartOfBezierCurve()
        {
            var bezierPathPointCalculator = new BezierPathPointCalculator();
            BezierControlPointQuartet bezierControlPointQuartet = CreateBezierControlPointQuartet();
            BezierPathPoint pathPoint = bezierPathPointCalculator.CalculatePathPoint(bezierControlPointQuartet, 0);
            Assert.AreEqual(bezierControlPointQuartet.XPoint0, pathPoint.XPosition);
            Assert.AreEqual(bezierControlPointQuartet.YPoint0, pathPoint.YPosition);
        }

        [TestMethod]
        public void CalculatePathPoint_EndTime_ReturnsStartOfBezierCurve()
        {
            var bezierPathPointCalculator = new BezierPathPointCalculator();
            BezierControlPointQuartet bezierControlPointQuartet = CreateBezierControlPointQuartet();
            BezierPathPoint pathPoint = bezierPathPointCalculator.CalculatePathPoint(bezierControlPointQuartet, 0.9999999f);
            Assert.AreEqual(bezierControlPointQuartet.XPoint3, Math.Round(pathPoint.XPosition));
            Assert.AreEqual(bezierControlPointQuartet.YPoint3, Math.Round(pathPoint.YPosition));
        }

        [TestMethod]
        public void LogPathToConsole()
        {
            var bezierPathPointCalculator = new BezierPathPointCalculator();
            BezierControlPointQuartet bezierControlPointQuartet = CreateBezierControlPointQuartet();

            for (float time = 0; time <= 1; time += 0.05f)
            {
                BezierPathPoint pathPoint = bezierPathPointCalculator.CalculatePathPoint(bezierControlPointQuartet, time);
                System.Diagnostics.Debug.WriteLine("t {0} \t X {1} \t Y {2}", time, pathPoint.XPosition, pathPoint.YPosition);
            }
        }

        private BezierControlPointQuartet CreateBezierControlPointQuartet()
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
