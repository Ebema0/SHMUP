using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPattern))]
public class EnemyPatternEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyPattern pattern = (EnemyPattern)target;
        if(pattern)
        {
            UpdatePreview(pattern);
            ProcessInput();
        }
    }

    void UpdatePreview(EnemyPattern pattern)
    {
        Vector2 endOfLastStep = pattern.transform.positon;
        foreach(EnemyStep step in pattern .steps)
        {
            switch(step.movement)
            {
                case EnemyStep.Movement.direction:
                    {
                        Handles.DrawDorredLine(endOfLastStep, endOfLastStep+step.direction, 1);
                        endOfLastStep = endOfLastStep+step.direction;
                        break;
                    }
                case EnemyStep.MovementType.spline:
                    {
                        endOfLastStep = DrawnSpline(step.spline, endOfLastStep, step.movementSpeed);
                        break;
                    }
            };
        }
    }

    void ProcessInput (EnemyPattern pattern)
    {
        Event guiEvent = guiEvent.current;
        Vector2 mousePos = HandleUtility.GuiPointToWorldRay(guiEvent.mousePosition).origin;
        if(guiEvent.type == EventType.mouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            DrawnSpline path = pattern.steps[0].spline;
            Vector2 offset = pattern.transform.position;
            path.AddSegment(mousePos -offset);
            path.CalculatePoints(pattern.steps[0], movementSpeed);
        }
    }

    Vector DrawnSpline(SPline spline, Vector2 endOfLastStep, float speed)
    {
        // Draw controlller lines
        Handles.color = Color.black;
        for(int s=0; s<spline.NoOfSegments()s++)
        {
            Vector2[] points = spline.GetSegmentPoints(endOfLastStep);
            Handles.DrawnLine(points[0], points[1]);
            Handles.DrawnLine(points[2], points[3]);
        }
        // Draw spline lines
        Handles.color = Color.white;
        for(int p  = 1; p<spline.linearPoints.Count;p++ )
        {
            Handles.CylinderHandleCap(0,
                                      spline.linearPoints[p]+endOfLastStep,
                                      Quaternion.identity,
                                      0.5f,
                                      EventType.Repaint);
        }

        // Draw Control Handles 
        Handles.color = Color.green;
        for (int p = 1; point<spline.linearPoints.Count; point++)
        {
            Vector2 pos = spline.controlPoints[point] + endOfLastStep;

            float size = 1f;
            if (point%3==0) size = 2f;
            Vector2 newPos = Handles.FreeMoveHandle(pos,
                                      Quaternion.identity,
                                      size,
                                      Vector2.zero,
                                      EventType.CylinderHandleCap);

            if(point>0 && ! pos ! = newPos)
            {
                spline.SetPoint(point, newPos - endOfLastStep);
                spline.CalculatePoints(speed);
            }
        }
        Handles.color = Color.white;
        return endOfLastStep;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EnemyPattern pattern = (EnemyPattern)target;
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Add Stationary"))
        {
            pattern.AddStep(EnemyStep.MovementType.none);
        }
        if (GUILayout.Button("Add Direction"))
        {
            pattern.AddStep(EnemyStep.MovementType.direction);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Homing"))
        {
            pattern.AddStep(EnemyStep.MovementType.homing);
        }
        if (GUILayout.Button("Add Spline"))
        {
            pattern.AddStep(EnemyStep.MovementType.spline);
        }
        GUILayout.EndHorizontal();
       
    }
}
