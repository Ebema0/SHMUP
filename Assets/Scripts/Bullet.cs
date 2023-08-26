using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
}

[Serializable]

public struct BulletData
{
    public float positionX;
    public float positionY;
    public float dX;
    public float dY;
    public float angle;
    public int type;
    public bool active;


    public BulletData(float inX,
                    float inY,
                    float inDX,
                    float inDY,
                    float inAngle,
                    int intType,
                    bool inActive)
    {
        positionX = inX;
        positionY = inY;
        dX        =inDX;
        dY        =inDY;
        angle     =inAngle;
        type      =intType;
        active    =inActive;
    }

}