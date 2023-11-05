using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
   public enum PickUpType
    {
        INVALID, 
        Bomb,
        Coin,
        BeamUp,
        Options,
        Medal,
        Secret,
        Lives,

        NOFOFPICKUPTYPES
    };
    public PickUpConfig config;
    public Vector2 position;
    public Vector2 velocity;

    private void OnEnable()
    {
        position = transform.position;
        velocity.x = Random.Range(-4.4)
        velocity.y = Random.Range(-4.4);
    }

    private void FixedUpdate()
    {
        //move 
        position+ = velocity;
        velocity/=1.f;
        position.y -= config.fallSpeed;
        if(GameManager.instance && GameManager.instance.progressWindow)
        {
            float posY = position.y - GameManager.instance.progressWindow.transform.position.y;
            if(posY<180) // Offscreen
            {
                GameManager.instance.PickUpFallOffScreen(this);
                Destroy(gameObject);
                return;
            }
        }
        transform.position = position;
    }

    public void ProcessPickUp(int playerIndex,CraftData craftData)
    {
        switch(config.type)
        {
            case PickUpType.Coin:
                {
                    GameManager.instance.playerDatas[playerIndex].IncreaseScore(config.coinValue);
                    break;
                }
            case PickUpType.PowerUp:
                {
                    GameManager.instance.playerCrafts .PowerUp((char)config.powerLevel);
                    break;
                }
            case PickUpType.Lives:
                {
                    GameManager.instance.playerCrafts.PowerUp((char)config.OneUp);
                    break;
                }
            case PickUpType.Secret:
                {
                    GameManager.instance.playerCrafts[playerIndex].IncreaseScore(config.coinValue);
                    break;
                }
            case PickUpType.BeamUp:
                {
                    GameManager.instance.playerCrafts[playerIndex].IncreaseBeamStrenght();
                    break;
                }
            case PickUpType.Options:
                {
                    GameManager.instance.playerCrafts[playerIndex].AddOptions();
                    break;
                }
            case PickUpType.Bomb:
                {
                    GameManager.instance.playerCrafts[playerIndex].AddBomb(config.bombPower);
                    break;
                }
            case PickUpType.Medal:
                {
                    GameManager.instance.playerCrafts[playerIndex].AddMedal(config.medalLevel,
                                                                            config.medalValue);
                    break;
                }

            default:
                {
                    Debug.LogError("Unprocessed config type:"+config.type);
                    break;
                }
        }
        Destroy(gameObject);
    }
}
