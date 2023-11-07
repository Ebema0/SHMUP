using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialiser : MonoBehaviour
{
    public enum GameMode
    {
        INVALID,
        Menus,
        Gameplay
    }

    public GameMode gameMode;

    public GameObject gameManagerPrefab = null;

    public AudioManager.Tracks playMusicTrack = AudioManager.Tracks.None;

    private bool menuLoaded = false;

    private Scene displayScene;

    void Start()
    {
        if (GameManager.instance == null)
        {
            if (gameManagerPrefab)
            {
                Instantiate(gameManagerPrefab);
                Debug.Log("Instantiating gameManager");
                displayScene = SceneMAnager.GetScenesByName("DisplayScene");
            }
            else
                Debug.LogError("gameManagerprefab isn¨t set");
        }

    }

    void Update()
    {
        if (!menuLoaded)
        {
            if(!displayScene.isLoaded)
            {
                SceneManager.LoadScene("DisplayScene", LoadSceneMode.Addative);
            }
            switch (gameMode)
            {
                case GameMode.Menus:
                    MenuManager.instance.SwitchToMainMenuMenus();
                    break;
                case GameMode.Gameplay:
                    MenuManager.instance.SwitchToGameplayMenus();
                    break;
            };

            if (playMusicTrack != AudioManager.Tracks.None)
                AudioManager.instance.PlayMusicTrack(playMusicTrack, true, 1);

            if (gameMode == GameMode.Gameplay)
                gameManager.instance.SpawnPlayers();

            menuLoaded = true;
        }

    } 
}
