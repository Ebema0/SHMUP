using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    private float timer = 0;

  
    void Update()
    {
        timer+=Time.deltaTime;

        if (timer>5)
            VideoFinished();
    }

    void VideoFinished()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }
}
