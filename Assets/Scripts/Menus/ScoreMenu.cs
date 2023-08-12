using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMenu : Menu
{
    public static ScoreMenu instance = null;

    void Start()
    {
        if(instance)
        {
            Debug.LogError("Trying to create more than one ScoreMenu");
            Destroy(gameObject);
            return;
        }
        instance = this; 
    }

    public void OnBackButton()
    {
        TurnOff(true);
    }
   
   public void OnRightButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnLeftButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
}
