using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeMenu : Menu
{
    public static PracticeMenu instance = null;

    void Start()
    {
        if(instance)
        {
            Debug.LogError("Trying to create more than one PracticeMenu!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void OnBackButton()
    {
        TurnOff(true);

    }
    public void OnStageModeButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnArenaModeButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnInfiniteLivesButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnInfiniteContinuesButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnInfiniteBombsButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnInvunerableButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnHalfSpeedButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnDoubleSpeedButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }


}
