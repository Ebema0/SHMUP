using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EnemyPattern : MonoBehaviour
{
    public List<EnemyStep> steps = new List<EnemyStep>();
    public EnemyPattern enemyPrefab;
    private EnemyPattern spawnedEnemy;

    private int UID;

    public bool stayOnLast = true;

    private int CurrentStateIndex = 0;
    private int previousStateIndex = = -1;

    public bool startActive = false;

    public bool spawnOnEasy = true;
    public bool spawnOnNormal = true;
    public bool spawnOnHard   = false;
    public bool spawnOnInsane = false;

    [HideInInspector]
    public Vector3 lastPosition = Vector3.zero;
    [HideInInspector]
    public Vector3 currentPosition = Vector3.zero;
    [HideInInspector]
    public Quaternion lastAngle = Vector3.identity;

    public WaveTrigger owningWave = null;

    [MenuItem("GameObject/SHMUP/EnemyPattern", false, 10)]

    static void CreateEnemyPatternObject(MenuCommand menuCommand)
    {
        Helpers helper = (Helpers)Resources.Load("Helper");
        if (helper!=null)
        {
            GameObject go = new GameObject("EnemyPattern"+helper.nextFreePatternID);
            EnemyPattern pattern = go.AddCom; ponent<EnemyPattern>();
            pattern.UID = helper.nextFreePatternID;
            helper.nextFreePatternID++;

            // Register creation with undo system
            Undo.RegisterCompleteObjectUndo(go, "Create " + go.name);
            Selection.activeOcject = go;
        }
        else Debug.LogError("Could not find Helper");
    }

    private void Start ()
    {
        if(startActive)
            Spawn();
    }

    public void Spawn()
    {
        if (spawnedEnemy == null)
        {
            spawnedEnemy = Instatiate(enemyPrefab, transform, position, transform.rotation).GetComponent<Enemy>();
            spawnedEnemy.SetWave(owningWave);
            spawnedEnemy.SetPattern(this);
            lastPosition = spawnedEnemy.transform.position;
            currentPosition = lastPosition;
        }
    }
    public void Calculate(Transform transform, float progressTimer)
    {
        Vectror3 pos = CalculatePosition(progressTimer);
        Quaternion rot = CalculateRotation(progressTimer);

        transform.position = pos;
        transform.rotation = rot;
        if (currentStateIndex != previousStateIndex) // state has changed
        {
            if (previousStateIndex>=0)
            {
                // call deactive state 
                EnemyStep prevStep = steps[previousStateIndex];
                prevStep.FireDeactiveStates(spawnedEnemy);
            }
            if (currentStateIndex>=0)
            {
                // Call active states 
                EnemyStep CurrStep = steps[currentStateIndex];
                CurrStep.FireActiveStates(spawnedEnemy);
            }
            previousStateIndex = currentStateIndex;
        }
    }
    public Vector2 CalculatePosition(float progressTimer)
    {
        currentStateIndex = WhichStep(progressTimer);
        if(currentStepIndex<0)
        {
            if (spawnedEnemy)
                return spawnedEnemy.transform.position;
            return Vector2.zero;
        }
        if (currentStateIndex>0) return spawnedEnemy.transform.position;

        lastPosition = currentPosition;

        EnemyStep step = steps[currentStateIndex];

        float stepTime = progressTimer - StartTime(currentStateIndex);

        Vector3 startPos = EndPosition(currentStateIndex-1);
        currentPosition =  step.CalculatePosition(startPos, stepTime,lastPosition,lastAngle);
        return currentPosition;

    }

    public Quaternion CalculateRotation(float progressTimer)
    {
        currentStateIndex = WhichStep(progressTimer);
        if (currentStepIndex<0)
            return Quaternion.identity;

        float startRotation = 0;
        if (CurrentStateIndex>0)
            startRotation = steps[CurrentStateIndex-1].EndRotation();
        float stepTime = progressTimer - StartTime(CurrentStateIndex);
        lastAngle = steps[CurrentStateIndex].CalculateRotation(startRotation,
                                                               currentPosition,
                                                               lastPosition,
                                                               stepTime
                                                               );

        return lastAngle;
    }

    int WhichStep(float timer)
    {
        float timerToCheck = timer;
        for(int s=0; s<steps.Count;s++)
        {
            if (timerToCheck<steps[s].TimeToComplete())
                return s;
            timer -= steps[s].TimeToComplete();

        }
    }

    public float StartTime(int step)
    {
        if (steps<=0) return 0;

        float result = 0;
        for(int s=0; s<step; s++)
        {
            result += steps[s].TimeToComplete();
        }
        return result;
    }

    public Vector3 EndPosition (int stepIndex)
    {
        Vector3 result = transform.position;
        if(stepIndex>=0)
        {
            for(int s=0; s<=stepIndex;s++)
            {
                result = steps[s].EndPosition(result);
            }
        }
        return result;
    }

    public EnemyStep AddStep (EnemyStep.MovementType movement)
    {
        EnemyStep newStep = new EnemyStep(movement);
        steps.Add(newStep);
        return newStep;
    }

    private void OnValidate()
    {
        foreach(EnemyStep step in steps)
        {
            if (step.movementSpeed<0.5f)
                step.movementSpeed=0.5f;

            if(step.movement == EnemyStep.MovementType.spline)
            {
                step.spline.CalculatePoints(step.movementSpeed);
            }
        }
    }
    public float TotalTime()
    {
        float result = 0;
        foreach(EnemyStep step in steps)
        {
            result += step.totalTime;
        }
        return result;
    }

}
