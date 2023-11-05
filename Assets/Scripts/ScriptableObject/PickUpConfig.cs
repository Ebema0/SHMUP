using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="PickUpConfig",menuName = "SHMUP/PickUpConfig")]
public class PickUpConfig : ScriptableObject
{
    public PickUpConfig.PickUpType type;

    public int powerLevel = 1;
    public int bombLevel = 1;
    public int medalValue = 100;
    public float fallSPeed = 0;
    public int coinLevel = 1;
    public int medalLevel = 1;
    public int surplusLevel = 1;
}
