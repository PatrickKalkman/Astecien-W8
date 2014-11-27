namespace Astecien.Bezier.Portable
{
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

            //quartet = new BezierControlPointQuartet(
            //    140, 364,
            //    159, 304,
            //    199, 304,
            //    139, 264);

            //bezierControlPointQuartetCollectionToCreate.Add(quartet);

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
