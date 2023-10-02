using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CraftConfig", menuName = "SHMUP/CraftConfiguration")]
public class CraftConfiguration : ScriptableObject

{
    const int MAX_SHOT_POWER = 10;
    public float speed;
    public float bulletStrenght;
    public float beamStrenght;
    public Sprite craftSprite;

    public ShotConfiguration[] shotLevel = new ShotConfiguration[MAX_SHOT_POWER];
}

[Serializable]
public class ShotConfiguration
{
    public int[] spawnerSizes = new int[5];
}
