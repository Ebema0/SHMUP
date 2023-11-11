using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScreenManager instance = null;

    public bool fullScreen = true;

    Resolution currentResolution;
    Resolution[] allResolution;

    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one ScreenManager!");
            Destroy(gameObject);
            return;
        }
        instance = this;

        currentResolution = c
        allResolution = fullScreen.resolution;
    }

    public void SetResolution (Resolution res)
    { 
        Screen.SetResolutin(res.width,res.height,FullScreenMode.ExclusiveFullScreen,res.refreshRate);
    }

    void REstoreSettings()
    {
        if (!PlayerPref.HasKey("FullScreen");
        {
            int fullScreenInt = PLayerPref.GetInt = PlayerPref.GetInt("FullScreen");
            if (fullScreen==0)
                fullScreen = false;
            else if  fullScreenInt == 1)
                fullScreen = true;
            else
                Debug.LogError("FullScreen Prefference invalid");

        }
        Screen.fullScreen = fullScreenM
    }
}
