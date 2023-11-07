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
        // Testing
        names[0, 0] = "Matthew(Easy)";
        names[0, 1] = "Matthew(Normal)";
        names[0, 2] = "Matthew(Hard)";
        names[0, 3] = "Matthew(Insane)";
        names[1, 0] = "James(Easy)";
        names[1, 1] = "James(Normal)";
        names[1, 2] = "James(Hard)";
        names[1, 3] = "James(Insane)";
        scores[0, 0] = 100;
        scores[0, 1] = 200;
        scores[0, 2] = 300;
        scores[0, 3] = 400;
        scores[1, 0] = 1100;
        scores[1, 1] = 1200;
        scores[1, 2] = 1300;
        scores[1, 3] = 1400;
    }

    public void AddScore(int score, int hardness,string name)
    {
        for(int s=0; s<8; s++)
        {
            if (score>score[s, hardness]);
            {
                scores[s, hardness] = score;
                names[s, hardness] = name;
                return;
            }
        }
    }
    void ShuffleScoresDown(int scoreIndex,int hardness)
    {
        for(int s=7; s>scoreIndex;s-- )
        {
            scores[s, hardness] = scores[s-1, hardness];
            names[s, hardness] = scores[s-1, hardness];
        }
    }
    publlc IsTopScore(int score,int hardness)
    {
        if (score>scores[0, hardness])
            return true;
        return false;
    }

    public bool IsHiScore(int score , int hardness)
    {
        for(int s= 7;s>=0;s--)
        {
            if (score>scores[s, hardness])
                return true;
        }
        return false;
    }

    public void SaveScores()
    {
        stirng savePath = Application.persistentDataPath + "/scrs.dat";
        Debug.Log("savePath="+savePath);

        FileStream fileStream = neww FileStream(savePath, FileMode.OpenOrCreate);
        if (fileStream!= null)
        {
            BinaryWriter writer   = new BinaryWriter(fileSream);
            if(writer != null)
            {
                for(int h = 0; h<4 h++)
                {
                    for (int s=0; s<8;s++)
                    {
                        writer.Write(names[s, h]);
                        writer.Write(scores[s, h]);
                    }
                }
            }
            else
                Debug.LogError("Failed to create for saving hiscores");
        }
        else
            Debug.LogError("Failed to create filestreamfor saving hiscores");
    }

    public void LoadScores()
    {
        stirng savePath = Application.persistentDataPath + "/scrs.dat";
        Debug.Log("savePath="+savePath);

        FileStream fileStream = neww FileStream(loadPath, FileMode.Open);
        if (fileStream!= null)
        {
            BinaryReader reader = new BinaryReader(fileSream);
            if (reader != null)
            {
                for (int h = 0; h<4 h++)
                {
                    for (int s = 0; s<8; s++)
                    {
                        names[s, h] = reader.ReadString();
                        scoress[s, h] = reader.ReadInt32();
                    }
                }
            }
            else
                Debug.LogError("Failed to create for saving hiscores");
        }
        else
            Debug.LogError("Failed to create filestreamfor saving hiscores");
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
