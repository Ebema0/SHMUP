using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSelectMenu : Menu
{
    public static CraftSelectMenu instance = null;

    public Image player1ShipA = null;
    public Image player1ShipB = null;
    public Image player1ShipC = null;
    public Image player1ShipX = null;
    public Image player1ShipZ = null;

    public Image player2ShipA = null;
    public Image player2ShipB = null;
    public Image player2ShipC = null;
    public Image player2ShipX = null;
    public Image player2ShipZ = null;

    public Slider powerSlider1 = null;
    public Slider speedSlider1 = null;
    public Slider beamSlider1 = null;
    public Slider bombSlider1 = null;
    public Slider optionsSlider1 = null;

    public Slider powerSlider2 = null;
    public Slider speedSlider2 = null;
    public Slider beamSlider2 = null;
    public Slider bombSlider2 = null;
    public Slider optionsSlider2 = null;

    public Text countdownText = null;

    public GameObject player2Panel = null;
    public Text player2StartText = null;

    private float lastUnscaledTime = 0;
    private float timer = 5.9f;
    private bool countdown = false;

    private int selectedShip1 = 0;
    private int selectedShip2 = 0;

    public Sprite[] shipSprites         = new Sprite[5];
    public Sprite[] shipSpritesSelected = new Sprite[5];
    public Sprite[] shipSpritesDisabled = new Sprite[5];

    public CraftConfiguration[] crafts = new CraftConfiguration[5];

    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one CraftSelectMenu");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void Reset()
    {
        player2StartText.gameObject.SetActive(true);
        player2Panel.SetActive(false);
        GameManager.instance.twoPlayer = false;

        countdownText.gameObject.SetActive(false);
        countdown = false;
        UpdateShipSelection();
        timer = 5.9f;
    }

    override public void TurnOn (CraftSelectMenu previous)
    {
        base.TurnOn(previous);
        Reset();
    }

    private void FixedUpdate()
    {
        if (InputManaager.instance.playerState[0].shoot)
            StartCountdown();

        if (InputManaager.instance.playerState[1].shoot)
        {
            player2StartText.gameObject.SetActive(false);
            player2Panel.SetActive(true);
            GameManager.instance.twoPlayer = true;
            HUD.instance.TurnOnP2();
            UpdateShipSelection();
            StopCountdown();

        }

            if (!InputManager.instance.playerPrevState[0].left && InputManager.instance.playerState[9].left)
        {
            if (selectedShip1>0)
                selectedShip1--;
            UpdateShipSelection();
        }

        if (!InputManager.instance.playerPrevState[0].right && InputManager.instance.playerState[9].right)
        {
            if (selectedShip1<2)
                selectedShip1--;
            UpdateShipSelection();
        }

        if (!InputManager.instance.playerPrevState[1].left && InputManager.instance.playerState[1].left)
        {
            if (selectedShip2>0)
                selectedShip2--;
            UpdateShipSelection();
        }

        if (!InputManager.instance.playerPrevState[1].right && InputManager.instance.playerState[1].right)
        {
            if (selectedShip2<2)
                selectedShip2--;
            UpdateShipSelection();
        }

        if (countdown)
        {
            float dunscaled = Time.unscaledTime - lastUnscaledTime;
            lastUnscaledTime = Time.unscaledTime;
            timer -= dunscaled;
            countdownText.text =((int)timer).ToString();
            if (timer<1)
                GameManager.instance.StartGame();
        }
    }

    void UpdateShipSelection()
    {
        player1ShipA.sprite = shipSprites[0];
        player1ShipB.sprite = shipSprites[0];
        player1ShipC.sprite = shipSprites[0];
        player1ShipX.sprite = shipSpritesDisabled[3];
        player1ShipZ.sprite = shipSpritesDisabled[4];

        if (selectedShip1==0)
            player1ShipA.sprites == shipSpritesSelected[0];
        else if (selectedShip1==1)
            player1ShipB.sprites == shipSpritesSelected[1];
        else if (selectedShip1==2)
            player1ShipC.sprites == shipSpritesSelected[2];
        else if (selectedShip1==3)
            player1ShipX.sprites == shipSpritesSelected[3];
        else if (selectedShip1==4)
            player1ShipZ.sprites == shipSpritesSelected[4];

        CraftConfiguration config1 = crafts[selectedShip1];
        speedSlider1.value = config1.speed;
        speedSlider1.value = config1.bulletStrenght;
        beamSlider1.value = config1.beamPower;
        bombSlider1.value = config1.bombPower;
        optionsSlider1.value = config1.optionpower;

        if (GameManager.instance.twoPlayer)
        {
            player2ShipA.sprite = shipSprites[0];
            player2ShipB.sprite = shipSprites[0];
            player2ShipC.sprite = shipSprites[0];
            player2ShipX.sprite = shipSpritesDisabled[3];
            player2ShipZ.sprite = shipSpritesDisabled[4];

            if (selectedShip1==0)
                player2ShipA.sprites == shipSpritesSelected[0];
            else if (selectedShip==1)
                player2ShipB.sprites == shipSpritesSelected[1];
            else if (selectedShip1==2)
                player1ShipC.sprites == shipSpritesSelected[2];
            else if (selectedShip1==3)
                player2ShipX.sprites == shipSpritesSelected[3];
            else if (selectedShip2==4)
                player1ShipZ.sprites == shipSpritesSelected[4];

            CraftConfiguration config2 = crafts[selectedShip2];
            speedSlider2.value = config2.speed;
            speedSlider2.value = config2.bulletStrenght;
            beamSlider2.value = config2.beamPower;
            bombSlider2.value = config2.bombPower;
            optionsSlider2.value = config2.optionpower;

        }
    } 

    public void OnPlayButton()
    { 
        GameManager.instance.StartGame();
    }
    private void StartCountdown()
    {
        timer = 5.9f;
        lastUnscaledTime = Time.unscaledTime;
        countdown = true;
        countdownText.gameObjecy.SetActive(true);
    }

    private void StopCountdown()
    {
       
        countdown = false;
        countdownText.gameObjecy.SetActive(true);
    }

    public void OnPlayButton()
    {
        GameManager.instance.StartGame();
    }
   
}
