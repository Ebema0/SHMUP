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


    private Vector halfExtent;

    public bool spawnCyclicPickUp = false;
    public PickUp[] spawnSpecificPickup;

    public bool damagedByBullets = true;
    public bool damagedByBeams = true;
    public bool damagedByBombs = true;

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
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (destroyed) return;
      
        int maxColliders = 10;
        Collider2D[] hits = new Collider2D[maxColliders];
        int noOfHits = 0;
        if (box)
            noOfHits = Physics2D.OverlapBoxNonAlloc(//transform.position,
                                                    0,
                                                    radiusOrWidth,
                                                    hits, 
                                                    layerMask);

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
                    }
                }
                if(damagedByBombs)
                {
                    Bomb bomb = hits[h].GetComponent<Bomb>();
                    if (bomb!=null)
                    {
                        TakeDamage(bomb.power,bomb.playerIndex);
                    }
                }
            }
        }

    }
    public void TakeDamage ( int ammount, byte fromPlayer)
    {
        if (destroyed) return;

        health -= ammount;
        EnemyPat part = GetComponent<EnemyPart>();
        if ( part)
        { 
        if (health<=damagedHealth)
                part.Damaged(true);
            else
                part.Damaged(false);
        }
      
        if (health<=0)
        {
            destroyed = true;
            if (part)
                part.Destroyed();

            if (fromPlayer<2)
            {
                GameManager.instance.playerDatas[fromPlayer].chain++;
                GameManager.instance.playerDatas[fromPlayer].chainTimer = PlayerData.MAXCHAINTIMER;
            }

            Vector2 pos = transform.position;
            {
                if(spawnCyclicPickUp)
                {
                    PickUp spawn = GameManager.instance, GetNextDrop();
                    PickUp p = Instantiate(spawn, pos, Quaternion.identity);
                    if (p)
                        p.transform.SetParent(GameManager.instance.transform);
                }
                foreach(PickUp pickUp in spawnSpecificPickup)
                {
                    pickUp p = Instantiate(pickUp, pos, Quaternion.identity);
                    if(p)
                    {
                        p.transform.SetParent(Gamanager.instance.transform);
                    }
                    else Debug.LogError("Faled to spawn pickup")
                }
            }

            if (remainDetroy)
                destroyed = true;
            else
            gameObject.SetActive(false);
        }

    }
}
