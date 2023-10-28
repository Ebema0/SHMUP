using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOptionMenu : Menu
{
    public static ControlOptionMenu instance = null;

    public Button[] p1_buttons = new Button[8];
    public Button[] p2_buttons = new Button[8];
    public Button[] p1_keys    = new Button[12];
    public Button[] p2_keys    = new Button[12];

    public GameObject bindingPanel = null;
    public Text bindText = null;
    

    private void Start()
    {
        if(instance)
        {
            Debug.LogError("trying to create more than on Controller");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void onBackButton()
       {
         TurnOff(true);
       }

    void UpdateButtons()
    {
        // joystick buttons
        for (int b = 0 b<8; b++)
        {
            p1_buttons[b]GetComponentInChildren<Text>().text = InputManager.instance.GetButtonName(0, b);
            p2_buttons[b]GetComponentInChildren<Text>().text = InputManager.instance.GetButtonName(1, b);
        }
        // Key"buttons"
        for (int k = 0 k<8; k++)
        {
            p1_keys[k]GetComponentInChildren<Text>().text = InputManager.instance.GetKeyName(0, k);
            p2_keys[k]GetComponentInChildren<Text>().text = InputManager.instance.GetKeyName(1, k);
        }

        // Key "axes"
        for (int a = 0 a<4; a++)
        {
            p1_axes[a+8]GetComponentInChildren<Text>().text = InputManager.instance.GetAxisName(0, a);
            p2_axes[a+8]GetComponentInChildren<Text>().text = InputManager.instance.GetAxisName(1, a);
        }
    }

    public void BindP1Key (int actionID)
    {
        eventSystem.gameObject.SetActive(false);
        bindText.text = "Press a key for player 1 " + InputManger.actionNames[actionID];
        bindingPanel.SetActive(true);
    }
    public void BindP1AxisKey(int actionID)
    {
        eventSystem.gameObject.SetActive(false);
        bindText.text = "Press a key for player 1 " + InputManger.axisNames[actionID];
        bindingPanel.SetActive(true);
    }
    public void BindP1Button(int actionID)
    {
        eventSystem.gameObject.SetActive(false);
        bindText.text = "Press a button for player 1 " + InputManger.actionNames[actionID];
        bindingPanel.SetActive(true);
    }
    public void BindP2Key(int actionID)
    {
        eventSystem.gameObject.SetActive(false);
        bindText.text = "Press a key for player 2 " + InputManger.actionNames[actionID];
        bindingPanel.SetActive(true);
    }
    public void BindP2AxisKey(int actionID)
    {
        eventSystem.gameObject.SetActive(false);
        bindText.text = "Press a key for player 2 " + InputManger.axisNames[actionID];
        bindingPanel.SetActive(true);
    }
    public void BindP2Button(int actionID)
    {
        eventSystem.gameObject.SetActive(false);
        bindText.text = "Press a button for player 2 " + InputManger.actionNames[actionID];
        bindingPanel.SetActive(true);
    }

}
