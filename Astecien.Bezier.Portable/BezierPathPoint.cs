namespace Astecien.Bezier.Portable
{
    public class BezierPathPoint
    {
        private readonly float xPosition;
        private readonly float yPosition;

        public BezierPathPoint(float xPosition, float yPosition)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
        }

        public float XPosition
        {
            get { return xPosition; }
        }

        public float YPosition
        {
            get { return yPosition; }
        }
    }
}