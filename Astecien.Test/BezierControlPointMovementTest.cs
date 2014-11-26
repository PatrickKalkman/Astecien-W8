using Astecien.Bezier.Portable;

using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Astecien.Test
{
    [TestClass]
    public class SmoothBezierControlPointMovementTest
    {
        [TestMethod]
        public void Move_FirstPathPoint_AlsoMovesLastPathPoint()
        {
            var bezierControlPointQuartetCollection = CreateBezierControlPointQuartetCollection();

            var mover = CreateControlHandlerMover(bezierControlPointQuartetCollection);

            var idOfFirstPathPoint = new ControlPointHandlerId { QuartetIndex = 0, ControlPointIndex = 0 };
            var idOfLastPathPoint = new ControlPointHandlerId { QuartetIndex = 1, ControlPointIndex = 3 };

            const int XPositionToMoveTo = 21;
            const int YPositionToMoveTo = 23;

            mover.MoveControlHandlerTo(idOfFirstPathPoint, XPositionToMoveTo, YPositionToMoveTo);

            BezierControlPoint lastPathPoint = bezierControlPointQuartetCollection.GetBezierControlPoint(idOfLastPathPoint);

            Assert.AreEqual(XPositionToMoveTo, lastPathPoint.X);
            Assert.AreEqual(YPositionToMoveTo, lastPathPoint.Y);
        }

        [TestMethod]
        public void Move_LastPathPoint_AlsoMovesFirstPathPoint()
        {
            var bezierControlPointQuartetCollection = CreateBezierControlPointQuartetCollection();

            var mover = CreateControlHandlerMover(bezierControlPointQuartetCollection);

            var idOfLastPathPoint = new ControlPointHandlerId { QuartetIndex = 1, ControlPointIndex = 3 };
            var idOfFirstPathPoint = new ControlPointHandlerId { QuartetIndex = 0, ControlPointIndex = 0 };

            const int XPositionToMoveTo = 21;
            const int YPositionToMoveTo = 23;

            mover.MoveControlHandlerTo(idOfLastPathPoint, XPositionToMoveTo, YPositionToMoveTo);

            BezierControlPoint firstPathPoint = bezierControlPointQuartetCollection.GetBezierControlPoint(idOfFirstPathPoint);

            Assert.AreEqual(XPositionToMoveTo, firstPathPoint.X);
            Assert.AreEqual(YPositionToMoveTo, firstPathPoint.Y);
        }

        [TestMethod]
        public void Move_SecondPathPoint_AlsoMovesThirdPathPoint()
        {
            var bezierControlPointQuartetCollection = CreateBezierControlPointQuartetCollection();

            var mover = CreateControlHandlerMover(bezierControlPointQuartetCollection);

            var idOfSecondPathPoint = new ControlPointHandlerId { QuartetIndex = 0, ControlPointIndex = 3 };
            var idOfThirdPathPoint = new ControlPointHandlerId { QuartetIndex = 1, ControlPointIndex = 0 };

            const int XPositionToMoveTo = 21;
            const int YPositionToMoveTo = 23;

            mover.MoveControlHandlerTo(idOfSecondPathPoint, XPositionToMoveTo, YPositionToMoveTo);

            BezierControlPoint thirdPathPoint = bezierControlPointQuartetCollection.GetBezierControlPoint(idOfThirdPathPoint);

            Assert.AreEqual(XPositionToMoveTo, thirdPathPoint.X);
            Assert.AreEqual(YPositionToMoveTo, thirdPathPoint.Y);
        }

        [TestMethod]
        public void
            Move_ThirdControlPoint_MovesSecondControlPointOfNextQuartetSoThatTheLineBetweenTheseControlPointsCrossesThePathPoints()
        {
            var bezierControlPointQuartetCollection = CreateBezierControlPointQuartetCollection();

            var mover = CreateControlHandlerMover(bezierControlPointQuartetCollection);

            var idOfThirdControlPoint = new ControlPointHandlerId { QuartetIndex = 0, ControlPointIndex = 2 };
            var idOfRelatedPathPoint = new ControlPointHandlerId { QuartetIndex = 0, ControlPointIndex = 3 };
            var idOfRelatedControlPoint = new ControlPointHandlerId { QuartetIndex = 1, ControlPointIndex = 1 };

            const int XPositionToMoveTo = 20;
            const int YPositionToMoveTo = 20;

            bezierControlPointQuartetCollection.GetBezierControlPoint(idOfRelatedPathPoint).X = 40;
            bezierControlPointQuartetCollection.GetBezierControlPoint(idOfRelatedPathPoint).Y = 40;

            mover.MoveControlHandlerTo(idOfThirdControlPoint, XPositionToMoveTo, YPositionToMoveTo);

            BezierControlPoint relatedControlPoint =
                bezierControlPointQuartetCollection.GetBezierControlPoint(idOfRelatedControlPoint);

            Assert.AreEqual(60, relatedControlPoint.X);
            Assert.AreEqual(60, relatedControlPoint.Y);
        }

        [TestMethod]
        public void Move_PathPoint_MovesRelatedControlPoints()
        {
            var bezierControlPointQuartetCollection = CreateBezierControlPointQuartetCollection();

            var mover = CreateControlHandlerMover(bezierControlPointQuartetCollection);

            var idOfPathPoint = new ControlPointHandlerId { QuartetIndex = 0, ControlPointIndex = 0 };

            var idRelatedPathPoint1 = new ControlPointHandlerId { QuartetIndex = 0, ControlPointIndex = 1 };
            var idRelatedPathPoint2 = new ControlPointHandlerId { QuartetIndex = 1, ControlPointIndex = 2 };

            BezierControlPoint relatedPathPoint1 = bezierControlPointQuartetCollection.GetBezierControlPoint(idRelatedPathPoint1);
            BezierControlPoint relatedPathPoint2 = bezierControlPointQuartetCollection.GetBezierControlPoint(idRelatedPathPoint2);

            int xPositionFirstRelatedPoint = relatedPathPoint1.X;
            int yPositionFirstRelatedPoint = relatedPathPoint1.Y;

            const int XPositionToMoveTo = 21;
            const int YPositionToMoveTo = 23;

            mover.MoveControlHandlerTo(idOfPathPoint, XPositionToMoveTo, YPositionToMoveTo);

            relatedPathPoint1 = bezierControlPointQuartetCollection.GetBezierControlPoint(idRelatedPathPoint1);

            Assert.AreNotEqual(xPositionFirstRelatedPoint, relatedPathPoint1.X);
            Assert.AreNotEqual(yPositionFirstRelatedPoint, relatedPathPoint1.Y);
        }

        [TestMethod]
        public void Move_PathPoint_MovesRelatedControlPointsLargeSet()
        {
            var bezierControlPointQuartetCollection = BezierControlPointCollectionFactory.CreateDemoCollection();

            var mover = CreateControlHandlerMover(bezierControlPointQuartetCollection);

            var idOfPathPoint = new ControlPointHandlerId { QuartetIndex = 0, ControlPointIndex = 0 };

            var idRelatedPathPoint1 = new ControlPointHandlerId { QuartetIndex = 0, ControlPointIndex = 1 };
            var idRelatedPathPoint2 = new ControlPointHandlerId { QuartetIndex = 1, ControlPointIndex = 2 };

            BezierControlPoint relatedPathPoint1 = bezierControlPointQuartetCollection.GetBezierControlPoint(idRelatedPathPoint1);
            BezierControlPoint relatedPathPoint2 = bezierControlPointQuartetCollection.GetBezierControlPoint(idRelatedPathPoint2);

            int xPositionFirstRelatedPoint = relatedPathPoint1.X;
            int yPositionFirstRelatedPoint = relatedPathPoint1.Y;

            const int XPositionToMoveTo = 21;
            const int YPositionToMoveTo = 23;

            mover.MoveControlHandlerTo(idOfPathPoint, XPositionToMoveTo, YPositionToMoveTo);

            relatedPathPoint1 = bezierControlPointQuartetCollection.GetBezierControlPoint(idRelatedPathPoint1);

            Assert.AreNotEqual(xPositionFirstRelatedPoint, relatedPathPoint1.X);
            Assert.AreNotEqual(yPositionFirstRelatedPoint, relatedPathPoint1.Y);
        }


        private BezierControlPointQuartetCollection CreateBezierControlPointQuartetCollection()
        {
            var bezierControlPointQuartetCollection = new BezierControlPointQuartetCollection();
            bezierControlPointQuartetCollection.Add(CreateFirstBezierControlPointQuartet());
            bezierControlPointQuartetCollection.Add(CreateSecondBezierControlPointQuartet());
            return bezierControlPointQuartetCollection;
        }

        private BezierControlPointQuartet CreateFirstBezierControlPointQuartet()
        {
            return new BezierControlPointQuartet(0, 1, 10, 11, 20, 21, 30, 31);
        }

        private BezierControlPointQuartet CreateSecondBezierControlPointQuartet()
        {
            return new BezierControlPointQuartet(0, 1, 10, 11, 20, 21, 0, 1);
        }

        private static ControlHandlerMover CreateControlHandlerMover(
            BezierControlPointQuartetCollection bezierControlPointQuartetCollection)
        {
            return new ControlHandlerMover(bezierControlPointQuartetCollection,
                new BezierPathPointSelector(bezierControlPointQuartetCollection));
        }
    }

    public static class BezierControlPointCollectionFactory
    {
        public static BezierControlPointQuartetCollection CreateDemoCollection()
        {
            var bezierControlPointQuartetCollectionToCreate = new BezierControlPointQuartetCollection();

            var quartet = new BezierControlPointQuartet(
                189, 119,
                56, 17,
                203, 254,
                240, 364);

            bezierControlPointQuartetCollectionToCreate.Add(quartet);

            quartet = new BezierControlPointQuartet(
                240, 364,
                259, 904,
                299, 904,
                339, 664);

            bezierControlPointQuartetCollectionToCreate.Add(quartet);

            quartet = new BezierControlPointQuartet(
                140, 364,
                159, 304,
                199, 304,
                139, 264);

            bezierControlPointQuartetCollectionToCreate.Add(quartet);

            //quartet = new BezierControlPointQuartet(
            //    240, 264,
            //    259, 204,
            //    299, 204,
            //    239, 234);

            //bezierControlPointQuartetCollectionToCreate.Add(quartet);

            //quartet = new BezierControlPointQuartet(
            //    40, 164,
            //    259, 1204,
            //    399, 154,
            //    439, 164);

            //bezierControlPointQuartetCollectionToCreate.Add(quartet);


            return bezierControlPointQuartetCollectionToCreate;
        }

    }
}
