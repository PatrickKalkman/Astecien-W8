using Astecien.Bezier.Portable;

using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Astecien.Test
{
    [TestClass]
    public class BezierControlPointQuartetCollectionTest
    {
        [TestMethod]
        public void Add_EmptyCollection_FillsCollectionWithOneQuartet()
        {
            var controlPointQuartetCollection = new BezierControlPointQuartetCollection();
            controlPointQuartetCollection.Add(BezierControlPointQuartetFactory.CreateBezierControlPointQuartet());
            Assert.AreEqual(1, controlPointQuartetCollection.NumberOfQuartets);
        }

        [TestMethod]
        public void Add_CollectionWithOne_ReturnTwoQuartets()
        {
            var controlPointQuartetCollection = new BezierControlPointQuartetCollection();
            controlPointQuartetCollection.Add(BezierControlPointQuartetFactory.CreateBezierControlPointQuartet());
            controlPointQuartetCollection.Add(BezierControlPointQuartetFactory.CreateBezierControlPointQuartet());
            Assert.AreEqual(2, controlPointQuartetCollection.NumberOfQuartets);
        }

        [TestMethod]
        public void GetQuartet_TimeIsInFirstQuartetRange_ReturnsFirstQuarted()
        {
            var controlPointQuartetCollection = new BezierControlPointQuartetCollection();
            BezierControlPointQuartet firstQuartet = BezierControlPointQuartetFactory.CreateBezierControlPointQuartet();
            controlPointQuartetCollection.Add(firstQuartet);
            BezierControlPointQuartet secondQuartet = BezierControlPointQuartetFactory.CreateBezierControlPointQuartet();
            controlPointQuartetCollection.Add(secondQuartet);

            BezierControlPointQuartet quartetFromTime = controlPointQuartetCollection.GetQuartet(0.5f);

            Assert.AreEqual(firstQuartet, quartetFromTime);
        }

        [TestMethod]
        public void GetQuartet_TimeIsInSecondQuartetRange_ReturnsSecondQuarted()
        {
            var controlPointQuartetCollection = new BezierControlPointQuartetCollection();
            BezierControlPointQuartet firstQuartet = BezierControlPointQuartetFactory.CreateBezierControlPointQuartet();
            controlPointQuartetCollection.Add(firstQuartet);
            BezierControlPointQuartet secondQuartet = BezierControlPointQuartetFactory.CreateBezierControlPointQuartet();
            controlPointQuartetCollection.Add(secondQuartet);

            BezierControlPointQuartet quartetFromTime = controlPointQuartetCollection.GetQuartet(1.5f);

            Assert.AreEqual(secondQuartet, quartetFromTime);
        }

        [TestMethod]
        public void GetQuartet_TimeIsInThirdQuartetRange_ReturnsThirdQuarted()
        {
            var controlPointQuartetCollection = new BezierControlPointQuartetCollection();
            BezierControlPointQuartet firstQuartet = BezierControlPointQuartetFactory.CreateBezierControlPointQuartet();
            controlPointQuartetCollection.Add(firstQuartet);
            BezierControlPointQuartet secondQuartet = BezierControlPointQuartetFactory.CreateBezierControlPointQuartet();
            controlPointQuartetCollection.Add(secondQuartet);
            BezierControlPointQuartet thirdQuartet = BezierControlPointQuartetFactory.CreateBezierControlPointQuartet();
            controlPointQuartetCollection.Add(thirdQuartet);

            BezierControlPointQuartet quartetFromTime = controlPointQuartetCollection.GetQuartet(2.5f);

            Assert.AreEqual(thirdQuartet, quartetFromTime);
        }

    }
}
