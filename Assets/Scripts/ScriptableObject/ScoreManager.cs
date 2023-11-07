using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    int currentMultiplier = 1;
   
    void Start()
    {
        if(instance)
        {
            Debug.LogError("Trying to create more than 1 ScoreManager");
            Destroy(gameObject);
            return;
        }
    }

    public void ShootableHit(int playerIndex, int score)
    {
        GameManager.instance.playerCrafts[playerIndex].IncreaseScore(score * currentMultiplier);
    }

    public void ShootableDestroyed(int playerIndex, int score)
    {
        GameManager.instance.playerCrafts[playerIndex].IncreaseScore(score * currentMultiplier);
    }

    public void BossDestroyed(int playerIndex, int score)
    {
        GameManager.instance.playerCrafts[playerIndex].IncreaseScore(score * currentMultiplier);
    }

    public void PickCollect(int playerIndex, int score)
    {
        GameManager.instance.playerCrafts[playerIndex].IncreaseScore(score * currentMultiplier);
    }

    public void BulletDestroyed(int playerIndex, int score)
    {
        GameManager.instance.playerCrafts[playerIndex].IncreaseScore(score * currentMultiplier);
    }

    public void MedalCollected(int playerIndex, int score)
    {
        GameManager.instance.playerCrafts[playerIndex].IncreaseScore(score * currentMultiplier);
    }

    public void UpdateChainMultiplier(int playerIndex)
    {
        int chain = GameManager.instance.playerDatas[playerIndex].chain;
        currentMultiplier = (int)Math.Pow(chain +1 ), 1,5)
    }
}
