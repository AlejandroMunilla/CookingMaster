using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGUI : MonoBehaviour
{
    public float timer1 = 240;
    public float timer2 = 240;
    private float timeToAdd = 20.0f;
    private float maxTime = 500;
    public int score1 = 100;
    public int score2 = 50;
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
        myStyle = new GUIStyle();
        myStyle.fontSize = (int)(Screen.height * 0.04f);
        myStyle.normal.textColor = Color.black;

        slotStyle = new GUIStyle();
        slotStyle.fontSize = (int)(Screen.height * 0.08f);
        slotStyle.normal.textColor = Color.black;



        labelWidth = (int)(Screen.width * 0.10);
        labelHeight = (int)(Screen.height * 0.05f);
        buttonWidth = (int)(Screen.width * 0.05f);
        int column1 = (int)(Screen.width * 0.10f);
        int column2 = (int)(Screen.width * 0.75f);
        int columnAdd1 = (int)(Screen.width * 0.02f);
        int columnAdd2 = (int)(Screen.width * 0.94f);


        timer1Rect = new Rect(column1, Screen.height * 0.05f, labelWidth, labelHeight);
        timer2Rect = new Rect(column2, Screen.height * 0.05f, labelWidth, labelHeight);
        score1Rect = new Rect(column1, Screen.height * 0.05f + labelHeight, labelWidth, 2 * labelHeight); 
        score2Rect = new Rect(column2, Screen.height * 0.05f + labelHeight, labelWidth, 2 *labelHeight); 
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

        gameController = GetComponent<GameController>();
    }

    private void OnGUI()
    {
        GUI.Label(timer1Rect, "Time: " + timer1.ToString("f0"), myStyle);
        GUI.Label(timer2Rect, "Time: " + timer2.ToString("f0"), myStyle);
        GUI.Label(score1Rect, "Score: " + score1.ToString(), myStyle);
        GUI.Label(score2Rect, "Score: " + score2.ToString(), myStyle);

        GUI.Button(addSpeedRect1, speed);
        GUI.Button(addSpeedRect2, speed);
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

        if (mix1 == "No")
        {
            GUI.Label(slot1Player1Rect, slot1P1, slotStyle);
            GUI.Label(slot2Player1Rect, slot2P1, slotStyle);
        }
        else
        {
            GUI.Label(slot1Player1Rect, mix1, myStyle);
        }

        GUI.Label(slot1Player2Rect, slot1P2, slotStyle);
        GUI.Label(slot2Player2Rect, slot2P2, slotStyle);

        for (int cnt = 0; cnt < gameController.timeClients.Count; cnt++)
        {
            GUI.Label (new Rect((Screen.width * 0.34f) + (cnt * (Screen.width * 0.083f)), Screen.height * 0.15f, labelWidth, labelHeight), gameController.timeClients[cnt].ToString("f0"), myStyle);
        }

        //    GUI.Label ()
    }

    private void Update()
    {
        timer1 -= Time.deltaTime;
        timer2 -= Time.deltaTime;
        for (int cnt = 0;cnt < gameController.timeClients.Count;cnt++)
        {
            gameController.timeClients[cnt] -= Time.deltaTime * gameController.timerSpeed[cnt];
        }

        if (timer1 < 0)
        {
            timer1 = 0;
        }
        if (timer2 < 0)
        {
            timer2 = 0;
        }
    }
}
