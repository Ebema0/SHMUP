using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;

public class BulletManager : MonoBehaviour
{
    public Bullet[] bulletPrefab;
   
    public enum BulletType
    {
        Bullet1_Size1,
        Bullet1_Size2,
        Bullet1_Size3,
        Bullet1_Size4,
        Bullet1_Size5,
        Bullet2_Size1,
        Bullet2_Size2,
        Bullet2_Size3,
        Bullet2_Size4,
        Bullet2_Size5,
        Bullet3_Size1,
        Bullet3_Size2,
        Bullet3_Size3,
        Bullet4_Size1,
        Bullet4_Size2,
        Bullet4_Size3,
        Bullet4_Size4,
        Bullet5_Size1,
        Bullet5_Size2,
        Bullet5_Size3,
        Bullet5_Size4,
        Bullet6_Size1,
        Bullet6_Size2,
        Bullet6_Size3,
        Bullet6_Size4,
        Bullet7_Size1,
        Bullet7_Size2,
        Bullet7_Size3,
        Bullet7_Size4,
        Bullet8_Size1,
        Bullet8_Size2,
        Bullet8_Size3,
        Bullet8_Size4,
        Bullet9_Size1,
        Bullet9_Size2,
        Bullet9_Size3,
        Bullet9_Size4,
        Bullet10_Size1,
        Bullet10_Size2,
        Bullet10_Size3,
        Bullet10_Size4,
        Bullet11_Size1,
        Bullet11_Size2,
        Bullet11_Size3,
        Bullet11_Size4,
        Bullet12_Size1,
        Bullet12_Size2,
        Bullet12_Size3,
        Bullet12_Size4,
        Bullet13_Size1,
        Bullet13_Size2,
        Bullet13_Size3,
        Bullet13_Size4,
        Bullet14_Size1,
        Bullet14_Size2,
        Bullet14_Size3,
        Bullet14_Size4,
        Bullet15_Size1,
        Bullet15_Size2,
        Bullet15_Size3,
        Bullet15_Size4,
        MAX_Types
    }

        const int MAX_BULLET_PER_TYPE = 500;
        const int MAX_BULLET_COUNT = MAX_BULLET_PER_TYPE * (int)BulletType.MAX_TYPES;
        private Bullet[] bullets = new Bullet[MAX_BULLET_COUNT];
        private Bullet[] = new Bullet[Max_Bullet_Cunt];
        private NativeArray<BulletData>bulletData;
       private TransformAccesArray bulletTransforms;

    ProcessBulletJob jobProcessor;


    // Start is called before the first frame update
    void Start()
    {
        bulletData = new NativeArray<BulletData>(MAX_BULLET_COUNT, Allocator.Presistent);
        bulletTransforms = new TransformAccesArray(MAX_BULLET_COUNT);
        int index = 0;
        for (int bulletType = (int)BulletType.Bullet1_Size1_Size1; bulletType<(int)BulletType.MAX_TYPES; bulletType++)
        {
            for (int b = 0; b<MAX_BULLET_PER_TYPE; b++)
            {
                Bullet newBullet = Instatiate(bulletPrefab[bulletType]).GetComponent<Bullet>();
                newBullet.gameObject.SetActive(false);
                newBullet.transfornm.SetParent(transform);
                bullets(index) = newBullet;
                index++;
            }
        }

        jobProcessor = new ProcessBulletJob { bullets = bulletData };

    }
    private void OnDestroy()
    {
        bulletData.Dispose();
        bulletTransforms.Dispose();
    }
    private int NextFreeBulletIndex()
    {
        int startIndex = (int) type * MAX_BULLET_PER_TYPE;
        for (int b = 0; b<MAX_BULLET_PER_TYPE;b++)
        {

            if (!bulletData[startIndex+b].active)
                return startIndex + b; 
        }
        return- 1
    }

    public BulletManager SpawnBullet(BulletType type, float x, float y,float dX, float dY , float angle)

    {

        int bulletIndex = NextFreeBulletIndex(type);
        if(bulletIndex>-1 )
        {
            BulletType result = bullets[bulletIndex];
            result.gameObject.SetActitive(true);
            bulletData[bulletIndex] = new BulletData(x, y, dX, dY, angle,(int) type, (true);
            bullets[bulletsIndex].gameObject.transform.position = new Vector3(x, y, 0);
            return result;
        }
        return null;
    }


    private void FixedUpdate()
    {
        ProcessBullets();
    }

    void  ProcessBullets()
    {
        JobHandle handler = jobProcessor.Schedule(bulletTransforms); 
    }

    public struct ProcessBulletJob : IJobParralelForTransform
    {
        public NativeArray<BulletData> bullets;
        {
           public void Excute (int index, TransformAccess transform)
           {

           }
        }
    }
}
