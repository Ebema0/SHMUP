using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
  
    Vector3 newPosition = new Vector3();

    public GameObject AftFlame1;
    public GameObject AftFlame2;

    public GameObject RightFlame;
    public GameObject LeftFlame;

    public GameObject FrontFlame1;
    public GameObject FrontFlame2;


    public int playerIndex;

    public CraftConfiguration config;
    


    Animator animator;
    int leftBoolID;
    int rightBoolID;

    bool invunerable = true;
    int invunerableTimer = 120;
    const int INVUNERABLELENGTH = 120;
    bool alive = true;

    public static int MAXIMUMBEAMCHARGE = 64;

    const int MAXLIVES = 5;
    const int MAXSMALLBOMBS = 8;
    const int MAXLARGEBOMBS = 5;

    SpriteRenderer spriteRenderer = null;
    int layerMask = 0;
    int pickUpLayer = 0;

    public BulletSpawner [] bulletSpawner = new BulletSpawner[5];

    public Option[] options = new Option[4];

    public GameObject[] optionMarkersL1 = new GameObject[4];
    public GameObject[] optionMarkersL2 = new GameObject[4];
    public GameObject[] optionMarkersL3 = new GameObject[4];
    public GameObject[] optionMarkersL4 = new GameObject[4];

    public Beam beam = null;

    public GameObject bombPrefab = null;

    public SoundFX explodingNoise = null;
    public SoundFX bombSound = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Assert(animator);

        leftBoolID = Animator.StringToHash("Left");
        rightBoolID = Animator.StringToHash("Right");

        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Assert(spriteRenderer);

        layerMask = LayerMask.GetMask("PlayerBullet") &
            ~LayerMask.GetMask("PlayerBombs") &
             ~LayerMask.GetMask("GroundEnemy") &
            ~LayerMask.GetMask("Player");
        pickUpLayer = LayerMask.NameToLayer("PickUp");

        craftData.beamCharge = (char)100;
    }

    public void SetInvunerable()
    {
        invunerable = true;
        invunerableTimer = INVUNERABLELENGTH;
    }

    private void FixedUpdate()
    {
        if (InputManager.instance && alive)
        {
            // Chain Drop 
            if (GameManager.instance.playerDatas[playerIndex].chainTimer>0)
            {
                GameManager.instance.playerDatas[playerIndex].chainTimer--;
                if GameManager.instance.playerDatas[playerIndex].chain = 0;
                {
                    GameManager.instance.playerDatas[playerIndex].chain = 0;
                    ScoreManager.instance.UpdateChainMultiplier(playerIndex);
                }
            }
            //Invurnable flashing 

            if (invunerable)
            {
                if (invunerableTimer % 12<6)
                    spriteRenderer.material.SetColor("_Overbright", Color.black);
                else
                    spriteRenderer.material.SetColor("_Overrbright", Color.white);
                invunerableTimer--;

                if (invunerableTimer<=0)
                {
                    invunerable = false;
                    spriteRenderer.material.SetColor("_Overbrigt", Color.black);
                }

                // Hit Detection
                int maxCollider = 10;
                Collider2D[] hits = new Collider2D[maxCollider];

                // Bullets Hits
                Vector2 halfSize = new Vector2(3f, 4f);
                int noOfHits = Physics2D.OverlapBoxNonAlloc(transform.position,0, halfSize, hits);
                if (noOfHits >0)
                {
                    foreach (Collider2D hit in hits)
                    {
                        if (hit)
                        {
                            if (hit.gameObject.layer == pickUpLayer)
                                PickUp(hit.GetComponent<PickUp>());
                            Hit();
                        }
                    }
                }

                // PickUps and Bullet Crazings
                halfSize = new Vector2(15f, 21f);
                int noOfHits = Physics.OverlapBoxNonAlloc(transform.position,0, halfSize, hits);
                if (noOfHits >0)
                {
                    foreach (Collider2D hit in hits)
                    {
                        if (hit)
                        {
                            if (hit.gameObject.layer == pickUpLayer)
                                PickUp(hit.GetComponent<PickUp>());
                            else // Bullet Graze
                            if (craftData.beamCharge<MAXIMUMBEAMCHARGE)
                            {
                                craftData.beamCharge++;
                                craftData.beamTimer++;
                            }
                        }
                    }
                }

                // Movement
                craftData.positionX += InputManager.instance.playerState[0].movement.x;
                    craftData.positionY += InputManager.instance.playerState[0].movement.y;

                    if (craftData.positionX<-146) craftData.positionX = -146;
                    if (craftData.positionX>146) craftData.positionX = 146;

                if (craftData.positionX<-180) craftData.positionX = -180;
                if (craftData.positionX>180) craftData.positionX = 180;

                newPosition.x = (int)craftData.positionX;
                if (GameManager.instance.progressWindow)
                    if (!GameManager.instance.progressWindow)
                        GameManager.instance.progressWindow = GameObject.FindObjectLevelType<LevelProgress>();

                            if (GameManager.instance.progressWindow)
                     newPosition.y = (int)craftData.positionY + GameManager.instance.progressWindow.transform.position.y;
                else
                    newPosition.y = (int)craftData.positionY
                    gameObject.transform.position = newPosition;

                if (InputManager.instance.playerState[playerIndex].up)
                {
                    AftFlame1.SetActive(true);
                    AftFlame2.SetActive(true);
                }
                else
                {
                    AftFlame1.SetActive(false);
                    AftFlame2.SetActive(false);
                }

                if (InputManager.instance.playerState[playerIndex].down)
                {
                    FrontFlame1.SetActive(true);
                    FrontFlame2.SetActive(true);
                }
                else
                {
                    FrontFlame1.SetActive(false);
                    FrontFlame2.SetActive(false);
                }

                if (InputManager.instance.playerState[playerIndex].left)
                {
                    RightFlame.SetActive(true);
                    animator.SetBool(leftBoolID,true);
                }
                else
                {
                    RightFlame.SetActive(false);
                    animator.SetBool(leftBoolID, false);
                }
                if (InputManager.instance.playerState[playerIndex].left)
                {
                    LeftFlame.SetActive(true);
                    animator.SetBool(rightBoolID, true);
                }
                else
                {
                    LeftFlame.SetActive(false);
                    animator.SetBool(rightBoolID, false);
                }
                // Shooting Bullets 
                if (InputManager.instance.playerState[playerIndex].shoot)
                {
                    //Shot
                    ShotConfiguration shotConfig = config.shotLevel[craftData.shotPower];
                    for (int s = 0; s<5; s++)
                    {
                        bulletSpawner[s].Shoot(shotConfig.spawnerSizes[s]);
                    }
                    //Options
                    for (int o = 0; o<craftData.noOfEnabledOptions;o++)
                    {
                        if (options[o])
                            options[o].Shoot();
                    }
                }
                //Options Button
                if (!InputManager.instance.playerPrevState[playerIndex].options &&
                    InputManager.instance.playerState[playerIndex].options) 
                {
                    craftData.optionsLayout++;
                    if(craftData.optionsLayout>3)
                        craftData.optionsLayout = 0;
                    SetOptionsLayout(craftData.optionsLayout);
                }

                //Beam
                if(InputManager.instance.playerState[playerIndex].beam)
                {
                    beam.Fire();
                }

                // Bomb
                if (!InputManager.instance.playerPrevState[playerIndex].bomb &&
                    InputManager.instance.playerState[playerIndex].bomb)
                {
                    FireBomb();
                }
            }
        }
    }

    public void PickUp(PickUp pickUp)
    {
        if(pickUp)
        {
            pickUp.ProcessPickUp(playerIndex, craftData); 
        }
    }

    public void Hit ()
    {
        if (!invunerable && GameManager.instance.gameSession.invincible)
            Explode();
    }
    public void Explode()
    {
        alive = false;
        GameManager.instance.playerDatas[playerIndex].lives--;
        StartCoroutine(Exploding());

        if (explodingNoise)
            explodingNoise.Play();

        if (GameManager.instance.playerDatas[playerIndex].lives == 0)
        {

        }
        else
        {

            // Eject powers and spwan next life
            CraftData craftData = GameManager.instance.gameSession.craftDatas[playerIndex];
            int noOfOptionsToRespawn = craftData.noOfEnabledOptions-1;
            int noOfOptionsToRespawn = craftData.shotPower-1;
            int noOfOptionsToRespawn = craftData.beamPower-1;

            for (int o=0; o<noOfOptionsToRespawn; o++)
            {
                PickUp pickUp = GameManager.instance.SpawnPickup(GameManager.instance.options, transform.position);
                pickup.transform.position += new Vector3 (UnityEngine.Random.Range(-128,128),UnityEngine.Random.Range(-128,128),0)
            }

            for (int o = 0; o<noOfPowerUpsToRespawn; o++)
            {
                PickUp pickUp = GameManager.instance.SpawnPickup(GameManager.instance.powerup, transform.position);
                pickup.transform.position += new Vector3(UnityEngine.Random.Range(-128, 128), UnityEngine.Random.Range(-128, 128), 0)
            }

            for (int o = 0; o<noOfBeamUpsToRespawn; o++)
            {
                PickUp pickUp = GameManager.instance.SpawnPickup(GameManager.instance.beamup, transform.position);
                pickup.transform.position += new Vector3(UnityEngine.Random.Range(-128, 128), UnityEngine.Random.Range(-128, 128), 0)
            }
        }

    }

    IEnumerator Exploding()
    {
        Color col = Color.white;
        for(float redness = 0; redness <= 1; redness+=0.3f)
        {
            col.g = 1 - redness;
            col.b = 1 - redness;
            spriteRenderer.color = col;
            yield return new WaitForSeconds(0.1f);
        }

        EffectSystem.instance.CraftExplosion(transform.position);
        GameManager.instance.playerCrafts[playerIndex] = null;
        Destroy(gameObject);

        if (GameManager.instance.playerDatas[playerIndex].lives == 0)
        {

        }
        else
        {

            // Eject powers and spwan next life
            CraftData craftData = GameManager.instance.gameSession.craftDatas[playerIndex];
            int noOfOptionsToRespawn = craftData.noOfEnabledOptions-1;
            int noOfOptionsToRespawn = craftData.shotPower-1;
            int noOfOptionsToRespawn = craftData.beamPower-1;
            GameMAnager.instance.ResetState(playerIndex);

            for (int o = 0; o<noOfOptionsToRespawn; o++)
            {
                PickUp pickUp = GameManager.instance.SpawnPickup(GameManager.instance.options, transform.position);
                pickup.transform.position += new Vector3(UnityEngine.Random.Range(-128, 128), UnityEngine.Random.Range(-128, 128), 0)
            }

            for (int o = 0; o<noOfPowerUpsToRespawn; o++)
            {
                PickUp pickUp = GameManager.instance.SpawnPickup(GameManager.instance.powerup, transform.position);
                pickup.transform.position += new Vector3(UnityEngine.Random.Range(-128, 128), UnityEngine.Random.Range(-128, 128), 0)
            }

            for (int o = 0; o<noOfBeamUpsToRespawn; o++)
            {
                PickUp pickUp = GameManager.instance.SpawnPickup(GameManager.instance.beamup, transform.position);
                pickup.transform.position += new Vector3(UnityEngine.Random.Range(-128, 128), UnityEngine.Random.Range(-128, 128), 0)
            }
            GameManager.instance.DelayedRespawn(playerIndex);
        }

    }


    yield return null;
    }

    public void AddOption()
    {
        if (craftData.noOfEnabledOptions<4)
        {
            options[craftData.noOfEnabledOptions].gameObject.SetActive(true);
            craftData.noOfEnabledOptions++;
        }
    }

    public void SetOptionsLayout(int layoutIndex)
    {
        Debug.Assert(layoutIndex<4);
            for (int o = 0; o<4; o++)
        {
            switch (layoutIndex)
            {

                case 0:
                    options[o].gameObject.transform.position = optionMarkersL1[o].transform.position;
                    options[o].gameObject.transform.rotation = optionMarkersL1[o].transform.rotation;
                break;                                                        
                case 1:                                                       
                    options[o].gameObject.transform.position = optionMarkersL2[o].transform.position;
                    options[o].gameObject.transform.rotation = optionMarkersL2[o].transform.rotation;
                    break;                                                    
                case 2:                                                       
                    options[o].gameObject.transform.position = optionMarkersL3[o].transform.position;
                    options[o].gameObject.transform.rotation = optionMarkersL3[o].transform.rotation;
                    break;                                                    
                case 3:                                                       
                    options[o].gameObject.transform.position = optionMarkersL4[o].transform.position;
                    options[o].gameObject.transform.rotation = optionMarkersL4[o].transform.rotation;
                    break;                                                    
            }
        }
    }

    public void IncreaseBeamStrength( int surplusValue)
    {
        if (craftData.beamPower<5)
        {
            craftData.beamPower++;
            UpdateBeam();
        }
        else
            ScoreManager.instance.PickupCollected(playerIndex, surplusValue);
    }

    void UpdateBeam()
    {
        beam.beamWidth = (craftData.beamPower+2)*8f;
    }

    void FireBomb()
    {
        if (craftData.smallBombs>0)
        {
            craftData.smallBombs--;
            Vector3 pos = transform.position;
            pos.y += 100;
            if (bombSound)
                bombSound.Play();
            Bomb bomb = Instantiate(bombPrefab, pos, Quaternion.identity).GetComponent<Bomb>();
            if (bomb)
                bomb.playerIndex = (byte)playerIndex;
        }
    }

    public void PowerUp(byte powerLevel, int surplusValue)
    {
        craftData.shotPower += powerLevel;
        if (craftData.shotPower>8)
        {
            craftData.shotPower = 8;
            ScoreManager.instance.PickupCollected(playerIndex, surplusValue);
        }
    }

    public void IncreaseScore(int value)
    {
        GameManager.instance.playerDatas[playerIndex].score += value;
        GameManager.instance.playerDatas[playerIndex].stagecore += value;
    }
    public void OneUp(int surplusValue )
    {
        GameManager.instance.playerDatas[playerIndex].lives++;
       if (GameManager.instance.playerDatas[playerIndex].lives>MAXLIVES)
        {
            ScoreManager.insance.MedalCollected(playerIndex, surplusValue);
            GameManager.instance.playerDatas[playerIndex].lives =MAXLIVES;
        }
    }

    public void AddMedal(int level, int value)
    {
        
        ScoreManager.insance.MedalCollected(playerIndex, value);
    }

    public void AddBomb(int power, int surplusValue)
    {
        if (power==1)
        {
            craftData.smalBombs++;
            if (craftData.smallBombs>MAXSMALLBOMBS)
            {
                craftData.smallBombs = MAXSMALLBOMBS;
                ScoreManager.insance.PickupCollected(playerIndex, surplusValue);
            }
            else if (power==2)
            {
                craftData.largeBombs++;
                if (craftData.largeBombs>MAXLARGEBOMBS)
                {
                    craftData.largeBombs = MAXLARGEBOMBS;
                    ScoreManager.insance.PickupCollected(playerIndex, surplusValue);
                }
            }
            else
                Debug.LogError("Invalid boomb power pick up");
        }
    }
}

[Serializable]
public class CraftData
{
    public float positionX;
    public float positionY;

    public char shotPower;

    public char noOfEnabledOptions;
    public char optionsLayout;

    public bool beamFiring;
    public char beamPower;  //Power settings and widht
    public char beamCharge;  // max charge
    public char beamTimer;   // current charge level

    public byte smallBombs;
    public byte largeBombs;

    public void Save(BinaryWriter writer)
    {
        writer.Writer(shotPower);

        writer.Write(noOfEnabledOptions);
        writer.Write(optionsLayout);

        writer.Write(beamPower);
        writer.Write(beamCharge);

        writer.Write(smallBombs);
        writer.Write(largeBombs);
    }

     public void Load(BinaryReader reader)
    {
        shotPower = reader.ReadByte();

        noOfEnabledOptions = reader.ReadByte();
        optionsLayout = reader.ReadByte();

        beamPower = reader.ReadByte();
        beamCharge = reader.ReadByte();

        smallBombs= reader.ReadByte();
        largeBombs = reader.ReadByte();
    }

}