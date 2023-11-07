using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : Menu
{
    public static ScoreMenu instance = null;

    public Text[] scores;
    public Text[] names;

    void Start()
    {
        if(instance)
        {
            Debug.LogError("Trying to create more than one ScoreMenu");
            Destroy(gameObject);
            return;
        }
        instance = this; 
    }

    public void OnBackButton()
    {
        TurnOff(true);
    }

   
   public void OnPrevButton()
    {
       
    }

    public void OnFriendsButton()
    {
        
    }
    public void OnBackButton()
    {
        TurnOff(true);
    }

    public override void TurnOn(Menu previous)
    {
        base.TurnOn(previous);

        int hardness = 0; 

    for(int s=0; s<8; s++)
        {
            scores[s].text = ScoreManager.instance.scores[s, hardness]();
            names[s].text = ScoreManager.instance.names[s, hardness]();
        }
    }
}
