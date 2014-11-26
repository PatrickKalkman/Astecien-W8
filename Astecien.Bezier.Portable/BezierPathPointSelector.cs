using System;
using System.Collections.Generic;

namespace Astecien.Bezier.Portable
{
    public class BezierPathPointSelector
    {
        private readonly BezierControlPointQuartetCollection controlPointQuartetCollection;

        public BezierPathPointSelector(BezierControlPointQuartetCollection controlPointQuartetCollection)
        {
            this.controlPointQuartetCollection = controlPointQuartetCollection;
            CreatePathPointMapping();
        }

        public bool IsPathPoint(ControlPointHandlerId controlPoint)
        {
            if (controlPoint.ControlPointIndex == 0 || controlPoint.ControlPointIndex == 3)
            {
                return true;
            }

            return false;
        }

        private readonly Dictionary<string, ControlPointHandlerId> pathPointMapping = new Dictionary<string, ControlPointHandlerId>();

        public void CreatePathPointMapping()
        {
            for (int quartetIndex = 0; quartetIndex < controlPointQuartetCollection.NumberOfQuartets; quartetIndex++)
            {
                int mappedFirstQuartetIndex;
                if (quartetIndex == 0)
                {
                    mappedFirstQuartetIndex = controlPointQuartetCollection.NumberOfQuartets - 1;
                }
                else
                {
                    mappedFirstQuartetIndex = quartetIndex - 1;
                }

                int mappedLastQuartetIndex;
                if (quartetIndex == 0 && controlPointQuartetCollection.NumberOfQuartets > 1 && quartetIndex != controlPointQuartetCollection.NumberOfQuartets - 1)
                {
                    mappedLastQuartetIndex = quartetIndex + 1;
                }
                else
                {
                    mappedLastQuartetIndex = 0;
                }

                pathPointMapping.Add(createKey(quartetIndex, 0), new ControlPointHandlerId { QuartetIndex = mappedFirstQuartetIndex, ControlPointIndex = 3 });
                pathPointMapping.Add(createKey(quartetIndex, 1), null);
                pathPointMapping.Add(createKey(quartetIndex, 2), null);
                pathPointMapping.Add(createKey(quartetIndex, 3), new ControlPointHandlerId { QuartetIndex = mappedLastQuartetIndex, ControlPointIndex = 0 });
            }
        }

        public string createKey(int quartetIndex, int controlPointIndex)
        {
            return string.Format("{0}{1}", quartetIndex, controlPointIndex);
        }

// With one Quartet
//        [ 0, 0 ] = [ 0, 3]
//        [ 0, 1 ] = [ Invalid ]
//        [ 0, 2 ] = [ Invalid ] 
//        [ 0, 3 ] = [ 0, 0]

// With two Quartets
//        [ 0, 0 ] = [ 1, 3]
//        [ 0, 1 ] = [ Invalid ]
//        [ 0, 2 ] = [ Invalid ] 
//        [ 0, 3 ] = [ 1, 0]

//        [ 1, 0 ] = [ 0, 3]
//        [ 1, 1 ] = [ Invalid ]
//        [ 1, 2 ] = [ Invalid ] 
//        [ 1, 3 ] = [ 0, 0]

// With three Quartets
//        [ 0, 0 ] = [ 1, 3]
//        [ 0, 1 ] = [ Invalid ]
//        [ 0, 2 ] = [ Invalid ] 
//        [ 0, 3 ] = [ 1, 0]

//        [ 1, 0 ] = [ 0, 3]
//        [ 1, 1 ] = [ Invalid ]
//        [ 1, 2 ] = [ Invalid ] 
//        [ 1, 3 ] = [ 2, 0]

//        [ 2, 0 ] = [ 1, 3]
//        [ 2, 1 ] = [ Invalid ]
//        [ 2, 2 ] = [ Invalid ] 
//        [ 2, 3 ] = [ 0, 0]

// With four Quartets
//        [ 0, 0 ] = [ 1, 3]
//        [ 0, 1 ] = [ Invalid ]
//        [ 0, 2 ] = [ Invalid ] 
//        [ 0, 3 ] = [ 1, 0]

//        [ 1, 0 ] = [ 0, 3]
//        [ 1, 1 ] = [ Invalid ]
//        [ 1, 2 ] = [ Invalid ] 
//        [ 1, 3 ] = [ 2, 0]

//        [ 2, 0 ] = [ 1, 3]
//        [ 2, 1 ] = [ Invalid ]
//        [ 2, 2 ] = [ Invalid ] 
//        [ 2, 3 ] = [ 3, 0]

//        [ 3, 0 ] = [ 2, 3]
//        [ 3, 1 ] = [ Invalid ]
//        [ 3, 2 ] = [ Invalid ] 
//        [ 3, 3 ] = [ 0, 0]

// etc.

        public ControlPointHandlerId FindRelatedPathPoint(ControlPointHandlerId controlPointId)
        {
            if (!IsPathPoint(controlPointId))
            {
                throw new ArgumentException("The given control point is not a path point.");
            }

            return pathPointMapping[string.Format("{0}{1}", controlPointId.QuartetIndex, controlPointId.ControlPointIndex)];
        }

        public ControlPointHandlerId FindRelatedControlPoint(ControlPointHandlerId controlPoint)
        {
            var controlPointHandler = new ControlPointHandlerId();

            if (controlPoint.ControlPointIndex == 1)
            {
                controlPointHandler.ControlPointIndex = 2;
                if (controlPoint.QuartetIndex == 0)
                {
                    controlPointHandler.QuartetIndex = GetLastQuartetIndex();
                }
                if (controlPoint.QuartetIndex > 0)
                {
                    controlPointHandler.QuartetIndex = controlPoint.QuartetIndex - 1;
                }
            } 
            else if (controlPoint.ControlPointIndex == 2)
            {
                controlPointHandler.ControlPointIndex = 1;
                if (controlPoint.QuartetIndex < GetLastQuartetIndex())
                {
                    controlPointHandler.QuartetIndex = controlPoint.QuartetIndex + 1;
                }
                else
                {
                    controlPointHandler.QuartetIndex = 0;
                }
            }

            return controlPointHandler;
        }

        private int GetLastQuartetIndex()
        {
            return controlPointQuartetCollection.NumberOfQuartets - 1;
        }

        public ControlPointHandlerId FindPathPointOfControlPoint(ControlPointHandlerId controlPoint)
        {
            var relatedControlPoint = new ControlPointHandlerId();

            if (controlPoint.ControlPointIndex == 1)
            {
                relatedControlPoint.ControlPointIndex = 0;
            }
            else if (controlPoint.ControlPointIndex == 2)
            {
                relatedControlPoint.ControlPointIndex = 3;
            }

            relatedControlPoint.QuartetIndex = controlPoint.QuartetIndex;
            return relatedControlPoint;
        }

        public ControlPointHandlerId[] FindControlPointsOfPathPoint(ControlPointHandlerId pathPoint)
        {
            var relatedControlPoints = new ControlPointHandlerId[2];

            if (pathPoint.ControlPointIndex == 0 )
            {
                relatedControlPoints[0] = new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = pathPoint.QuartetIndex };

                if (pathPoint.QuartetIndex == 0)
                {
                    relatedControlPoints[1] = new ControlPointHandlerId { ControlPointIndex = 2, QuartetIndex = GetLastQuartetIndex() };
                }
                else
                {
                    relatedControlPoints[1] = new ControlPointHandlerId { ControlPointIndex = 2, QuartetIndex = pathPoint.QuartetIndex - 1 };
                }
            }
            else if (pathPoint.ControlPointIndex == 3)
            {
                relatedControlPoints[0] = new ControlPointHandlerId { ControlPointIndex = 2, QuartetIndex = pathPoint.QuartetIndex };

                if (pathPoint.QuartetIndex == 0 && controlPointQuartetCollection.NumberOfQuartets > 1)
                {
                    relatedControlPoints[1] = new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = pathPoint.QuartetIndex + 1 };
                }
                else
                {
                    if (pathPoint.QuartetIndex == controlPointQuartetCollection.NumberOfQuartets - 1)
                    {
                        relatedControlPoints[1] = new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = 0 };
                    }
                    else
                    {
                        relatedControlPoints[1] = new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = pathPoint.QuartetIndex + 1 };
                    }
                }
            }
            
            return relatedControlPoints;
        }

        public ControlPointHandlerId[] GetControlPointPairs()
        {
            var lineList = new List<ControlPointHandlerId>();

            lineList.Add(new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = 0 });
            lineList.Add(new ControlPointHandlerId { ControlPointIndex = 2, QuartetIndex = GetLastQuartetIndex() });

            if (controlPointQuartetCollection.NumberOfQuartets > 1)
            {
                for (int quartetIndex = 0; quartetIndex < controlPointQuartetCollection.NumberOfQuartets - 1; quartetIndex++)
                {
                    lineList.Add(new ControlPointHandlerId { ControlPointIndex = 2, QuartetIndex = quartetIndex });
                    lineList.Add(new ControlPointHandlerId { ControlPointIndex = 1, QuartetIndex = quartetIndex + 1 });
                }
            }

            return lineList.ToArray();
        }
    }
}