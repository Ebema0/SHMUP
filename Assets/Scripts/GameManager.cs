using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool twoPlayer = false;

    public GameObject[] craftPrefab;

    


    Craft playerOneCraft = null;

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
    }
}
