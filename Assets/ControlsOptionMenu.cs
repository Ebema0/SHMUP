using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsOptionMenu : Menu
{
    public static ControlsOptionMenu instance = null;

    void Start()
    {
     if(instance)
        {
            Debug.LogError("trying to create more than one ControlsOptionsMenu!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void OnBackButton()
    {
        TurnOff(true);
    }
}
