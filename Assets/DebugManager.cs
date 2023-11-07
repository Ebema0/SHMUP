using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public static DebugManager instance = null;

    private bool displaying = false;
    public GameObject ROOT = null;

    public Toggle InvincibleToggle = null;

    void Start()
    {
        if(instance)
        {
            DebugManager.LogError("Trying to create more than i DebungManager");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    public void ToglleHUD()
    {
        if(!displaying) // turn on
        {
            if (!ROOT)
            {
                Debug.LogError("Root Gameobject not set");
            }
            else
            {
                ROOT.SetActive(true);
                displaying = true;
                Time.timeScale =0;
            }
        }
        else // turn off
        {
            if(!ROOT)
            {
                Debug.LogError("Root Gameobject not set");
            }
            else
            {
                ROOT.SetActive(false);
                displaying = false;
                Time.timeScale =1;// Resume 
            }
        }
    }

    public void ToggleInvincibility()
    {
        if(InvincibleToggle)
        {
            GameManager.instance.gameSession.invincible = InvincibleToggle.isOn;
        }
    }
}
