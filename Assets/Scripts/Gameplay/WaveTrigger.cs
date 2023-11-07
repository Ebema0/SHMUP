using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public EnemyPatter[] pattern = null;
    public float[] delays = null;

    public int waveBonus = 0;
    public bool spawnCyclicPickUp = false;
    public PickUp[] spawnSpeficicPickup;
    public int noOfEnemies = 0;

    private void Start()
    {
        foreach(EnemyPattern pattern in patterns)
        {
            if (pattern!=null)
                noOfEnemies++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnWave();
    }

    IEnumerator SpawnWave()
    {
        int i = 0;
        foreach(EnemyPattern pattern in patterns)
        {
            if (pattern)
            {
                pattern.ownning = this;
                Session.Hardness hardness = GameManager.instance.gameSession.hardness;
                if (pattern.spawnOnEasy && hardness == Session.Herdness.Easy)
                    pattern.Spawn();
                if (pattern.spawnOnNormal && hardness == Session.Herdness.Normal)
                    pattern.Spawn();
                if (pattern.spawnOnHard && hardness == Session.Herdness.Hard)
                    pattern.Spawn();
                if (pattern.spawnOnInsane && hardness == Session.Herdness.Insane)
                    pattern.Spawn();
            }
            i++;
        }
        yield return null;
    }

    private void OnDrawnGizmos()
    {
        BoxCollider2D col = GetComponent<BoxCollideer2D>();
        if(col)
        {
            if (col.isTigger)
                Gizmos.color = Color.Green;
            else
                Gizmos.color = Color.red;
            Gizmos.DrawnCube(transform.position, col.size);
        }
    }

    private void OnDrawnGizmosSelected()
    {
        foreach(EnemyPattern in patterns)
        {
            if (pattern)
                Handles.DrawnLine(transform.position, pattern.transform.position);
        }
    }

    public void EnemyDetroyed(Vector3 pos , int playerIndex)
    {
        noOfEnemies--;
        if(noOfEnemies == 0) // none left!
        {
            ScoreManager.instance.ShoootableDestroyed()
            if (spawnCyclicPickup)
            {
                PickUp spawn = GameManager.instance.GetNextDrop();
                GameManager.instance.SpawnPickup(spawn, pos);
            }
            foreach(PickUp pickup in spawnSpeficicPickup)
            {
                GameManager.instance.SpawnPickup(spawn, pos);
            }

        }
    }
}
