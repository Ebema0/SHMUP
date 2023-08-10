using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlsOptionMenu : Menu
{
    public static ControlsOptionMenu instance = null;

    public Button[] p1_buttons = new Button[8];
    public Button[] p2_buttons = new Button[8];
    public Button[] p1_keys = new Button[12];
    public Button[] p2_keys = new Button[12];

    public GameObject bindingPanel = null;
    public Text bindText           = null;
    public EventSystem eventSystem = null;

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

    private void OnEnable()
    {
        UpdateButtons();
    }
    public void OnBackButton()
    {
        TurnOff(true);
    }

    void UpdateButtons()

    { 
        //Joystick Buttons
        for (int b = 0; b <8; b++)
        {
            p1_buttons[b].GetComponentInChildren<Text>().text = InputManager.instance.GetButtonName(0, b);
            p2_buttons[b].GetComponentInChildren<Text>().text = InputManager.instance.GetButtonName(1, b);      
        }
        //Keyboard Buttons
        for (int k = 0;k <8; k++)
        {
            p1_keys[k].GetComponentInChildren<Text>().text = InputManager.instance.GetKeyName(0, k);
            p2_keys[k].GetComponentInChildren<Text>().text = InputManager.instance.GetKeyName(1, k);

        }

        //Key "axes"
        for (int a = 0; a <4; a++)
        {
            p1_keys[a+8].GetComponentInChildren<Text>().text = InputManager.instance.GetKeyAxisName(0, a);
            p2_keys[a+8].GetComponentInChildren<Text>().text = InputManager.instance.GetKeyAxisName(1, a);

        }

    }
}
