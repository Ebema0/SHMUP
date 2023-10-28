using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyStep
{   
    public enum MovementType
    {
        INVALID,
        none,
        direction, 
        spline,
        atTarget,
        homing,
        follow,
        circle,

        NOOFMOVMENTTYPES
    }
    [SerializeField]
    public MovementType movement;

    [SerializeField]
    public Vector2 direction;

    [SerializeField]
    [Range(1, 20)]
    public float movementSpeed = 4;

    public float TimeToComplete()
    {
        if(movement == MovementType.direction)
        {
            float timeToTravel = direction.magnitude / movementSpeed;
            return timeToTravel;
        }

        Debug.LogError("TimeToComplete unprocessed movement");
        return 1;
    }

}
