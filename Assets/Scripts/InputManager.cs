using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    public InputState[] playerState = new InputState[2];

    public ButtonMapping[] playerButtons = new ButtonMapping[2];
    public AxisMapping[] playerAxis = new AxisMapping[2];

    public KeyButtonMapping[] playerKeyButtons = new KeyButtonMapping[2];
    public KeyAxisMapping[] playerKeyAxis = new KeyAxisMapping[2];


    public int[] playerController = new int[2];
    public bool[] playerUsingKeys = new bool[2];

    public const float deadZone = 0.01f;

    private System.Array allKeyCodes = System.Enum.GetValues(typeof(KeyCode));

    private string[,] playerButtonsNames = { { "J1_B1","J1_B2","J1_B3","J1_B4","J1_B5","J1_B6","J1_B7","J1_B8"},
                                            { "J2_B1","J2_B2","J2_B3","J2_B4","J2_B5","J2_B6","J2_B7","J2_B8"},
                                            { "J3_B1","J3_B2","J3_B3","J3_B4","J3_B5","J3_B6","J3_B7","J3_B8"},
                                            { "J4_B1","J4_B2","J4_B3","J4_B4","J4_B5","J4_B6","J4_B7","J4_B8"},
                                            { "J5_B1","J5_B2","J5_B3","J5_B4","J5_B5","J5_B6","J5_B7","J5_B8"},
                                            { "J6_B1","J6_B2","J6_B3","J6_B4","J6_B5","J6_B6","J6_B7","J6_B8"}};

    private string[,] playerAxisNames = { {"J1_Horizontal","J1_Vertical" },
                                   {"J2_Horizontal","J2_Vertical" },
                                   {"J3_Horizontal","J3_Vertical" },
                                   {"J4_Horizontal","J4_Vertical" },
                                   {"J5_Horizontal","J5_Vertical" },
                                   {"J6_Horizontal","J6_Vertical" },};

    public string[] oldJoystick = null;

    public static string[] actionNames = { "Shoot", "Bomb", "Auto", "Beam", "Menu", "Extra2", "Extra3", "Options" };
    public static string[] axisNames = { "Left", "Right", "Up", "Down"};

    private void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one InputManager!");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        //Initialisation

        playerController[0] = 0;
        playerController[1] = 1;

        playerUsingKeys[0] = false;
        playerUsingKeys[1] = false;

        playerAxis[0] = new AxisMapping();
        playerAxis[1] = new AxisMapping();

        playerButtons[0] = new ButtonMapping();
        playerButtons[1] = new ButtonMapping();

        playerKeyButtons[0] = new KeyButtonMapping();
        playerKeyButtons[1] = new KeyButtonMapping();

        playerKeyAxis[0] = new KeyAxisMapping();
        playerKeyAxis[1] = new KeyAxisMapping();

        playerState[0] = new InputState();
        playerState[1] = new InputState();

        oldJoystick = Input.GetJoystickNames();

        StartCoroutine(CheckControllers());
    }

    private bool PlayerIsUsingController(int i)
    {
        if (playerController[0] == i)
            return true;
        if (GameManager.instance.twoPlayer && playerController[1] == i)
            return true;
        return false;
    }

    IEnumerator CheckControllers()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);

            string[] currentJoysticks = Input.GetJoystickNames();

            for (int i = 0; i<currentJoysticks.Length; i++)
            {
                if (i<oldJoystick.Length)
                {
                    if (currentJoysticks[i] != oldJoystick[i])
                    {
                        if (string.IsNullOrEmpty(currentJoysticks[i])) //disconnect
                        {
                            Debug.Log("Controller "+i+" has been disconnected.");
                            if (PlayerIsUsingController(i))
                            {
                                ControllerMenu.instance.whichPlayer = i;
                                ControllerMenu.instance.playerText.text = " Player "+ (i+1) +" controller is disconnected";
                                ControllerMenu.instance.TurnOn(null);
                                // Turn on CotrollerMenu
                                // GameManager.instace.PauseGameplay();
                            }
                        }
                    }
                }
                else // connected
                {
                    Debug.Log("Controller "+i+" is connected using: "+currentJoysticks[i]);

                }
            }
        }
        
    }

    void UpdatePlayerState(int playerIndex)
    {
        playerState[playerIndex].left = false;
        playerState[playerIndex].right = false;
        playerState[playerIndex].down = false;
        playerState[playerIndex].up = false;

        playerState[playerIndex].shoot = false;
        playerState[playerIndex].bomb = false;
        playerState[playerIndex].options = false;
        playerState[playerIndex].auto = false;
        playerState[playerIndex].beam = false;
        playerState[playerIndex].menu = false;
        playerState[playerIndex].extra2 = false;
        playerState[playerIndex].extra3 = false;

        if (Input.GetKey(playerKeyAxis[playerIndex].left)) playerState[playerIndex].left = true;
        if (Input.GetKey(playerKeyAxis[playerIndex].right)) playerState[playerIndex].right = true;
        if (Input.GetKey(playerKeyAxis[playerIndex].up)) playerState[playerIndex].up = true;
        if (Input.GetKey(playerKeyAxis[playerIndex].down)) playerState[playerIndex].down = true;

        if (Input.GetKey(playerKeyButtons[playerIndex].shoot)) playerState[playerIndex].shoot = true;
        if (Input.GetKey(playerKeyButtons[playerIndex].bomb)) playerState[playerIndex].bomb = true;
        if (Input.GetKey(playerKeyButtons[playerIndex].options)) playerState[playerIndex].options = true;
        if (Input.GetKey(playerKeyButtons[playerIndex].beam)) playerState[playerIndex].beam = true;
        if (Input.GetKey(playerKeyButtons[playerIndex].menu)) playerState[playerIndex].menu = true;
        if (Input.GetKey(playerKeyButtons[playerIndex].extra2)) playerState[playerIndex].extra2 = true;
        if (Input.GetKey(playerKeyButtons[playerIndex].extra3)) playerState[playerIndex].extra3 = true;
        

        if (playerController[playerIndex]<0) return;

        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].horizontal])<deadZone) playerState[playerIndex].left = true; 
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].horizontal])>deadZone) playerState[playerIndex].right = true;
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].vertical])<deadZone)   playerState[playerIndex].down = true; 
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].vertical])>deadZone)   playerState[playerIndex].up = true;   


        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].shoot])) playerState[playerIndex].shoot = true;    
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].bomb])) playerState[playerIndex].bomb = true;      
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].options])) playerState[playerIndex].options = true;
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].auto])) playerState[playerIndex].auto = true;      
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].beam])) playerState[playerIndex].beam = true;      
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].menu])) playerState[playerIndex].menu = true;  
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].extra2])) playerState[playerIndex].extra2 = true;  
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].extra3])) playerState[playerIndex].extra3 = true;  
    }

    private void FixedUpdate()
    {
        UpdatePlayerState(0);
        if (GameManager.instance!=null && GameManager.instance.twoPlayer) ;
        UpdatePlayerState(1);
    }

    public int DetectControllerButtonPress()
    {
        int result = -1;

        for (int j = 0; j<6; j++)
        {
            for (int b = 0; b<8; b++)
            {
                if (Input.GetButton(playerButtonsNames[j, b])) return j;
            }
        }
        return result;
    }

    public int DetectKeyPress()
    {
        foreach (KeyCode key in allKeyCodes)
        {
            if (Input.GetKey(key)) return ((int)key);
        }
        return -1;
    }

    public bool CheckForPlayerInput(int playerIndex)
    {
        int controller = DetectControllerButtonPress();
        if (controller>-1)
        {
            playerController[playerIndex] = controller;
            playerUsingKeys[playerIndex] = false;
            Debug.Log("Player"+playerIndex+"is set controller " + controller);
            return true;
        }
        if (DetectKeyPress()>-1)
        {
            playerController[playerIndex] = -1;
            playerUsingKeys[playerIndex] = true;
            Debug.Log("Player"+playerIndex+"is set Keyboard " + controller);
            return true;
        }
        return false;
    }

    public string GetButtonName(int playerIndex, int actionID)
    {
        string buttonName = "";
        switch(actionID)
        {
            case 0:
                buttonName = playerButtonsNames[playerIndex, playerButtons[playerIndex].shoot];
                break;                   
            case 1:                      
                buttonName = playerButtonsNames[playerIndex, playerButtons[playerIndex].bomb];
                break;                   
            case 2:                      
                buttonName = playerButtonsNames[playerIndex, playerButtons[playerIndex].options];
                break;                   
            case 3:                      
                buttonName = playerButtonsNames[playerIndex, playerButtons[playerIndex].auto];
                break;                   
            case 4:                      
                buttonName = playerButtonsNames[playerIndex, playerButtons[playerIndex].beam];
                break;                   
            case 5:                      
                buttonName = playerButtonsNames[playerIndex, playerButtons[playerIndex].menu];
                break;                   
            case 6:                      
                buttonName = playerButtonsNames[playerIndex, playerButtons[playerIndex].extra2];
                break;                   
            case 7:                      
                buttonName = playerButtonsNames[playerIndex, playerButtons[playerIndex].extra3];
                break;
        }
        char b = buttonName[4];
        return "Button" + b.ToString();
    }
    public string GetKeyName(int playerIndex,int actionID)
    {
        KeyCode key = KeyCode.None;
        switch(actionID)
        {
            case 0:
                key = playerKeyButtons[playerIndex].shoot;
                break;
            case 1:
                key = playerKeyButtons[playerIndex].bomb;
                break;
            case 2:
                key = playerKeyButtons[playerIndex].options;
                break;
            case 3:
                key = playerKeyButtons[playerIndex].auto;
                break;
            case 4:
                key = playerKeyButtons[playerIndex].beam;
                break;
            case 5:
                key = playerKeyButtons[playerIndex].menu;
                break;
            case 6:
                key = playerKeyButtons[playerIndex].extra2;
                break;
            case 7:
                key = playerKeyButtons[playerIndex].extra3;
                break;
        }
        return key.ToString();

    }

    public string GetKeyAxisName(int playerIndex,int actionID)
    {
        KeyCode key = KeyCode.None;
        switch(actionID)
        {
            case 0:
                key = playerKeyAxis[playerIndex].left;
                break;
            case 1:
                key = playerKeyAxis[playerIndex].right;
                break;
            case 2:
                key = playerKeyAxis[playerIndex].up;
                break;
            case 3:
                key = playerKeyAxis[playerIndex].down;
                break;
        }
        return key.ToString();

    }

}
    public class InputState
    {
        public bool left, right, up, down;
        public bool shoot, bomb, options, auto, beam, menu, extra2, extra3;
    }

    public class ButtonMapping
    {
        public byte shoot   = 0;
        public byte bomb    = 1;
        public byte options = 2;
        public byte auto    = 3;
        public byte beam    = 4;
        public byte menu    = 5;
        public byte extra2  = 6;
        public byte extra3  = 7;
    }

    public class AxisMapping
    {
        public byte horizontal = 0;
        public byte vertical = 1;
    }

   public class KeyButtonMapping
   {
    public KeyCode shoot   = KeyCode.B;
    public KeyCode bomb    = KeyCode.N;
    public KeyCode options = KeyCode.M;
    public KeyCode auto    = KeyCode.Comma;
    public KeyCode beam    = KeyCode.Period;
    public KeyCode menu    = KeyCode.J;
    public KeyCode extra2  = KeyCode.K;
    public KeyCode extra3  = KeyCode.L;
   }

    public class KeyAxisMapping
   {
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;

   }



