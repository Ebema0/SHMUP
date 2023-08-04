using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOptionsMenu : Menu
{
    public static AudioOptionsMenu instance = null;

    void Start()
    {
        if(instance)
        {
            Debug.LogError("Trying to create more than one AudioOptionsMenu");
            Destroy(gameObject);
            return;

        }
        instance= this;
    }

    public void BackButton()
    {
        TurnOff(false);
        MainMenu.instance.TurnOn(this);
    }
    
}
