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

    public float dAngle = 0; // change is angle

    public GameObject muzzleFlash = null;

    public bool autoFireActive = false;
    private bool firing = false;
    private int frame = 0;

    public bool fireAtPlayer = false;
    public bool fireAtTarget = false;
    public GameObject target = null;
    public bool cleverShot = false;

    public bool homing = false;

    public bool fireOffscreen = false;

    //public bool isPlayer = false;

    public byte playerIndex = 2; //>1 = enemy

    public SoundFX shootSounds = null;

    public void Shoot (int size)
    {
        if (size<0) return;

        if(playerIndex>1)
        {
            float y = transform.position.y;
            if (GameManager.instance && GameManager.instance.progressWindow)
                y+= GameManager.instance.progressWindow.data.positionY;
            if (y<-100 || y>180)
                return;
        }

        Vector2 primaryDirection = trasform.up;
        if (fireAtPlayer || fireAtTarget)
        {
            Vector2 targetPosition;
            if (fireAtTarget)
                targetPosition = GameManager.instance.playerOneCraft.transform,position;
            else if (fireAtTarget && target.transform.position;

            primaryDirection = targetPosition - (Vector2)transform.position;
            primaryDirection.Normalize();

        }

        if(firing || timer==0) // Shoot 
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
                                                               angle,dAngle,
                                                               homing,playerIndex);
                angle = angle + ((endAngle -startAngle/(radialNumber-1));
            }
        }
        if (muzzleFlash)
            muzzleFlash.SetActive(true);
        if (shootSounds)
            shootSounds.Play();
    }
    public void FixedUpdate()
    {
        timer++;
        if(timer>=rate)
        {
            timer = 0;
            if (muzzleFlash)
                muzzleFlash.SetActive(false);
            if (autoFireActive)
            {
                firing = true
                    frame = 0;
            }
        }
        
        if (firing)
        {
            if (sequence.ShouldFire(frame))
                Shoot(1);

            frame++
                if (frame>sequence.totalFrames)
                firing = false;
        }
    }
    public void Active()
    {
        autoFireActive = false;
        timer = 0;
        frame = 0;
        firing = true;
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