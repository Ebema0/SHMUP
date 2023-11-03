using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
  public static Vector2 CalculateQuadratic(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 p0 = Vector2.Lerp(a, b, t);
        Vector2 p1= Vector2.Lerp(b, c, t);
        Vector2 result = Vector2.Lerp(p0,p1, t);
        return result;
    }
    public static Vector2 CalculateCubic(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 p0 = CalculateQuadratic(a, b, t);
        Vector2 p1 = CalculateQuadratic(b, c, t);
        Vector2 result = Vector2.Lerp(p0, p1, t);
        return result;
    }

}
