using System;

namespace Astecien.Bezier.Portable
{
    public class Drawer
    {
        private readonly BezierControlPointQuartetCollection bezierControlPointQuartetCollection;
        private readonly BezierPathPointSelector bezierPathPointSelector;
        private readonly BezierPathPointCalculator calculator;

        public Drawer(BezierControlPointQuartetCollection bezierControlPointQuartetCollection, 
                      BezierPathPointSelector bezierPathPointSelector,
                      BezierPathPointCalculator calculator)
        {
            this.bezierControlPointQuartetCollection = bezierControlPointQuartetCollection;
            this.bezierPathPointSelector = bezierPathPointSelector;
            this.calculator = calculator;
        }

        public void DrawControlPoints(Action<BezierControlPoint> controlPointDrawAction, bool isControlPointSelected, ControlPointHandlerId selectedControlPoint)
        {
            for (int quartetIndex = 0; quartetIndex < bezierControlPointQuartetCollection.NumberOfQuartets; quartetIndex++)
            {
                BezierControlPointQuartet bezierQuartetToDraw = bezierControlPointQuartetCollection.GetQuartet(quartetIndex);

                for (int pointIndex = 0; pointIndex < 3; pointIndex++)
                {
                    if (!isControlPointSelected)
                    {
                        BezierControlPoint point = bezierQuartetToDraw.GetBezierControlPoint(pointIndex);
                        controlPointDrawAction(point);
                    }
                    else if (selectedControlPoint.ControlPointIndex != pointIndex || selectedControlPoint.QuartetIndex != quartetIndex)
                    {
                        BezierControlPoint point = bezierQuartetToDraw.GetBezierControlPoint(pointIndex);
                        controlPointDrawAction(point);
                    }
                }
            }
        }

        public void DrawControlLines(Action<BezierControlPoint, BezierControlPoint> drawLineAction)
        {
            ControlPointHandlerId[] handlers = bezierPathPointSelector.GetControlPointPairs();
            for (int handlerIndex = 0; handlerIndex < handlers.Length; handlerIndex += 2)
            {
                BezierControlPoint point1 = bezierControlPointQuartetCollection.GetBezierControlPoint(handlers[handlerIndex]);
                BezierControlPoint point2 = bezierControlPointQuartetCollection.GetBezierControlPoint(handlers[handlerIndex + 1]);
                drawLineAction(point1, point2);
            }
        }

        public void DrawPath(Action<BezierPathPoint, BezierPathPoint> draw)
        {
            BezierPathPoint pathPointStart = null;

            for (float time = 0; time < bezierControlPointQuartetCollection.NumberOfQuartets * 1.0f; time += 0.001f)
            {
                BezierControlPointQuartet bezierControlPointQuartet = bezierControlPointQuartetCollection.GetQuartet(time);
                BezierPathPoint pathPointEnd = calculator.CalculatePathPoint(bezierControlPointQuartet, time);

                if (pathPointStart == null)
                {
                    pathPointStart = pathPointEnd;
                }

                draw(pathPointStart, pathPointEnd);

                pathPointStart = new BezierPathPoint(pathPointEnd.XPosition, pathPointEnd.YPosition);
            }
        }
    }
}
