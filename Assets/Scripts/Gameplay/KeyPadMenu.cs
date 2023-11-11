using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPadMenu : Menu
{
    public static KeyPadMenu instance = null;
    public int playerIndex = null;

    public Text nameText = 0;

    void Start()
    {
        if (instance)
        {
            Debug.LogError("trying to create more than one Keypad");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public override void TurnOn(Menu previous)
    {
        base.TurnOn(previous);
        enterText.text = "Enter Name Player"+playerIndex+1;
        nameText.text = "";
    }
    public void OnEnterButton()
    {
        ScoreManager.instance.AddScore(GameManager.instance.playerDatas[playerIndex].score,
                                       GameManager.instance.gameSession.hardness,
                                       nameText.text);
        ScoreManager.instance.SaveScores();

        if (bothPlayers && playerIndex == 0)
        {
            playerIndex = 1;
            enterText.text = "Enter Name Player"+playerIndex+1;
            nameText.text = "";
        }
        else
        {
            TurnOff(false);
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public void OnKeyPress(int key)
    {
        nameText.text += (char)key;
    }


    public void OnClearButton()
    {
        nameText.text += "";
    }

    public void OnDeleteButton()
    {
        nameText.text += "";
    }

}
