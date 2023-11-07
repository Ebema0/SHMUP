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

    public SoundFX sounds = null;

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
        if (sounds)
            sounds.Play();

        switch(config.type)
        {
            case PickUpType.Coin:
                {
                    ScoreManager.instancePickupCollected(playerIndex,config.coinValue);
                    break;
                }
            case PickUpType.PowerUp:
                {
                    GameManager.instance.playerCrafts .PowerUp((byte)config.powerLevel,config.surplusValue);
                    break;
                }
            case PickUpType.Lives:
                {
                    GameManager.instance.playerCrafts[playerIndex].OneUp(config.surplusValue);
                    break;
                }
            case PickUpType.Secret:
                {
                    ScoreManager.instancePickupCollected(playerIndex, config.coinValue);
                    break;
                }
            case PickUpType.BeamUp:
                {
                    GameManager.instance.playerCrafts[playerIndex].IncreaseBeamStrenght(config.surplusValue);
                    break;
                }
            case PickUpType.Options:
                {
                    GameManager.instance.playerCrafts[playerIndex].AddOptions(config.surplusValue);
                    break;
                }
            case PickUpType.Bomb:
                {
                    GameManager.instance.playerCrafts[playerIndex].AddBomb(config.bombPower,config.surplusValue);
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
