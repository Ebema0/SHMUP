using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

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

    public void Save(BinaryWriter writer)
    {
        writer.Write(score);
        writer.Write(lives);

        
    }

    
    public void Load(BinaryReader reader)
    {
        score=reader.ReadInt32();
        lives = reader.ReadByte();
        
    }

    public void ResetData()
    {
        score =0;
        stageScore = 0;
        lives = 3;
        chain =0;
        chainTimer = 0;
    }

}
