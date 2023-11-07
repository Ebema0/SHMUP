using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelProgress : MonoBehaviour
{
    public ProgressData data;
    public int levelSize;
    public GameObject midGroundTitleGrid;
    public float midGroundRate = 0.75f;

    public AnimationCurve speedCurve = new AnimationCurve();

    private Craft player1Craft;

    // Start is called before the first frame update
    void Start()
    {
        data.positionX = transform.position.x;
        data.positionX = transform.position.y;
        if (GameManager.instance)
            GameManager.instance.progressWindow = this; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(data.progress < levelSize)
        {
            float ratio = (float)data.progress/ (float)levelSize;
            float movement = speedCurve.Evaluate(ratio);
            data.progress++;
            UpdateProgressWindow(movement);
            if (player1Craft==null)
                player1Craft = GameManager.instance.playerOneCraft;
            if (player1Craft)
                UpdateProgressWindow(player1Craft.craftData.positionX, movement);
        }
    }
    void UpdateProgressWindow(float shipX, float movement)
    {
        data.positionX = shipX/10f;
        data.positionY += movement;
        transform.position = new Vector3(data.positionX, data.positionY, 0);
        midGroundTitleGrid.transform.position = new Vector3(0, data.positionY* midGroundRate, 0);
    }
}
[Serializable]
public class ProgressData
{
    public int progress;
    public float positionX;
    public float positionY;
};
