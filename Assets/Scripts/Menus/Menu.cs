using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject ROOT = null;
    public Menu previousMenu = null;
    private GameObject previousItem = null;

    public virtual void TurnOn(Menu previous)
    {
        if (ROOT)
        {
            if (previous!=null)
                previousMenu = previous;
            ROOT.SetActive(true);
            if(previousItem)
            {
                EventSystem.current.SetSelectedGameObject(previousItem);
            }
        }
        else
            Debug.LogError("ROOT object not set");
    }

    public virtual void TurnOff(bool returnToPrevious)
    {
        if (ROOT)
        {
            if (EventSystem.current)
                previousItem = EventSystem.current.currentSelectedGameObject;

            ROOT.SetActive(false);

            if(previousMenu && returnToPrevious)
            {
                previousMenu.TurnOn(null);
            }
            
        }
                  
    }
}
