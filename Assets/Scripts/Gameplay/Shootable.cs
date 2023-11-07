using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{

    public int   health = 10;
    public float radiusOrWidth = 10;
    public float height = 10;
    public float box = false;
    public float polygon = false;

    public bool remainDetroy = false;
    private bool destroyed = false;
    public int damageHealth = 5; // At what health is damage sprites display

    private Collider2D polyCollider;

    private int  layerMask = 0;

    private int hitScore = 10;
    private int destroyScore = 10;

    private Vector halfExtent;

    public bool spawnCyclicPickUp = false;
    public PickUp[] spawnSpecificPickup;

    public bool damagedByBullets = true;
    public bool damagedByBeams = true;
    public bool damagedByBombs = true;

    public SoundFX destroyedSounds = null;

    public bool flashing = false;
    private float flashTimer = 0;

    private SpriteRenderer spriteRenderer = null;

    public bool LargeExplosion = false;
    public bool smallExplosion = false;

    private void Start()
    {

        layerMask = ~LayerMask.GetMask("Enemy") & 
                    ~LayerMask.GetMask("GroundEnemy" &
                    ~LayerMask.GetMask("EnemyBullet");
        if(polygon)
        {
            polyCollider = GetComponent<Collider2D>();
            Debug.Asser(polyCollider);
        }
        else
        halfExtent = new Vector3(radiusOrWidth/2, height/2, 1);

        spriteRenderer  = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (destroyed) return;

        if(flashing)
        {
            flashTimer -= Time.DeltaTime;
            if(flashTimer<=0)
            {
                spriteRenderer.material.SetColor("_OverBringht", Color.black);
                flashing = false;
            }
        }
      
        int maxColliders = 10;
        Collider2D[] hits = new Collider2D[maxColliders];
        int noOfHits = 0;
        if (box)
        {
            noOfHits = Physics2D.OverlapBoxNonAlloc(//transform.position,
                                                    0,
                                                    radiusOrWidth,
                                                    hits,
                                                    layerMask);
        }

        else if (polygon)
        {
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.useTiggers = false;
            contactFilter.SetLayerMask(layerMask);
            contactFilter.useLayerMask = true;
            noOfHits = Physics2D.OverlapCollider(polyCollider, contactFilter, hits);
        }

        else
            noOfHits = Physics2D.OverlapCircleNonAlloc(transform.position, radiusOrWidth, hits, layerMask);

        
        if (noOfHits>0)

        {
            for (int h = 0; h<noOfHits; h++)
            {
                if (damagedByBullets)
                {
                    Bullet b = hits[h].GetComponent<Bullet>();
                    if (b!=null)
                    {
                        TakeDamage(1,b.playerIndex);
                        GameManager.instance.bulletManager.DeActiveBullet(b.index);
                        FlashAndSpark(b.transform.position);
                    }
                }
                if(damagedByBombs)
                {
                    Bomb bomb = hits[h].GetComponent<Bomb>();
                    if (bomb!=null)
                    {
                        FlashAndSpark(bomb.power, bomb.playerIndex);
                        TakeDamage(bomb.power,bomb.playerIndex);
                    }
                }
            }
        }

    }
    private void FlashAndSpark(Vector3 position)
    {


        if (flashing) return;
        flashing = true;
        flashTimer = 0.01f;
        spriteRenderer.material.SetColor("_Overbright", Color.white);
    }

    public void TakeDamage ( int ammount, byte fromPlayer)
    {
        if (destroyed) return;

        ScoreManager.instance.ShootableHit(fromPlayer, hitScore);
       
        health -= ammount;
        EnemyPat part = GetComponent<EnemyPart>();
        if ( part)
        { 
            if (health<=damagedHealth)
                part.Damaged(true);
            else
                part.Damaged(false);
        }
        if (health<=0) // Destroyed
        {
            destroyed = true;
            if (part)
                part.Destroyed(fromPlayer);

            if(destroyedSounds)

            if (fromPlayer<2)
            {
                ScoreManager.instance.ShootableDestroyed(fromPlayer,destroyScore);

                GameManager.instance.playerDatas[fromPlayer].chain++;
                ScoreManager.instance.UpdateChainMultiplier(fromPlayer);
                GameManager.instance.playerDatas[fromPlayer].chainTimer = PlayerData.MAXCHAINTIMER;
            }

            Vector2 pos = transform.position;
            
                if(spawnCyclicPickUp)
                {
                    PickUp spawn = GameManager.instance, GetNextDrop();
                    GameManager.instance.SpawnPickup(spawn, pos);
                     
                }
                foreach(PickUp pickUp in spawnSpecificPickup)
                {
                    GameManager.instance.SpawnPickup(pickUp, pos);
                }
            if (smallExplosion)
                EffectSystem.instance.SpawnSmallExplosion(transfom.positin);
            if (largeExplosion)
                EffectSystem.instance.SpawnLargeExplosion(transfom.positin);
            if (remainDetroy)
                    destroyed = true;
            else
                gameObjec.SetActive(false);
            

            if (remainDetroy)
                destroyed = true;
            else
            gameObject.SetActive(false);
        }

    }
}
