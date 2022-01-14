/// <summary>
/// Alejandro Munilla, January 12th, 2022
/// Originally from Unity Assets, Third Person, I made some modifications, and I added all buttons to handle interactions and scripts for those interactions. 
/// This scripts also relies strongly on Colliders interactions, which is handled by TriggerEnter and TriggerExits. 
/// I quickly developed for one player then I expanded to a second player, however I believe this could be improved and become nicer script by unifying 
/// more actions and interactions from Player1 / Player2, instead of defining if then for Player1 and then for Player2. 
/// </summary>


using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


[RequireComponent(typeof(ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
    public string vertical = "Vertical";
    public string horizontal = "Horizontal";
    public string getButton = "Space";
    public string setButtton = "E";
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    private string facingRaw = "No";
    private MyGUI myGui;
    private Text text1;
    private Text pic1;
    private Text text2;
    private Text pic2;
    private int clientNo;
    private GameController gameController;
    private float pickingTime = 5;
    private Animator anim;
    


    private void Start()
    {
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        gameController = gc.GetComponent<GameController>();
        anim = GetComponent<Animator>();
        myGui = gc.GetComponent<MyGUI>();
        text1 = gc.transform.Find("Canvas/Table1/Text").GetComponent<Text>();
        pic1 = gc.transform.Find("Canvas/Table1/Text/Pick").GetComponent<Text>();
        text2 = gc.transform.Find("Canvas/Table2/Text").GetComponent<Text>();
        pic2 = gc.transform.Find("Canvas/Table2/Text/Pick").GetComponent<Text>();


    }

    private void Update()
    {

        if (CrossPlatformInputManager.GetButtonUp(getButton))
        { 
            //qwe dont get anything from the trash
            if (facingRaw != "Trash")
            {
                Debug.Log("No trash");
                if (facingRaw != "No" && facingRaw != "Table1" && facingRaw != "Table2" && facingRaw != "Client")
                {
                    Debug.Log("facing raw");
                    if (gameObject.name == "Player1")
                    {
                        Debug.Log("Player1");
                        if (myGui.slot1P1 == "X")
                        {
                            myGui.slot1P1 = facingRaw;
                        }
                        else
                        {
                            if (myGui.slot2P1 == "X")
                            {
                                myGui.slot2P1 = facingRaw;
                            }
                        }
                    }
                    else
                    {
                        if (myGui.slot1P2 == "X")
                        {
                            myGui.slot1P2 = facingRaw;
                        }
                        else
                        {
                            if (myGui.slot2P2 == "X")
                            {
                                myGui.slot2P2 = facingRaw;
                            }
                        }
                    }
                }
                else if (facingRaw == "Table1" && text1.text != "X" && gameObject.name == "Player1")
                {
                    myGui.mix1 = text1.text;
                    text1.text = "X";

                }
                else if (facingRaw == "Table2" && text2.text != "X" && gameObject.name == "Player2")
                {
                    myGui.mix2 = text2.text;
                    text2.text = "X";
                }
            }

         
        }

        if (CrossPlatformInputManager.GetButtonUp(setButtton))
        {

            if (facingRaw == "Table1" && gameObject.name == "Player1")
            {
                if (myGui.slot1P1 != "X")
                {
                    if (text1.text == "X")
                    {
                        text1.text = myGui.slot1P1;
                    }
                    else
                    {
                        text1.text = text1.text + myGui.slot1P1;
                    }
                    pic1.text = "Picking " + myGui.slot1P1;
                    myGui.slot1P1 = "X";
                    DisableThis();


                }
                else if (myGui.slot2P1 != "X")
                {             
          
                    if (text1.text == "X")
                    {
                        text1.text = myGui.slot2P1;
                    }
                    else
                    {
                        text1.text = text1.text + myGui.slot2P1;
                    }
                    pic1.text = "Picking " + myGui.slot2P1;
                    myGui.slot2P1 = "X";
                    DisableThis();

                }
            }
            else if (facingRaw == "Table2" && gameObject.name == "Player2")
            {
                if (myGui.slot1P2 != "X")
                {
                    if (text2.text == "X")
                    {
                        text2.text = myGui.slot1P2;
                    }
                    else
                    {
                        text2.text = text2.text + myGui.slot1P2;
                    }
                    pic2.text = "Picking " + myGui.slot1P2;
                    myGui.slot1P2 = "X";
                    DisableThis();


                }
                else if (myGui.slot2P2 != "X")
                {

                    if (text2.text == "X")
                    {
                        text2.text = myGui.slot2P2;
                    }
                    else
                    {
                        text2.text = text2.text + myGui.slot2P2;
                    }
                    pic2.text = "Picking " + myGui.slot2P2;
                    myGui.slot2P2 = "X";
                    DisableThis();

                }
            }
            else if (facingRaw == "Client")
            {
                if (gameObject.name == "Player1")
                {
                    gameController.CheckDelivery(gameObject, myGui.mix1, clientNo);
                }
                else
                {
                    gameController.CheckDelivery(gameObject, myGui.mix2, clientNo);
                }
                
            }
            else if (facingRaw == "Trash")
            {
                if (gameObject.name == "Player1")
                {
                    if (myGui.mix1 != "No")
                    {
                        int penaltyPoints = myGui.mix1.Length * 5;
                        myGui.score1 -= penaltyPoints;
                        myGui.mix1 = "No";

                    }
                    else
                    {
                        if (myGui.slot1P1 != "X")
                        {
    
                            myGui.slot1P1 = "X";
                            myGui.score1 -= 5;


                        }
                        else if (myGui.slot2P1 != "X")
                        {


                            myGui.slot2P1 = "X";
                            myGui.score1 -= 5;

                        }
                    }

                }
                if (gameObject.name == "Player2")
                {
                    if (myGui.mix2 != "No")
                    {
                        int penaltyPoints = myGui.mix2.Length * 5;
                        myGui.score2 -= penaltyPoints;
                        myGui.mix2 = "No";

                    }
                    else
                    {
                        if (myGui.slot1P2 != "X")
                        {

                            myGui.slot1P2 = "X";
                            myGui.score2 -= 5;


                        }
                        else if (myGui.slot2P2 != "X")
                        {


                            myGui.slot2P2 = "X";
                            myGui.score2 -= 5;

                        }
                    }

                }

            }
        }       
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis(horizontal);
        float v = CrossPlatformInputManager.GetAxis(vertical);
        bool crouch = false;


        m_Move = v * Vector3.forward + h * Vector3.right;

#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

        // pass all parameters to the character control script
        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;
    }

    private void OnTriggerEnter(Collider col)
    {
    //    Debug.Log(col.name + col.tag);
        if (col.tag == "Raw")
        {
            facingRaw = col.name;
            
        }
        else
        {
            facingRaw = "No";
        }

        if (col.name == "Table1" || col.name == "Table2")
        {
            facingRaw = col.name;
        }



        if (col.tag == "Client")
        {
            facingRaw = "Client";
            clientNo = int.Parse(col.name);
        }

        if (col.name == "PickUp")
        {
            PickUps pickUps = col.GetComponent<PickUps>();
            if (pickUps.player == gameObject.name)
            {
                if (pickUps.boost == "Time")
                {                    
                    if (gameObject.name == "Player1")
                    {
                        myGui.timer1 += 50;
                    }
                    else
                    {
                        myGui.timer2 += 50;
                    }
                    col.gameObject.SetActive(false);
                }
                else if (pickUps.boost == "Score")
                {
                    if (gameObject.name == "Player1")
                    {
                        myGui.score1 += 50;
                    }
                    else
                    {
                        myGui.score2 += 50;
                    }
                    col.gameObject.SetActive(false);
                }
                else if (pickUps.boost == "Speed")
                {
                    m_Character.m_AnimSpeedMultiplier = 1.3f;
                    m_Character.m_MoveSpeedMultiplier = 1.3f;
                    Invoke("SlowToNormal", 20);
                    col.gameObject.SetActive(false);
                }


            }
        }

        if (col.name == "Trash")
        {
            facingRaw = "Trash";
        }

        
    }

    private void OnTriggerExit(Collider other)
    {

            facingRaw = "No";

    }


    private void InvokeEnable()
    {
        this.enabled = true;
        if (gameObject.name == "Player1")
        {
            pic1.text = "";
        }
        else
        {
            pic2.text = "";
        }
       
        Debug.Log("Enable");

    }

    private void DisableThis ()
    {
        anim.SetFloat("Forward", 0);
        Invoke("InvokeEnable", pickingTime);
        this.enabled = false;
    }

    private void SlowToNormal ()
    {
        m_Character.m_AnimSpeedMultiplier = 1;
        m_Character.m_MoveSpeedMultiplier = 1;
    }
}



