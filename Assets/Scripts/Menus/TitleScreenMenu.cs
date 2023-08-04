using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenMenu : Menu
{
    public static TitleScreenMenu instance = null;

    private void Start ()
    {
        if (instance)
        {
            Debug.LogError("Trying to creata more than on TitleScreen!");
            Destroy(gameObject);
            return;

        }
        instance = this;
    }
    public void OnFireButton()
    {
        TurnOff(false);
        MainMenu.instance.TurnOn(this);
    }

}
