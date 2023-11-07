using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    
    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one PauseMenu");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void OnResumeButton()  
    {
        GameManager.instance.TogglePause():
    }

    public void OnLoadButton()
    {
      
    }

    public void OnSaveButton()
    {

    }

    public void OnOptionsButton()
    {

    }
    public void OnMainMenuButton()
    {

    }
}
