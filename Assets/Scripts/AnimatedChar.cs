using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedChar : MonoBehaviour
{
   public Sprite[] charSprites ;
   private SpriteRenderer spriteRenderer;
    private Image image;

    public int digit = 0;
    public int frame = 0;

    public int offset = 0;

    public int noOfCharacters;
    public int noOfFrames;
    public float FPS = 10f;
    private float timer;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(!spriteRenderer)
        {
            image = GetComponent<Image>();
            Debug.Assert(image!=null);
        }
        timer = 1f / FPS;
        UpdateSprite(0);
    }

    public void UpdateSprite(int newFrame)
    {
        int loopedFrame = (newFrame + offset )% noOfFrames;
        int spriteIndex = digit + (loopedFrame*noOfCharacters);
        if (spriteRenderer)
            spriteRenderer.sprite = charSprites[spriteIndex];
        else if (image)
            image.sprite = charSprites[spriteIndex];

    }
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            timer= 1f/FPS;
            UpdateSprite(frame);
        }
    }
}
