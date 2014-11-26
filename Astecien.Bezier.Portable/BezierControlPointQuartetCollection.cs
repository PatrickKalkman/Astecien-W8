using System.Collections.Generic;

namespace Astecien.Bezier.Portable
{
    public class BezierControlPointQuartetCollection 
    {
        private readonly List<BezierControlPointQuartet> controlPointQuartets = new List<BezierControlPointQuartet>();

        public void Add(BezierControlPointQuartet bezierControlPointQuartet)
        {
            controlPointQuartets.Add(bezierControlPointQuartet);
        }

        public int NumberOfQuartets
        {
            get
            {
                return controlPointQuartets.Count;
            }
        }

        public BezierControlPointQuartet GetQuartet(int quartetIndex)
        {
            return controlPointQuartets[quartetIndex];
        }

        public BezierControlPointQuartet GetQuartet(float time)
        {
            int quartetIndex = (int)(time);
            return controlPointQuartets[quartetIndex];
        }

        public bool GivenPositionIsInsideControlPoint(int xPosition, int yPosition, int imageWidth, out int selectedBasierQuartetIndex, out int controlPointIndex)
        {
            for (int bezierQuartetIndex = 0; bezierQuartetIndex < controlPointQuartets.Count; bezierQuartetIndex++)
            {
                if (controlPointQuartets[bezierQuartetIndex].IsInControlPoint(xPosition, yPosition, imageWidth, out controlPointIndex))
                {
                    selectedBasierQuartetIndex = bezierQuartetIndex;
                    return true;
                }
            }

            selectedBasierQuartetIndex = -1;
            controlPointIndex = -1;
            return false;
        }

        public BezierControlPoint GetBezierControlPoint(ControlPointHandlerId idOfcontrolPointToMove)
        {
            return
                controlPointQuartets[idOfcontrolPointToMove.QuartetIndex].GetBezierControlPoint(
                    idOfcontrolPointToMove.ControlPointIndex);
        }

        public void RemoveLast()
        {
            controlPointQuartets.RemoveAt(controlPointQuartets.Count - 1);
        }
    }
}