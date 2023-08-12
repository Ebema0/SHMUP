using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsOptionMenu : Menu
{
    public static GraphicsOptionMenu instance = null;
    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one GraphicsOptionMenu!!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void OnFullScreenButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnVSyncButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnApplyButton()
    {
        TurnOff(this);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnBackButton()
    {
        TurnOff(true);
    }

}
