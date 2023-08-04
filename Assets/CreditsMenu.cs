using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : Menu
{
    public static CreditsMenu instance = null;

    void Start()
    {
        if(instance)
        {
            Debug.LogError("Trying to create more than one CreditMenu !");
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
