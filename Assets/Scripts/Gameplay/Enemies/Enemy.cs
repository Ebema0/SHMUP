using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
    public EnemyData data;

    private EnemyPattern pattern;

    private EnemySection[] sections;

    private void Start ()
    {
        sections = gameObject.GetComponentsInChildren<EnemySection>();
    }

    public void SetPattern(EnemyPattern inPattern)
    {
        pattern = inPattern;
    }

    private FixedUpdate()
    {
        data.progressTimer++;
        if(pattern)
        pattern.Calculate(transform, data.progressTimer);

        // off screen check
        float y = transform.position.y;
        if (GameManager.instance&&GameManager.instance.progressWindow)
            y-= GameManager.instance.progressWindow.data.positionY;
        if (y<-200)
            OutOfBounds();
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
}

[Serializable]
public struct EnemyData
{
    public float progressTimer;

    public float positionX;
    public float positionY;

    public int patternUID;
}
