using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagementM
using using nityENgine.UI

public class GameOverMenu : Menu
{
    public static GameOverMenu instance = null;
    public Text scoreReadout = null;

   private void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying gto create more than one pauseMenu!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void OnContinueButton()
    {
        SceneManager.LoadScene("MainMenusScene");
    }
    
    void GameOver()
    {
        TurnOn(null);
        AudioManager.instance.PlayMusic(AudioMAnager.Tracks.GameOver, true, 0.5f);
        scoreReadout.text = GameMAnager.instance.playerDatas[0].score.ToString();  // to do what about player 
    }
}
