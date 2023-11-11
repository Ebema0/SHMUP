using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GraphicsOptionMenu : Menu
{
    public static GraphicsOptionMenu instance = null;

    public Toggle fullScreenToggle = null;
    public Button nextButton = null;

    bool fullScreenToApply = true;
    public Button prevButton = null;
    public Text resolutionText = null;

    bool fullScreenToApply = true;

    Resolution resolutionToApply;

    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one GraphicsOptionMenu!!");
            Destroy(gameObject);
            return;
        }
        instance = this;

        if (fullScreenToggle)
            fullScreenToggle.isOn = ScreenManager.instance.fullScreen;
        fullScreenToApply = ScreenManager.instance.fullScreen;

        resolutionToApply = ScreenManager.instance.currentResolution;

        if (resolutionText)
            resolutionText.text = resolutionToApply.width+"x"+resolutionToApply.height+"-"+resolutionToApply.refresRate;
    }

    public void OnNextButton()
    {
        resolutionToApply = ScreenManager.instance.NextResolution(resolutionToApply);

        if (resolutionText)
            resolutionText.text = resolutionToApply.width+"x"+resolutionToApply.height+"-"+resolutionToApply.refresRate;
    }

    public void OnPrevButton()
    {
        resolutionToApply = ScreenManager.instance.PrevResolution(resolutionToApply);

        if (resolutionText)
            resolutionText.text = resolutionToApply.width+"x"+resolutionToApply.height+"-"+resolutionToApply.refresRate;
    }
    public void OnVSyncButton()
    {
        TurnOff(false);
        CraftSelectMenu.instance.TurnOn(this);
    }
    public void OnApplyButton()
    {
        ScreenManager.instance.fullScreen = fullScreenToApply;
        Screen.fullScreen = fullScreenToApply;

        if (fullScreenToApply)
        {
            Debug.Log("Going Fullscreen !");
            PlayerPref.SetInt("FullScreen", 1)
        }
        else
        {
            Debug.Log("Going Windowed");
            PlayerPref.SetInt("FullScreen", 1)           
        }
        PlayerPrefs.Save();
    }

    public void OnFullScreenToggle()
    {
        fullScreenToApply =!fullScreenToApply;
    }

    public void OnBackButton()
    {
        TurnOff(true);
    }

}
