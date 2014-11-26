using System;

using Astecien.Bezier.Portable;

using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Astecien.Test
{
    [TestClass]
    public class BezierPathPointSelectorTest
    {
        [TestMethod]
        public void IsPathPoint_FirstControlPoint_ReturnsTrue()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 0, QuartetIndex = 0 };

            Assert.IsTrue(bezierPathPointSelector.IsPathPoint(firstControlPoint));
        }

        [TestMethod]
        public void IsPathPoint_SecondControlPoint_ReturnsFalse()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var thirdControlPoint = new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = 0 };

            Assert.IsFalse(bezierPathPointSelector.IsPathPoint(thirdControlPoint));
        }

        [TestMethod]
        public void IsPathPoint_FourthControlPoint_ReturnsTrue()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var thirdControlPoint = new ControlPointHandlerId { ControlPointIndex = 3, QuartetIndex = 0 };

            Assert.IsTrue(bezierPathPointSelector.IsPathPoint(thirdControlPoint));
        }

        [TestMethod]
        public void FindRelatedPathPoint_FirstControlPoint_ReturnsLastControlPoint()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 0, QuartetIndex = 0 };

            ControlPointHandlerId foundControlPoint = bezierPathPointSelector.FindRelatedPathPoint(firstControlPoint);

            Assert.AreEqual(collection.NumberOfQuartets - 1, foundControlPoint.QuartetIndex);
            Assert.AreEqual(3, foundControlPoint.ControlPointIndex);
        }

        [TestMethod]
        public void FindRelatedPathPoint_LastControlPoint_ReturnsFirstControlPoint()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 3, QuartetIndex = 1 };

            ControlPointHandlerId foundControlPoint = bezierPathPointSelector.FindRelatedPathPoint(firstControlPoint);

            Assert.AreEqual(0, foundControlPoint.QuartetIndex);
            Assert.AreEqual(0, foundControlPoint.ControlPointIndex);
        }


        [TestMethod]
        public void FindRelatedPathPoint_ThirdControlPoint_ReturnsFirstControlPointOfNextQuartet()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 3, QuartetIndex = 0 };

            ControlPointHandlerId foundControlPoint = bezierPathPointSelector.FindRelatedPathPoint(firstControlPoint);

            Assert.AreEqual(1, foundControlPoint.QuartetIndex);
            Assert.AreEqual(0, foundControlPoint.ControlPointIndex);
        }

        [TestMethod]
        public void FindRelatedPathPoint_SecondControlPoint_ThrowsArgumentException()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = 0 };

            Assert.ThrowsException<ArgumentException>(() => bezierPathPointSelector.FindRelatedPathPoint(firstControlPoint));
        }

        [TestMethod]
        public void FindRelatedControlPoint_ThirdControlPoint_ReturnsSecondControlPointOfNextQuartet()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 2, QuartetIndex = 0 };

            ControlPointHandlerId foundControlPoint = bezierPathPointSelector.FindRelatedControlPoint(firstControlPoint);

            Assert.AreEqual(1, foundControlPoint.QuartetIndex);
            Assert.AreEqual(1, foundControlPoint.ControlPointIndex);
        }

        [TestMethod]
        public void FindRelatedControlPoint_SecondControlPoint_ReturnsLastControlPointOfLastQuartet()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = 0 };

            ControlPointHandlerId foundControlPoint = bezierPathPointSelector.FindRelatedControlPoint(firstControlPoint);

            Assert.AreEqual(1, foundControlPoint.QuartetIndex);
            Assert.AreEqual(2, foundControlPoint.ControlPointIndex);
        }

        [TestMethod]
        public void FindRelatedControlPoint_LastControlPoint_ReturnsSecondControlPoint()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 2, QuartetIndex = 1 };

            ControlPointHandlerId foundControlPoint = bezierPathPointSelector.FindRelatedControlPoint(firstControlPoint);

            Assert.AreEqual(0, foundControlPoint.QuartetIndex);
            Assert.AreEqual(1, foundControlPoint.ControlPointIndex);
        }

        [TestMethod]
        public void FindRelatedControlPoint_ThirdControlPoint_ReturnsLastControlPointOfLastQuartet()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = 0 };

            ControlPointHandlerId foundControlPoint = bezierPathPointSelector.FindRelatedControlPoint(firstControlPoint);

            Assert.AreEqual(1, foundControlPoint.QuartetIndex);
            Assert.AreEqual(2, foundControlPoint.ControlPointIndex);
        }

        [TestMethod]
        public void FindRelatedControlPoint_FirstControlPointSecondQuartet_ReturnsLastControlPointOfFirstQuartet()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = 1 };

            ControlPointHandlerId foundControlPoint = bezierPathPointSelector.FindRelatedControlPoint(firstControlPoint);

            Assert.AreEqual(0, foundControlPoint.QuartetIndex);
            Assert.AreEqual(2, foundControlPoint.ControlPointIndex);
        }

        [TestMethod]
        public void FindRelatedControlPoint_LastControlPointOfFirstQuartet_ReturnsFirstControlPointOfFirstQuartet()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 2, QuartetIndex = 0 };

            ControlPointHandlerId foundControlPoint = bezierPathPointSelector.FindRelatedControlPoint(firstControlPoint);

            Assert.AreEqual(1, foundControlPoint.QuartetIndex);
            Assert.AreEqual(1, foundControlPoint.ControlPointIndex);
        }

        [TestMethod]
        public void FindPathPointOfControlPoint_SecondControlPoint_ReturnFirstPathPoint()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = 0 };

            ControlPointHandlerId foundControlPoint = bezierPathPointSelector.FindPathPointOfControlPoint(firstControlPoint);

            Assert.AreEqual(0, foundControlPoint.QuartetIndex);
            Assert.AreEqual(0, foundControlPoint.ControlPointIndex);
        }

        [TestMethod]
        public void FindPathPointOfControlPoint_ThirdControlPoint_ReturnSecondPathPoint()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstControlPoint = new ControlPointHandlerId { ControlPointIndex = 2, QuartetIndex = 0 };

            ControlPointHandlerId foundControlPoint = bezierPathPointSelector.FindPathPointOfControlPoint(firstControlPoint);

            Assert.AreEqual(0, foundControlPoint.QuartetIndex);
            Assert.AreEqual(3, foundControlPoint.ControlPointIndex);
        }

        [TestMethod]
        public void FindControlPointsOfPathPoint_FirstControlPoint_ReturnSecondControlPointAndOneBeforeLastControlPoint()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstPathPoint = new ControlPointHandlerId { ControlPointIndex = 0, QuartetIndex = 0 };

            ControlPointHandlerId[] foundControlPoints = bezierPathPointSelector.FindControlPointsOfPathPoint(firstPathPoint);

            Assert.AreEqual(0, foundControlPoints[0].QuartetIndex);
            Assert.AreEqual(1, foundControlPoints[0].ControlPointIndex);

            Assert.AreEqual(collection.NumberOfQuartets - 1, foundControlPoints[1].QuartetIndex);
            Assert.AreEqual(2, foundControlPoints[1].ControlPointIndex);
        }

        [TestMethod]
        public void FindControlPointsOfPathPoint_ThirdControlPoint_ReturnThirdControlPointAndFirstControlPointOfNextQuartet()
        {
            BezierControlPointQuartetCollection collection = ControlPointFactory.CreateBezierControlPointQuartetCollection();

            var bezierPathPointSelector = new BezierPathPointSelector(collection);
            var firstPathPoint = new ControlPointHandlerId { ControlPointIndex = 3, QuartetIndex = 0 };

            ControlPointHandlerId[] foundControlPoints = bezierPathPointSelector.FindControlPointsOfPathPoint(firstPathPoint);

            Assert.AreEqual(0, foundControlPoints[0].QuartetIndex);
            Assert.AreEqual(2, foundControlPoints[0].ControlPointIndex);

            Assert.AreEqual(1, foundControlPoints[1].QuartetIndex);
            Assert.AreEqual(1, foundControlPoints[1].ControlPointIndex);
        }


    }
}
