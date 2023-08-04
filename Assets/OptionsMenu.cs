using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : Menu
{
    public static OptionsMenu instance = null;

    void Start()
    {
        if(instance)
        {
            Debug.LogError("Trying to create more than one OPTIONSMENU !!");
            Destroy(gameObject);
            return;
            
        }
        instance = this;
    }

    public void OnGraphicsButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    
    public void OnAudioButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }

    public void OnControlsButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }

    public void OnBackButton()
    {
        TurnOff(true);
    }
  
}
