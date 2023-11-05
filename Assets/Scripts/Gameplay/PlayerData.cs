using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData 
{
    
    public int score = 0;
    public int stageScore = 0;
    public byte lives = 3;

    public int chain = 0;

    public byte chainTimer = 0;
    public const byte MAXCHAINTIMER = 200;

    // todo add other playthrough stats
}
