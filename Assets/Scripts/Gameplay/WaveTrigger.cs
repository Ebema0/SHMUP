using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public EnemyPatter[] pattern = null;

    private void OnTriggerEnter2D(Collider2D colission)
    {
        SpawnWave();
    }

    public SpawnWave()
    {
        foreach(Enemypattern pattern in patterns)
        {
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
    }
}
