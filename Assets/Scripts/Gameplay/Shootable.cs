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
                
                Bullet b = hits[h].GetComponent<Bullet>();
                if (b!=null)
                {
                    TakeDamage(1);
                    GameManager.instance.bulletManager.DeActiveBullet(b.index);
                }
                else
                {
                    Bomb bomb = hits[h].GetComponent<Bomb>();
                    if(bomb!=null)
                    {
                        TakeDamage(bomb.power);
                    }
                }
            }
        }

    }
    public void TakeDamage ( int ammount)
    {
        health -= ammount;
        if (health<=0)
        Destroy(gameObject);
    }
}
