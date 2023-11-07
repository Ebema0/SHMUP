using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{

    public static MainMenu instance = null; 

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

    public void OnQuitButton()
    {
        TurnOff(true);

    }
    public void OnPlayButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnPracticeButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnOptionsButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnScoresButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnMedalsButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnReplaysButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnCreditButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
}
