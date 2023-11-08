using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance = null;

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

    public void OnLoadButtton()
    {
        if(SaveManager.instance.LoadExists(1))
        {
            SaveManaager.instance.LoadGame(1);
        }
    }

    public void OnOptionsButton()
    {
       

    public void OnLoadButtton()
    {
            SceneManager.LoadScene("MainMEnuScene");
    }

}
