using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenu : Menu
{
    public static PlayMenu instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one PlayMenu!");
            Destroy(gameObject);
            return;
        }
        instance = this;
        
    }
     
    public void OnNormalButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnBullHellButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnBackButton()
    {
        TurnOff(true);
        
    }


}
