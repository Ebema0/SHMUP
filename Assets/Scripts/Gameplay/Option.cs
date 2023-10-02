using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public BulletSpawner shotSpawner = null;
    public GameObject muzzleFlash = null;

    public void Shoot()
    {
        shotSpawner.Shoot(1);
    }

  
}
