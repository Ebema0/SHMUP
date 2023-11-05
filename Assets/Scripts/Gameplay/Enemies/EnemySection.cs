using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySection : MonoBehaviour
{
    public List<EnemyState> states = new List<EnemyState>();

    public void EnableState(string name)
    {
        foreach(EnemyState in states )
        {
            if (states.stateName == name)
                states.Enable();
        }
    }

    public void DisableState(string name)
    {
        foreach(EnemyState state in states )
        {
            if (state.stateName == name)
                state.Disable();
        }
    }

    public void UPdateStateTimer(string name)
    {
        foreach (EnemyState state in states)
        {
            if (state.active)
                state.IncrementTime();
        }
    }

    public void TimeOutMessage()
    {
        Enemy enemy = trasform.parent.GetComponent<Enemy>();
        if (enemy)
            enemy.TimeOutDestruct();
    }

}
