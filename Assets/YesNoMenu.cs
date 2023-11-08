using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNoMenu : Menu
{
    public static YesNoMenu instance = null;

    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one YesNoMenu");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void onYesButton()
    {
        Applivation.Quit();
#if UNITY_EDITOR
       UnityEditor.EditorApplication.isPlaying = false;
#endif

    }

    public void OnNoButton()
    {
        TurnOFf(true);
    }

}
