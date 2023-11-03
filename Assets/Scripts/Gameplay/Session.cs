using System;
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
}
