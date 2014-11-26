namespace Astecien.Bezier.Portable
{
    public class ControlHandlerMover
    {
        private readonly BezierControlPointQuartetCollection controlPointQuartetCollection;
        private readonly BezierPathPointSelector bezierPathPointSelector;

        public ControlHandlerMover(BezierControlPointQuartetCollection controlPointQuartetCollection, BezierPathPointSelector bezierPathPointSelector)
        {
            this.controlPointQuartetCollection = controlPointQuartetCollection;
            this.bezierPathPointSelector = bezierPathPointSelector;
        }

        public void MoveControlHandlerTo(ControlPointHandlerId controlPointHandlerId, int xPosition, int yPosition)
        {
            int deltaX = controlPointQuartetCollection.GetBezierControlPoint(controlPointHandlerId).X - xPosition;
            int deltaY = controlPointQuartetCollection.GetBezierControlPoint(controlPointHandlerId).Y - yPosition;

            controlPointQuartetCollection.GetBezierControlPoint(controlPointHandlerId).X = xPosition;
            controlPointQuartetCollection.GetBezierControlPoint(controlPointHandlerId).Y = yPosition;

            if (bezierPathPointSelector.IsPathPoint(controlPointHandlerId))
            {
                ControlPointHandlerId relatedPathPointId = bezierPathPointSelector.FindRelatedPathPoint(controlPointHandlerId);
                controlPointQuartetCollection.GetBezierControlPoint(relatedPathPointId).X = xPosition;
                controlPointQuartetCollection.GetBezierControlPoint(relatedPathPointId).Y = yPosition;

                //Get the related Control points and move them with the same delta.
                ControlPointHandlerId[] relatedControlPoints = bezierPathPointSelector.FindControlPointsOfPathPoint(controlPointHandlerId);
                controlPointQuartetCollection.GetBezierControlPoint(relatedControlPoints[0]).X -= deltaX;
                controlPointQuartetCollection.GetBezierControlPoint(relatedControlPoints[0]).Y -= deltaY;

                controlPointQuartetCollection.GetBezierControlPoint(relatedControlPoints[1]).X -= deltaX;
                controlPointQuartetCollection.GetBezierControlPoint(relatedControlPoints[1]).Y -= deltaY;
            }
            else // It is a control point
            {
                ControlPointHandlerId relatedControlPoint = bezierPathPointSelector.FindRelatedControlPoint(controlPointHandlerId);
                ControlPointHandlerId relatedPathPoint = bezierPathPointSelector.FindPathPointOfControlPoint(controlPointHandlerId);

                //The distance between the given control point and the path point must be the same as 
                int xDistance = controlPointQuartetCollection.GetBezierControlPoint(relatedPathPoint).X - xPosition;
                int yDistance = controlPointQuartetCollection.GetBezierControlPoint(relatedPathPoint).Y - yPosition;

                controlPointQuartetCollection.GetBezierControlPoint(relatedControlPoint).X = controlPointQuartetCollection.GetBezierControlPoint(relatedPathPoint).X + xDistance;
                controlPointQuartetCollection.GetBezierControlPoint(relatedControlPoint).Y = controlPointQuartetCollection.GetBezierControlPoint(relatedPathPoint).Y + yDistance;
            }

            // The basic tricks in getting this to be a smooth curve across the whole path is to:
            // p3 of the each segment is in common to p0 of next each segment.
            // To make the circular, p3 of last segment must be same as p0 of 1st segment.
            // p2 and p3 of each segment must be on a strait line with p0 and p1 or the next segment. So this means 

            // If left handler moved,  must also move corresponding path point so it intersects line to other handler at mid point
            // If right handler moved, must also move corresponding path point so it intersects line to other handler at mid point
        }

        public void AlignAll()
        {
            for (int quartetIndex = 0; quartetIndex < controlPointQuartetCollection.NumberOfQuartets; quartetIndex++)
            {
                BezierControlPointQuartet quartet = controlPointQuartetCollection.GetQuartet(quartetIndex);
                for (int pointIndex = 0; pointIndex < 4; pointIndex++)
                {
                    MoveControlHandlerTo(
                        new ControlPointHandlerId { ControlPointIndex = pointIndex, QuartetIndex = quartetIndex },
                        quartet.GetBezierControlPoint(pointIndex).X,
                        quartet.GetBezierControlPoint(pointIndex).Y);
                }
            }            
        }
    }
}