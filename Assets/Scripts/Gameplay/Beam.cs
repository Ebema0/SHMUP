using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public LineRenderer lineRenderer = null;
    public float beamWidth = 20;
    public Craft craft = null;
    private int layerMask = 0;
    public byte playerIndex = 2;

    const int MINIMUMCHARGE = 10;

    public GameObject beamFlash = null;
    public GameObject[] beamHits = new GameObject[5];

    private void Start()
    {
        layerMask = ~LayerMask.GetMask("PlayerBullets") & ~LayerMask.GetMask("Player");
    }
    // Update is called once per frame
    public void Fire()
    {

        if (!craft.craftData.beamFiring)
            if (craft.craftData.beamCharge>MINIMUMCHARGER)
            {
                {
                    craft.craftData.beamFiring = true;
                    craft.craftData.beamTimer = craft.craftData.beamCharge;
                    craft.craftData.beamCharge = 0;
                    UpdateBeam();
                    gameObject.SetActive(true);
                    beamFlash.SetActive(true);
                }
            }
    }
    
    void FixedUpdate()
    {
        UpdateBeam();
    }
    void HideHits()
    {
        for (int h = 0; h<5; h++)
            beamHits[h].SetActive(false);
    }
    void UpdateBeam()
    {
       if(craft.craftData.beamTimer>0)  craft.craftData.beamTimer--;
        if(craft.craftData.beamTimer==0) // Beam Finished
        {
            craft.craftData.beamFiring = false;
            gameObject.SetActive(false);
            beamFlash.SetActive(false);
            HideHits();
            return;
        }
        float scale = beamWidth/30f;
        beamFlash.transform.localScale = new Vector3(scale, scale, 1);

        float topY = 180;
        if (GameManager.instance && GameMAnager.instance.progressWindow)
            topY += GameManager.instance.progressWindow.transform.position.y;

        int maxColliders = 20;
        Collider[] hits = new Collider[maxColliders];
        float middleY = (craft.transform.position.y + topY)*0.5f;
        Vector2 halfSize = new Vector2(beamWidth*0.5f, (topY-craft.transform.position.y)*0.5f);
        Vector3 center = new Vector3 (craft.transform.position.x , middleY, 0);
        int noOfHits = Physics.OverlapBoxNonAlloc(center, halfSize, hits, Quaternion.identity, layerMask);
        float lowest = topY;
        Shootable lowestShootable = null;
        Collider lowestCollider = null;
        if (noOfHits> 0)
        {
            for (int hit = 0; hit<noOfHits; hit++)
            {
                Shootable shootable = hits[hit].GetCompoonent<Shootable>();
                if (shootable && shootable.damagedByBeams)
                {
                    RaycastHit hitInfo;
                    Ray ray = new Ray(craft.transform.position, Vector3.up);
                    float height = topY -craft.transform.position.y;
                    if (hits[hit].Raycast(ray, out hitInfo, height))
                    {
                        if (hitInfo.point.y < lowest)
                        {
                            lowest = hitInfo.point.y;
                            lowestShootable = hits[hit].GetComponent<Shootable>();
                            lowestCollider = hits[hit];
                        }
                    }
                }
            }


            //Find hits on Collider 
            if (lowestShootable !=null)
            {
                // fire 5 rays to find each hit
                Vector3 start = craft.transform.position;
                start.x -= (beamWidth/5);

                for (int h = 0; h<5; h++)
                {
                    RaycastHit hitInfo;
                    Ray ray = new Ray(start, Vector3.up);
                    if (lowestCollider.Raycast(ray, out hitInfo, 360))
                    {
                        Vector3 pos = hitInfo.point;
                        pos.x += Random.Range(-3f, 3f);
                        pos.y += Random.Range(-3f, 3f);
                        beamHits[h].transform.position = pos;
                        beamHits[h].SetActive(true);
                        lowestShootable.TakeDamage(craft.craftData.beamPower +1 ,playerIndex);

                    }
                    else
                        beamHits[h].SetActive(false);
                    start.x += (beamWidth/5);
                }
            }
            else
            {
                HideHits();
            }
        }
        else
            HideHits();

        // Update Visuals
        lineRenderer.startWidth =  beamWidth;
        lineRenderer.endWidth   =  beamWidth;

        lineRenderer.SetPosition(0,transform.position);
        Vector3 top = transform.position;
        top.y = lowest;
        lineRenderer.SetPosition(1, top);
    }
}
