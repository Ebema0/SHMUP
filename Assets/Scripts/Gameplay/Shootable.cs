using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{

    public int   health = 10;
    public float radiusOrWidth = 10;
    public float height = 10;
    private int  layerMask = 0;
    public bool box = false;

    private Vector halfExtent;

    public bool spawnCyclicPickUp = false;
    public PickUp[] spawnSpecificPickup;

    public bool damagedByBullets = true;
    public bool damagedByBeams = true;
    public bool damagedByBombs = true;

    private void Start()
    {
        layerMask = ~LayerMask.GetMask("Enemy") &  ~LayerMask.GetMask("EnemyBullet");
        halfExtent = new Vector3(radiusOrWidth/2, height/2, 1);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        int maxColliders = 10;
        Collider[] hits = new Collider[maxColliders];
        int noOfHits = 0;
        if (box)
            noOfHits = Physics.OverlapBoxNonAlloc(transform.position, radiusOrWidth, hits, layerMask);
        else
            noOfHits = Physics.OverlapSphereNonAlloc(transform.position, radiusOrWidth, hits, layerMask);

        
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
        health -= ammount;
        if (health<=0)
        {
            if(fromPlayer<2)
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
            Destroy(gameObject);
        }

    }
}
