using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;
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
            if (facingRaw != "No" && facingRaw != "Table1" && facingRaw != "Client")
            {
                if (gameObject.name == "Player1")
                {
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
            else if (facingRaw == "Table1" && text1.text != "X")
            {
                if (gameObject.name == "Player1")
                {
                    myGui.mix1 = text1.text;
                    text1.text = "X";
                }
                else
                {
                    myGui.mix2 = text2.text;
                    text2.text = "X";
                }
   
            }
        }

        if (CrossPlatformInputManager.GetButtonUp(getButton))
        {

            if (facingRaw == "Table1")
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
            else if (facingRaw == "Client")
            {

                gameController.CheckDelivery(gameObject, myGui.mix1, clientNo);
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
        Debug.Log(col.name + col.tag);
        if (col.tag == "Raw")
        {
            facingRaw = col.name;
            
        }
        else
        {
            facingRaw = "No";
        }

        if (col.name == "Table1")
        {
            facingRaw = "Table1";
        }

        if (col.tag == "Client")
        {
            facingRaw = "Client";
            clientNo = int.Parse(col.name);
            Debug.Log(clientNo);
            Debug.Log(col.name);
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
                    m_Character.m_AnimSpeedMultiplier = 1.5f;
                    m_Character.m_MoveSpeedMultiplier = 1.5f;
                    Invoke("SlowToNormal", 20);
                    col.gameObject.SetActive(false);
                }


            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

            facingRaw = "No";

    }


    private void InvokeEnable()
    {
        this.enabled = true;
        pic1.text = "";
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
        m_Character.m_AnimSpeedMultiplier = 1.0f;
        m_Character.m_MoveSpeedMultiplier = 1.0f;
    }
}



