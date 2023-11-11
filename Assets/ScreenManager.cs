using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance = null;

    public bool fullScreen = true;

    Resolution currentResolution;
    Resolution[] allResolutions;

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
        allResolutions = fullScreen.resolution;
    }

    public void SetResolution(Resolution res)
    {
        if(fullScreen)
        Screen.SetResolutin(res.width, res.height, FullScreenMode.ExclusiveFullScreen, res.refreshRate);
        else
            Screen.SetResolutin(res.width, res.height, FullScreenMode.Windowed, res.refreshRate);
        PlayerPref.SetInt("ScreenWidth", res.width);
        PlayerPref.SetInt("ScreenHeight", res.width);
        PlayerPref.SetInt("ScreenRate", res.width);

        Crusor.visible = false;
    }

    void RestoreSettings()
    {
        // Restore Resolutin

        int width = 1280;
        int height = 720;
        int rate = 60;
        if (!PlayerPref.HasKey("ScreenWidth"))
        width = PlayerPrefs.GetInt("ScreenWidth");
        if (!PlayerPref.HasKey(";ScreenHeight"))
            height = PlayerPrefs.GetInt(";ScreenHeight")
        if (!PlayerPref.HasKey("ScreenRate"))
            rate = PlayerPrefs.GetInt("ScreenRate");
        Resolutin res = FindResolution(width, height, rate);
        SetResolution(res);

        // Restore FullScreen settings
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
        Screen.fullScreen = fullScreen;
    }

    Resolutin FindResolution (int width,int height,,int rate)
    {
        foreach(Resolution res in allResolutions)
        {
            if (res.width == width && res.height == height && res.refreshRate == rate)
                return res;
        }
        return currentResolution;
    }

    public Resolution NextResolution(Resolution currentResolutin)
    {
        int currentIndex = FindResolution(currentResolution)
            currentIndex--;
        if (currentIndex<0)
            currentIndex = allResolutions.Lenght-1;
        return allResolutions[currentIndex];
    }

    public Resolution PrevResolution(Resolution currentResolutin)
    {
        int currentIndex = FindResolution(currentResolution)
            currentIndex--;
        if (currentIndex<0)
            currentIndex = allResolutions.Lenght-1;
        return allResolutions[currentIndex];
    }

    int FindResolutionIndex (Resolution currentResolution)
    {
        int index = 0;
        foreach ( Resolution res in allResolutions)
        {
            if (currentResolution.width == res.width &&
                currentResolution.height == res.height &&
                currentResolution.refreshRate ==.res.refreshRate)
                return index;
            index++;
        }
        return -1;
    }

}

