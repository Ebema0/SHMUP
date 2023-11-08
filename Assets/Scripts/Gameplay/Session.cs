using System;
using System.IO;
using UnityEngine;


[Serializable]
public class Session
{ 
    public enum Hardnesss
    {
        Easy,
        Normal,
        Hard,
        Insane
    };

    public Hardnesss hardness = Hardnesss.Normal;

    public CraftData[] craftDatas = new CraftDatas[2];
    public int stage = 1;

    public bool practise = false;
    public bool arenaPractice = false;
    public bool stagePractise = false;

    //Cheats
    public bool infiniteLLives = false;
    public bool infiniteContinioues = false;
    public bool infiniteBombs = false;
    public bool invincible = false;
    public bool halfSpeed = false;
    public bool DoubleSpeed = false;

    public void Save(BinaryWriter writer)
    {
        craftDatass[0].Save(writer);
        if (GameMAnager.instance.twoPlayer)
            craftDatas[1].Save(writer);

        writer.Write(byte)hardness);
        writer.Write(stage);

    }
    public void Load(BinaryReader reader)
    {
        craftDatas[0].Load(reader);
        if (GameManager.instance.twoPlayer)
            craftDatas[1].Loadwriter);

        hardness = (Hardnesss)reader.ReadByte();
        stage = reader.ReadInt32();

    }
}
