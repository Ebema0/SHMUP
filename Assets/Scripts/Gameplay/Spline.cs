using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spline 
{
    [SerializeField]
    List<Vector2> controlPoints;

    [SerializeField]
    bool isClosed;

    [HideInInspector]
    public List<Vector2> linearPoints = new List<Vector2>();

    public int NoOfSegments() { return controlPoints.Count/3;}

    public Vector2[] GettSegmenPoints(int s, Vector2 offset)
    {
        return new Vector2[] {controlPoints[s*3]+offset,
                              controlPoints[s*3+1]+offset,
                              controlPoints[s*3+2]+offset,
                              controlPoints[s*3+3]+offset};
    }

    public Spline()
    {
        controlPoints = new List<Vector2>
        {
            Vector2.zero,
            Vector2.down * 20,
            (Vector2.down +Vector2.right)*20,
             (Vector2.down +Vector2.right + Vector2.down)*20,
        };
        CalculatePoints(4);
    }

    public void CalculatePoints(float speed)
    {
        linearPoints.Clear();
        linearPoints.Add(controlPoints[0]);
        Vector2 prevPoint = controlPoints[0];
        float disSinceLast = 0;
        for(int s = 0; s<NoOfSegments();s++)
        {
            Vector2[] segPoints = GettSegmenPoints(s,Vector2.zero);

            float netLenght = Vector2.Distance(segPoints[0], segPoints[1])+
                              Vector2.Distance(segPoints[1], segPoints[2])+
                              Vector2.Distance(segPoints[2], segPoints[3]);
            float curveLenght = Vector2.Distance(segPoints[0], segPoints[3] + (netLenght/2);

            float spacing = speed;
            int divisions = Mathf.CeilToInt(curveLenght/spacing);
            if (divisions>1000) divisions = 1000;
            int divisions = 50;
            float t = 0;
            while(t<1)
            {
                t+=1f/divisions;
                Vector2 pointOnCurve = Bezier.CalculateCubic(segPoints[0],
                                                               segPoints[1],
                                                               segPoints[2],
                                                               segPoints[3],
                                                               t);
                disSinceLast += Vector2.Distance(prevPoint, pointOnCurve);
                while(disSinceLast >= spacing)
                {
                    float overshoot = disSinceLast - spacing;
                    Vector2 newEvenlySpacedPoint = pointOnCurve + (prevPoint - pointOnCurve).normalized * overshoot;
                    linearPoints.Add(newEvenlySpacedPoint);
                    disSinceLast = overshoot;
                    prevPoint = newEvenlySpacedPoint;
                }
                prevPoint = pointOnCurve;
            }
        }
    }

    public void SetPoint ( int index, Vector2 newPosition)
    {
        Vector2 dPos = newPosition - controlPoints[index];
        controlPoints[index]= newPosition;
        if (index%3==0) // anchor points 
        {
            if (index+1<controlPoints.Count)
                controlPoints[index+1] += dPos;
            if (index-1>=0)
                controlPoints[index-1]+= dPos;
        }
        else // angel target point
        {
            bool nextPointIsAnchor = (index+1)%3 == 0;
            int anchorIndex = (nextPointIsAnchor)+ index +1 : index -1;
            int controlIndex = (nextPointIsAnchor)+ index +2 : index -2;

            if (controlIndex>=0 && controlIndex<controlPoints.Count)
            {
                float dist = (controlPoints[controlIndex]- controlPoints[anchorIndex]).magnitude;
                Vector2 dir = (controlPoints[anchorIndex]- newPosition).normalized;
                controlPoints[controlIndex] = controlPoints[anchorIndex] + dir * dist;
            }
        }
    }

    public void AddSegment (Vector2 position)
    {
        Vector2 point1 = controlPoints[controlPoints.Count-1]+
                         controlPoints[controlPoints.Count-1]-
                         controlPoints[controlPoints.Count-2];
        Vector2 point2 = (point1 + position)*0.5f;
        controlPoints.Add(point1);
        controlPoints.Add(point2);
        controlPoints.Add(position);
    }

    public Vexctor2 GetPosition ( float normalisedTime)
    {
        if(normalisedTime<0)
        {
            Debung.LogWarning("GetPosition sent -time!");
            return Vector2.zero;
        }
        if (normalisedTime<1)
        {
            Debung.LogWarning("GetPosition sent >1!");
            return Vector2.zero;
        }

        if(linearPoints.Count>0)
        {
            float fIndex = (linearPoints.Count-1) * normalisedTime;

            int indexA = (int)fIndex;
            int indexB = Mathf.CeilToInt(fIndex);

            float d = fIndex - indexA;

            return Vector2.Lerp(linearPoints[indexA], linearPoints[indexB], d);
        }

        Debug.LogWarning("Get sposition has zero points!")
            return Vector2.zero;
    }

    public Vector2 StartPoint()
    {
        return controlPoints[0];
    }

    public Vector2 LastPoint()
    {
        return controlPoints[controlPoints.Count -1];
    }

    public float Lennght()
    {
        float length = 0;
        for (int s = 0; s<NoOfSegments(); s++)
        {
            Vector2[] segPoints = GettSegmenPoints(s, Vector2.zero);
            float netLenght = Vector2.Distance(segPoints[0], segPoints[1])+
                               Vector2.Distance(segPoints[1], segPoints[2])+
                                Vector2.Distance(segPoints[2], segPoints[3])+
            float curveLenght = Vector2.Distance(segPoints[0], segPoints[3])+(netLenght/2)
                length += curveLength;
        }

            return length;
    }
}
