using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool twoPlayer = false;

    public GameObject[] craftPrefab;

    public Craft playerOneCraft = null;

    public BulletManager bulletManager = null;

    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more then 1 GameManager!");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("GameManager Created");

        bulletManager = GetComponent<BulletManager>();
    }

    public void SpawnPlayer(int playerIndex, int craftType)
    {
        Debug.Assert(craftType<craftPrefab.Length);
        Debug.Log("Spawning player "+playerIndex);
        playerOneCraft = Instantiate(craftPrefab[craftType]).GetComponent<Craft>();
        playerOneCraft.playerIndex = playerIndex;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!playerOneCraft) SpawnPlayer(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (playerOneCraft) playerOneCraft.Explode();
        }

        if (Input.GetKey(KeyCode.S))
            if (bulletManager)
                bulletManager.SpawnBullet(BulletManager.BulletType.Bullet1_Size3, 0, 150, Random.Range(-10f, -10f), Random.Range(-10f, -10f) ,0);
            
    }
}
