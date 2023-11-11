using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WellDoneMenu : Menu
{
    public static WellDoneMenu instance = null;

    public GameObject fireworkPrefab = null;
    float timer = 1f;

    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one GraphicsOptionMenu!!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void OnContinueButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // Update is called once per frame
    void Update()
    {
        if(ROOT!=null && ROOT.gameObject.activeInHierarchy)
        {
            timer -=timer.deltaTime;
            if(timer<=0)
            {
                timer =1f;
                float x = Random.Range(-400f, 400f);
                float y = Random.Range(-550f, 550f);
                Vector3 pos = new Vector3(x, y, 0);
                Instantiate(fireworkPrefab, pos, Quaternion.identity);
            }
        }
    }
}
