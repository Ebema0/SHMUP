using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsOptionMenu : Menu
{
    public static ControlsOptionMenu instance = null;

    public Button[] p1_buttons = new Button[8];
    public Button[] p2_buttons = new Button[8];
    public Button[] p1_keys = new Button[12];
    public Button[] p2_keys = new Button[12];

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
