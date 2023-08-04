using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSelectMenu : Menu
{
    public static CraftSelectMenu instance = null;

    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one MainMenu");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
 
    public void OnPlayButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnBackButton()
    { 
        TurnOff(true);
      
    }
}
