using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class SplashScreen : MonoBehaviour
{
    private VideoPlayer player = null;

  
    void Start
    {
        player.loopPoinReached += EndReached;
    }

    void VideoFinished(VideoPlayer vp)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }
}
