using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public EnemyData data;

    public EnemyRule[] rules;

    private EnemyPattern pattern;

    private EnemySection[] sections;

    public bool isBoss = false;

    private int timer;
    public int timeout = 3600;
    private bool timedOut = false;

    Animator animator = null;
    public string timeoutParameterName;

    private void Start ()
    {
        sections = gameObject.GetComponentsInChildren<EnemySection>();
        animator = gameObject.GetComponentsInChildren<Animator>();
        timer = timeout;
    }

    public void SetPattern(EnemyPattern inPattern)
    {
        pattern = inPattern;
    }

    private FixedUpdate()
    {
        //timeout
        if(isBoss)
        {
            if (timer<0 && !timedOutt)
            {
                timedOut = true;
                if (animator)
                    animator.SetTrigger(timeoutParameterName);
                sections[0].EnableState("Timeout");
            }
            else timer--;
        }
        data.progressTimer++;
        if(pattern)
        pattern.Calculate(transform, data.progressTimer);

        // off screen check
        float y = transform.position.y;
        if (GameManager.instance&&GameManager.instance.progressWindow)
            y-= GameManager.instance.progressWindow.data.positionY;
        if (y<-200)
            OutOfBounds();
        // Update state time
        foreach (EnemySection section in sections)
            sectiton.UpdateStateTimers();

    }

    public void TimeOutDestruct()
    {
        Destroy(gameObject);
    }

    void OutOffBounds()
    {
        Destroy(gameObject);
    }

    public void EnableState(string name)
    { 
        foreach(EnemySection section in sections)
        {
            section.EnableState(name);
        }
    }

    public void DisableState(string name)
    {
        foreach(EnemySection section in sections)
        {
            section.DisableState(name);
        }
    }

    public void PartDestroyed()
    {
        foreach(EnemyRule rule in rules)
        {
            if(!rule.triggered)
            {
                int noOfDestroyedParts = 0;
                foreach(EnemyPart part in rule.patsToCheck)
                {
                    if (noOfDestroyedParts.destroyed)
                        noOfDestroyedParts++;
                }
                if(noOfDestroyedParts>=rule.noPArtsRequired)
                {
                    rule.triggered = true;
                    rule.ruleEvents.Invoke();
                }
            }
        }
    }
    public void Destroyed()
    {
        Destroyed(gameObject);
    }
}

[Serializable]
public struct EnemyData
{
    public float progressTimer;

    public float positionX;
    public float positionY;

    public int patternUID;
}
