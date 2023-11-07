using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagementM
using using nityENgine.UI

public class GameOverMenu : Menu
{
    public static GameOverMenu instance = null;

    public Text scoreReadout = null;
    public Text hiscoreReadout = null;

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
        if (ScoreManager.instance.IsHisScore(GameManager.instance.playerDatas[0].score, (int)GameManager.instance.gameSession.hardness))
        {
            ScoreManager.instance.IsHisScore(GameManager.instance.playerDatas[0].score,
                                             (int)GameManager.instance.gameSession.hardness,
                                             "Player");
            ScoreManager.instance.SaveScores();
        }
            SceneManager.LoadScene("MainMenusScene");
    }
    
    void GameOver()
    {

        TurnOn(null);
        AudioManager.instance.PlayMusic(AudioManager.Tracks.GameOver, true, 0.5f);
        scoreReadout.text = GameManager.instance.playerDatas[0].score.ToString();  // to do what about player 

        if (ScoreManager.instance.IsHisScore(GameManager.instance.playerDatas[0].score,
                                             (int)GameManager.instance.gameSession.hardness))
        {
            hiscoreReadout.gameObject.SetActive(true);
        }
    }
}
