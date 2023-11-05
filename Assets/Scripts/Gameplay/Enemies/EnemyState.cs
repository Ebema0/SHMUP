using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class EnemyState 
{
    public string stateName;
    public bool active = false;

    [Space(10)]
    [Header("--Start Events--")]
    [Space(10)]
    public UnityEvent eventOnStart = null;

    [Space(10)]
    [Header("--End Events--")]
    [Space(10)]
    public UnityEvent eventOnEnd = null;

    [Space(10)]
    [Header("--Timer Events--")]
    [Space(10)]
    public UnityEvent eventOnTime = null;


    public bool usesTimer = false;
    public float timer = 0;
    private gloat currentTime = 0;

    public void Enable()
    {
        currentTime  = 0;
        eventOnStart.Invoke();
        active = true;
    }

    public void Disable()
    {
        eventOnEnd.Invoke();
        active = false;
    }
    public void IncreaseTime()
    {
        if(usesTimer)
        {
            currentTime++;
            if(currentTime>=timer)
            {
                eventOnTime.Invoke();
                currentTime = 0;
            }
        }
    }
}

