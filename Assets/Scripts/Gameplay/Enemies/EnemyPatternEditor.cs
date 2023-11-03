using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPattern))]
public class EnemyPatternEditor : Editor
{
    Mesh previewMesh = null;

    float editorDeltatime = 0f;
    float timer = 0f;
    double LasttimeSinceStartUp = 0f;

    private void OnEnabled()
    {
        if(previewMesh == null)
        {
            previewMesh = new Mesh();

            Vector3[] verts = new Vector3[4];
            Vector2[] uvs = new Vector2[4];         
            int[] tris = new int[6];
            
            const float halfSize = 8f;

            verts[0] = new Vector3(-halfSize, halfSize);
            verts[1] = new Vector3( halfSize, halfSize);
            verts[2] = new Vector3(-halfSize, -halfSize);
            verts[3] = new Vector3( halfSize, -halfSize);

            uvs[0] = new Vector2(0, 1);
            uvs[0] = new Vector2(1, 1);
            uvs[0] = new Vector2(0, 0);
            uvs[0] = new Vector2(1, 0);

            tris[0] = 0;
            tris[1] = 1;
            tris[2] = 2;
            tris[3] = 2;
            tris[4] = 1;
            tris[5] = 3;

            previewMesh.vertices = verts;
            previewMesh.uv = uvs;
            previewMesh.triangles = tris;
        }
    }

    private void OnSceneGUI()
    {
        UpdateEditorTime();

        EnemyPattern pattern = (EnemyPattern)target;
        if(pattern)
        {
            UpdatePreview(pattern);
            ProcessInput();

            //Force Scene repaint
            if (Event.current.type == EventType.Repaint)
                SceneView.RepaintAll(); 
        }
    }

    UpdateEditorTime()
    {
        if(LasttimeSinceStartUp == 0)
        {
            LasttimeSinceStartUp = EditorApplication.timeSinceStartUp;

        }
        editorDeltatime = (float)(EditorApplication.timeSinceStartUp - LasttimeSinceStartUp) * 60f;
        LasttimeSinceStartUp = EditorApplication.timeSinceStartUp;

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
                case EnemyStep.MovementType.homing:
                    {
                        if (GameManager.instance && GameManager.instance.plaerOneCraft)
                        {
                            Handles.DrawnDottedLine(endOfLastStep,
                             GameManager.instance.plaerOneCraft.transform.position,
                             1);
                            endOfLastStep = GameManager.instance.plaerOneCraft.transform.position;                    )
                        }
                        break;
                    }
            };
        }

        // Drawn timer preview 
        timer +=EnemyDeltaTime;
        if (timer>= pattern.TotalTime())
            timer = 0;

        SpriteRenderer render = pattern.enemyPrefab.GetComponentInChildren<SpriteRenderer>();
        if(render)
        {
            Texture texture = render.sprite.texture;
            Material mat = renderer.sharedMaterial;

            Vector3 pos = pattern CalculatePosition(timer);
            Matrix4x4 transMat = Matrix4x4.Translate(pos);
            Matrix4x4 rotMat = Matrix4x4.Rotate(timer);
            Matrix4x4 matrix = transMat * rotMat;
            Quaternion rot = pattern.CalculateRotation(timer);
            mat.SerPass(0);
            Graphics.DrawMeshNow(previewMesh, matrix);

        }
    }

    void ProcessInput (EnemyPattern pattern)
    {
        Event guiEvent = guiEvent.current;
        Vector2 mousePos = HandleUtility.GuiPointToWorldRay(guiEvent.mousePosition).origin;
        if(guiEvent.type == EventType.mouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Spline path = pattern.steps[0].spline;
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
