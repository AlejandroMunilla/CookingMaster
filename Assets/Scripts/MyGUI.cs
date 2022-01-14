///Alejandro Munilla, January 12th, 2022
///Script GUI elements. I also added some non-script GUI elements, directly from a Canvas places on World Space, so I could show you that I know how to work 
///with both GUI ways of Unity. It also keeps track of the countdown timers for both players, so once both reach zero, game stops showing the 
///winner. 

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGUI : MonoBehaviour
{
    public float timer1 = 240;
    public float timer2 = 240;
    public float initialTime = 240;
    private float timeToAdd = 20.0f;
    private float maxTime = 500;
    public int score1 = 100;
    public int score2 = 50;
    private bool started = false;
    private int scoreToAdd = 20;
    private int labelWidth;
    private int labelHeight;
    private int buttonWidth;
    public string slot1P1 = "X";
    public string slot2P1 = "X";
    public string slot1P2 = "X";
    public string slot2P2 = "X";
    public string mix1 = "No";
    public string mix2 = "No";
    private Rect timer1Rect;
    private Rect timer2Rect;
    private Rect score1Rect;
    private Rect score2Rect;
    private Rect addSpeedRect1;
    private Rect addSpeedRect2;
    private Rect addTimeRect1;
    private Rect addTimeRect2;
    private Rect addScoreRect1;
    private Rect addScoreRect2;
    private Rect slot1Player1Rect;
    private Rect slot2Player1Rect;
    private Rect slot1Player2Rect;
    private Rect slot2Player2Rect;
    private Rect exitRect;

    private Transform ordersTa;
    public List<GameObject> players = new List<GameObject>();
    public List<GameObject> clientTimes = new List<GameObject>();  
    
    private GUIStyle myStyle;
    private GUIStyle slotStyle;
    private GUIStyle mixStyle;

    public Texture2D speed;
    public Texture2D time;
    public Texture2D score;

    //   public List<string> choppingTable1 = new List<string>();
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        if (started == false)
        {
            //Styles for different usages by labels, etc. Using % of screen resolutions allows to adapt with different screen resolutions of different devices
            myStyle = new GUIStyle();
            myStyle.fontSize = (int)(Screen.height * 0.04f);
            myStyle.normal.textColor = Color.black;

            slotStyle = new GUIStyle();
            slotStyle.fontSize = (int)(Screen.height * 0.08f);
            slotStyle.normal.textColor = Color.black;


            //I like casting as int instead of large float numbers. 
            labelWidth = (int)(Screen.width * 0.10);
            labelHeight = (int)(Screen.height * 0.05f);
            buttonWidth = (int)(Screen.width * 0.05f);
            int column1 = (int)(Screen.width * 0.10f);
            int column2 = (int)(Screen.width * 0.75f);
            int columnAdd1 = (int)(Screen.width * 0.02f);
            int columnAdd2 = (int)(Screen.width * 0.94f);

            //All Rect used by this script, to optimize code. 
            timer1Rect = new Rect(column1, Screen.height * 0.05f, labelWidth, labelHeight);
            timer2Rect = new Rect(column2, Screen.height * 0.05f, labelWidth, labelHeight);
            score1Rect = new Rect(column1, Screen.height * 0.05f + labelHeight, labelWidth, 2 * labelHeight);
            score2Rect = new Rect(column2, Screen.height * 0.05f + labelHeight, labelWidth, 2 * labelHeight);
            addSpeedRect1 = new Rect(columnAdd1, Screen.height * 0.20f, buttonWidth, buttonWidth);
            addSpeedRect2 = new Rect(columnAdd2, Screen.height * 0.20f, buttonWidth, buttonWidth);
            addTimeRect1 = new Rect(columnAdd1, Screen.height * 0.20f + buttonWidth, buttonWidth, buttonWidth);
            addTimeRect2 = new Rect(columnAdd2, Screen.height * 0.20f + buttonWidth, buttonWidth, buttonWidth);
            addScoreRect1 = new Rect(columnAdd1, Screen.height * 0.20f + (2 * buttonWidth), buttonWidth, buttonWidth);
            addScoreRect2 = new Rect(columnAdd2, Screen.height * 0.20f + (2 * buttonWidth), buttonWidth, buttonWidth);
            slot1Player1Rect = new Rect(columnAdd1, Screen.height * 0.60f, buttonWidth, buttonWidth);
            slot2Player1Rect = new Rect(columnAdd1, Screen.height * 0.60f + buttonWidth, buttonWidth, buttonWidth);
            slot1Player2Rect = new Rect(columnAdd2, Screen.height * 0.60f, buttonWidth, buttonWidth);
            slot2Player2Rect = new Rect(columnAdd2, Screen.height * 0.60f + buttonWidth, buttonWidth, buttonWidth);
            exitRect = new Rect(Screen.width * 0.45f, labelHeight, Screen.width * 0.1f, labelHeight);

            gameController = GetComponent<GameController>();
            GameObject[] arrayPlayers = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < arrayPlayers.Length; i++)
            {
                players.Add(arrayPlayers[i]);
                //        Debug.Log (arrayPlayers[i]);
            }


            ordersTa = transform.Find("Canvas/Orders");
            for (int i = 0; i < ordersTa.childCount; i++)
            {
                clientTimes.Add (ordersTa.GetChild(i).Find("Time").gameObject);
                Debug.Log(ordersTa.GetChild(i).Find("Time").gameObject);
            }
        }

    }

    public void OnEnable()
    {
        if (started == true)
        {
            timer1 = 240;
            timer2 = 240;
            score1 = 0;
            score2 = 0;
        }

    }

    private void OnGUI()
    {
        if (GUI.Button(exitRect, "QUIT"))
        {
            Application.Quit();
        }
        GUI.Label(timer1Rect, "Time: " + timer1.ToString("f0"), myStyle);
        GUI.Label(timer2Rect, "Time: " + timer2.ToString("f0"), myStyle);
        GUI.Label(score1Rect, "Score: " + score1.ToString(), myStyle);
        GUI.Label(score2Rect, "Score: " + score2.ToString(), myStyle);

        GUI.Button(addSpeedRect1, speed);
        GUI.Button(addSpeedRect2, speed);

        //These are the buttons to add speed, score or time
        if( GUI.Button(addTimeRect1, time))
        {
            
            timer1 = timer1 + timeToAdd;
            if (timer1 > maxTime) timer1 = maxTime;
        }
        if (GUI.Button(addTimeRect2, time))
        {

            timer2 = timer2 + timeToAdd;
            if (timer2 > maxTime) timer2 = maxTime;
        }
       
        if (GUI.Button(addScoreRect1, score))
        {
            score1 = score1 + scoreToAdd;
        }
        if (GUI.Button(addScoreRect2, score))
        {
            score2 = score2 + scoreToAdd;
        }

        //This handles to show raw food or mixed food. I do it this way because for a real case app, I would expect to have different textures 
        //to instantiate for raw elements and mixtures of chopped elements. 
        if (mix1 == "No")
        {
            GUI.Label(slot1Player1Rect, slot1P1, slotStyle);
            GUI.Label(slot2Player1Rect, slot2P1, slotStyle);
        }
        else
        {
            GUI.Label(slot1Player1Rect, mix1, myStyle);
        }

        if (mix2 == "No")
        {
            GUI.Label(slot1Player2Rect, slot1P2, slotStyle);
            GUI.Label(slot2Player2Rect, slot2P2, slotStyle);
        }
        else
        {
            GUI.Label(slot1Player2Rect, mix2, myStyle);
        }



        //    GUI.Label ()
    }

    private void Update()
    {
        //Countdown for clients before they leave if they are not served on time. 
        for (int cnt = 0; cnt < gameController.timeClients.Count; cnt++)
        {

            clientTimes[cnt].GetComponent<Text>().text = gameController.timeClients[cnt].ToString("f0");
            //GUI.Label (new Rect((Screen.width * 0.34f) + (cnt * (Screen.width * 0.083f)), Screen.height * 0.15f, labelWidth, labelHeight), gameController.timeClients[cnt].ToString("f0"), myStyle);
        }

        timer1 -= Time.deltaTime;
        timer2 -= Time.deltaTime;

        for (int cnt = 0;cnt < gameController.timeClients.Count;cnt++)
        {
            if (gameController.timeClients[cnt] > 0)
            {
                gameController.timeClients[cnt] -= Time.deltaTime * gameController.timerSpeed[cnt];

                //if clients are not served before countdown reaches zero, they leave. 
                if (gameController.timeClients[cnt] <= 0)
                {
                    gameController.timeClients[cnt] = 0;
                    gameController.clients.GetChild(cnt).gameObject.SetActive(false);
                    gameController.transform.Find("Canvas/Orders").GetChild(cnt).gameObject.SetActive(false);
                }
            }
        }

        if (timer1 < 0)
        {
            timer1 = 0;
        }
        if (timer2 < 0)
        {
            timer2 = 0;
        }
        if (timer1 == 0 && timer2 == 0)
        {
            gameController.enabled = false;

            foreach (GameObject go in players)
            {
                Animator anim = go.GetComponent<Animator>();
                anim.SetFloat("Forward", 0);
                anim.SetFloat("Turn", 0);
                go.GetComponent<ThirdPersonUserControl>().enabled = false;
            }
        
            this.enabled = false;
            transform.Find("Canvas/Order").gameObject.SetActive(false);
            GetComponent<EndGUI>().enabled = true;
        }
    }

  //  private void End
}
