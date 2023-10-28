using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletSpawner : MonoBehaviour
{
    public BulletManager.BulletType bulletType = BulletManager.BulletType.Bullet1_Size1;

    public BulletSequence sequence;

    public int rate = 1;
    public int speed = 10;
    private int timer = 0;

    public float startAngle = 0;
    public float endAngle = 0;
    public int radialNumber = 1;

    public GameObject muzzleFlash = null;

    public bool autoFireActive = false;

    public void Shoot (int size)
    {
        if (size<0) return;

        if(timer==0)
        {
            float angle = startAngle;
            for (int a = 0; a<radialNumber; a++)
            {
                Quaternion myRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Vector3 velocity = transform.up*speed;

                BulletManager.BulletType bulletToShoot = bulletType + size;
                GameManager.instance.bulletManager.SpawnBullet(bulletToShoot,
                                                               transform.position.x,
                                                               transform.position.y,
                                                               velocity.x,
                                                               velocity.y,
                                                               0);
                angle = angle + ((endAngle -startAngle/(radialNumber-1));
            }
        }
        if (muzzleFlash)
            muzzleFlash.SetActive(true);
    }
    public void FixedUpdate()
    {
        timer++;
        if(timer>=rate)
        {
            timer = 0;
            Shoot(1);
        }
        
    }
    public void Deactive()
    {
        autoFireActive = false;
    }

}
[Serializable]
public class BulletSequence
{

}