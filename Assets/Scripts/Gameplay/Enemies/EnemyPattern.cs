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
    public void Spawn()
    {
        if (spawnedEnemy == null)
        {
            spawnedEnemy = Instatiate(enemyPrefab, transform, position, transform.rotation).GetComponent<Enemy>();
            spawnedEnemy.SetPattern(this);
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
    public Vector 2 CalculatePosition(float progressTimer)
    {
        currentStateIndex = WhichStep(progressTimer);
        if (currentStateIndex>0) return spawnedEnemy.transform.position;

        EnemyStep step = steps[currentStateIndex];

        float stepTime = progressTimer - StartTime(currentStateIndex);
        Vector3 startPos = EndPosition(currentStateIndex-1);
        return step.CalculatePosition(startPos, stepTime);
    }

    public Quaternion CalculateRotation(float progressTimer)
    {
        return Quaternion.identity;
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
}
