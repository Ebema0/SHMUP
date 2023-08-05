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
        while (true )
        {
            yield return new WaitForSecondsRealtime(1f);

            string[] currentJoysticks = Input.GetJoystickNames();

            for (int i = 0; 1<currentJoysticks.Length; i++)
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
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex],playerAxisNames[playerIndex].horizontal])<deadZone) playerState[playerIndex].left = true; else playerState[playerIndex].left = false;
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex],playerAxisNames[playerIndex].horizontal])>deadZone) playerState[playerIndex].right = true; else playerState[playerIndex].right = false;
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex],playerAxisNames[playerIndex].vertical])<deadZone) playerState[playerIndex].down = true; else playerState[playerIndex].down = false;
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxisNames[playerIndex].vertical])>deadZone) playerState[playerIndex].up = true; else playerState[playerIndex].up = false;

        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].shoot])) playerState[playerIndex].shoot = true; else playerState[playerIndex].shoot = false;
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].bomb])) playerState[playerIndex].bomb = true; else playerState[playerIndex].bomb = false;
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].options])) playerState[playerIndex].options = true; else playerState[playerIndex].options = false;
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].auto])) playerState[playerIndex].auto = true; else playerState[playerIndex].auto = false;
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].beam])) playerState[playerIndex].beam = true; else playerState[playerIndex].beam = false;
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].extra1])) playerState[playerIndex].extra1 = true; else playerState[playerIndex].extra1 = false;
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].extra2])) playerState[playerIndex].extra2 = true; else playerState[playerIndex].extra2 = false;
        if (Input.GetButton(playerButtonsNames[playerController[playerIndex], playerButtons[playerIndex].extra3])) playerState[playerIndex].extra3 = true; else playerState[playerIndex].extra3 = false;
    }

    private void FixedUpdate()
    {
        UpdatePlayerState(0);
        if (GameManager.instance!=null && GameManager.instance.twoPlayer) ;
        UpdatePlayerState(1);
    }

    public class InputState
    {
        public bool left, right, up, down;
        public bool shoot, bomb, options, auto, beam, extra1, extra2, extra3;
    }

    public class ButtonMapping
    {
        public byte shoot = 0;
        public byte bomb = 1;
        public byte options = 2;
        public byte auto = 3;
        public byte beam = 4;
        public byte extra1 = 5;
        public byte extra2 = 6;
        public byte extra3 = 7;
    }

    public class AxisMapping
    {
        public byte horizontal = 0;
        public byte vertical = 1;
    }

    public int DetectControllerButtonPress()
    {
        int result = -1;
        
        for(int j=0;j<6;j++)
        {
            for (int b=0; b<8;b++)
            {
                if (Input.GetButton(playerButtonsNames[j, b])) return j;
            }
        }
        return result;
    }

    public int DetectKeyPress()
    {
        foreach(KeyCode key in allKeyCodes)
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
        if(DetectKeyPress()>-1)
        {
            playerController[playerIndex] = -1;
            playerUsingKeys[playerIndex] = true;
            Debug.Log("Player"+playerIndex+"is set Keyboard " + controller);
            return true;
        }
        return false; 
     }
    
}