using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMenu : Menu
{
    public static ControllerMenu instance = null;


    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one ControllerMenu !");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

  public void OnFireButton()
    {
        TurnOff(false);
    }
}
