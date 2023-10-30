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

    [SerializeField]
    public float framesToWait = 30;

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

    public Vector2 EndPosition(Vector3 starPosition)
    {
        Vector result = starPosition;
        if(movement == MovementType.direction)
        {
            result += direction;
            return result;
        }
        else if (movement == MovementType.none)
        {
            return result;
        }

        Debug.LogError("TimeToComplete unprocessed movement");
        return result;
    }

    public Vector3 CalculatePosition(Vector2 startPos,float stepTime)
    {
        if(movement == MovementType.direction)
        {
            float timeToTravel = direction.magnitude / movementSpeed;
            float ratio = stepTime / timeToTravel;
            Vector2 place = startPos + (direction * ratio);
            return place;
        }
        else if (movement == MovementType.none)
        {
            return startPos;
        }

        Debug.LogError("CalculatePosition unprocessed movement tpe, returning startPosition");
        return startPos;
    }

    public void FireActiveStates(Enemy enemy)
    {
        foreach(string state in activeStates)
        {
            enemy.EnableState(state);
        }
    }

    public void FireDeactiveStates(Enemy enemy)
    {
        foreach (string state in deActiveStates)
        {
            enemy.DisableState(state);
        }
    }

}
