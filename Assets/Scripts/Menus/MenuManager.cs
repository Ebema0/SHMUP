using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public static MenuManager instance = null;

    internal Menu activeMenu = null;

    private void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one MenuManager!");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SwitchToGameplayMenu()
    {
        SceneManager.LoadScene("PauseMenu",     LoadSceneMode.Additive);
        SceneManager.LoadScene("GraphicsMenu",  LoadSceneMode.Additive);
        SceneManager.LoadScene("OptionsMenu",   LoadSceneMode.Additive);
        SceneManager.LoadScene("AudioMenu",     LoadSceneMode.Additive);
        SceneManager.LoadScene("ControlsOptionsMenu",  LoadSceneMode.Additive);
        SceneManager.LoadScene("YesNoMenu",     LoadSceneMode.Additive);
        SceneManager.LoadScene("ConrollerMenu", LoadSceneMode.Additive);

        SceneManager.LoadScene("DebugHUDScene", LoadSceneMode.Additive);


    }
    public void SwitchToMainMenuMenus()
    {
         SceneManager.LoadScene("MainMenu",      LoadSceneMode.Additive);
         SceneManager.LoadScene("ScoresMenu",        LoadSceneMode.Additive);
         SceneManager.LoadScene("MedalsMenu",        LoadSceneMode.Additive);
         SceneManager.LoadScene("CreditsMenu",       LoadSceneMode.Additive);
         SceneManager.LoadScene("PlayMenu",          LoadSceneMode.Additive);
         SceneManager.LoadScene("PracticeMenu",      LoadSceneMode.Additive);
         SceneManager.LoadScene("PracticeArenaMenu", LoadSceneMode.Additive);
         SceneManager.LoadScene("PracticeStageMenu", LoadSceneMode.Additive);
         SceneManager.LoadScene("CraftSelectMenu",   LoadSceneMode.Additive);
         SceneManager.LoadScene("GraphicsOptionsMenu",LoadSceneMode.Additive);
         SceneManager.LoadScene("OptionsMenu",       LoadSceneMode.Additive);
         SceneManager.LoadScene("AudioOptionsMenu",         LoadSceneMode.Additive);
         SceneManager.LoadScene("ControlsOptionsMenu",      LoadSceneMode.Additive);
         SceneManager.LoadScene("YesNoMenu",         LoadSceneMode.Additive);

        SceneManager.LoadScene("DebugHUDScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("GameOverMenu", LoadSceneMode.Additive);

    }
}
