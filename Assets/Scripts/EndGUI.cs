///This class handles all GUI elements during the game. There is a different GUI handling the post game (score board, restart or quit application)
///Elements:
///1) Normal GUI with different actions. 
///2) At the end of the game, it restrieve information stored on computer with previous players and scores, showing the top highest score
///3) Ask the name of the player who won that round
///4) Save new name / score along previous scores. If name exist, it doesnt add another name, it just get the highest value of two. 
///WARNING: it could be possible to save same names, but with some changes, as the Dictionary would throw an error if we tried to add an element
///with the same key. To do that, it could be possible to add an internal ID for each element, making a different Dictionary. ID would be the keys 
///of the dictionary, but they wont be displayed on the score to players. 
///Alejandro Munilla, January 14th, 2022


using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Linq;

public class EndGUI : MonoBehaviour
{
    private int buttonWidth;
    private int buttonHeight;
    private int score;
    private string messageWins;
    private Rect playerWinsRect;
    private Rect restartRect;
    private Rect quitRect;
    private Rect enterNameRect;
    private Rect windowRect;
    private string nameInput = "";
    private GUIStyle myStyle;
    private MyGUI myGui;
    private string initialData;
    public Texture2D background;
    private GameController gc;
    IOrderedEnumerable<KeyValuePair<string, int>> orderedList;
    SortedDictionary <string, int> myDict = new SortedDictionary<string, int>();



    // Every time this script is enable, it will load saved data. LoadData () function. Basically the names and scores of previous players and games
    void OnEnable()
    {
        myDict.Clear();
        myGui = GetComponent<MyGUI>();
        gc = GetComponent<GameController>();
        windowRect = new Rect(0, 0, Screen.width, Screen.height);
        buttonWidth = (int)(Screen.width * 0.12f);
        buttonHeight = (int)(Screen.height * 0.05f);
        playerWinsRect = new Rect(Screen.width * 0.02f, Screen.height * 0.1f, Screen.width * 0.4f, buttonHeight);
        restartRect = new Rect (Screen.width * 0.02f, Screen.height* 0.3f, buttonWidth, buttonHeight);
        quitRect = new Rect((Screen.width * 0.02f ) + buttonWidth, Screen.height * 0.3f, buttonWidth, buttonHeight);
        enterNameRect = new Rect(Screen.width * 0.02f, (Screen.height * 0.3f) + buttonHeight , buttonWidth, buttonHeight);
        myStyle = new GUIStyle();
        myStyle.fontSize = (int)(Screen.height * 0.04f);
        myStyle.normal.textColor = Color.black;

        if (myGui.score1 > myGui.score2)
        {
            messageWins = "Player1 wins! Enter your name";
            score = myGui.score1;
        }
        else if (myGui.score1 == myGui.score2)
        {
            messageWins = "Draw!!! Both players got the same score! Amazing!";
            score = myGui.score1;
        }
        else
        {
            messageWins = "Player2 wins! Enter your name";
            score = myGui.score2;
        }
        LoadData();
        string[] splitInitialData = null;

        //If initialData exist as a file, we use it. 
        //It would be in the format "Key1*Value1/Key2*Value2/....." So we aplit by / for the pair of values, and we add pair of values to dictionary 
        //by splitting those pairs separated by *
        if (initialData != null)
        {
            splitInitialData = initialData.Split(new string[] { "/" }, StringSplitOptions.None);
            foreach (string data in splitInitialData)
            {
                string[] splitNames = data.Split(new string[] { "*" }, StringSplitOptions.None);
                if (data != "")
                {
                    myDict.Add(splitNames[0], int.Parse(splitNames[1]));
                }
                else
                {
                    Debug.Log("nothing");
                }
            }
        }
        //If file doesnt exist... create it
        else
        {
            string path = Application.persistentDataPath + "/Eternalia";
            Debug.Log(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);


            }
            BinaryFormatter bf = new BinaryFormatter();
            string fileName = path + "/CurrentProfile.dat";
            //     Debug.Log(fileName);
            FileStream file = File.Create(fileName);

        }

        ///Dictionary are hashtables with no order. We need to order keys by values, store them on "orderedList" so we later on we could use it
        ///to display a score board with the top 10 highest score in descending order. 
        orderedList = myDict.OrderByDescending(i => i.Value);



    }

    private void OnGUI()
    {
        GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), background);
        GUI.Window(0, windowRect, MyWindow, "END GAME");
    }


    private void MyWindow (int windowID)
    {
        //Message displaying which player won
        GUI.Label(playerWinsRect, messageWins, myStyle);

        //Restart level (play again) or quit application. 
        if (GUI.Button(restartRect, "RESTART"))
        {
            if (nameInput != "")
            {
                RestartLevel();
            }
        }
        if (GUI.Button(quitRect, "QUIT"))
        {
            if (nameInput != "")
            {
                SaveData();
                Application.Quit();
            }
        }

        //Name of the player so it may be saved and go to the score board (assuming it is on the top 10)
        nameInput = GUI.TextField(enterNameRect, nameInput);


        //Sort of a for loop... using a foreach :) Funny. 
        int i = 0;
        foreach (var entry in orderedList)
        {
            GUI.Label(new Rect(Screen.width * 0.6f, Screen.height * 0.2f + (i * buttonHeight), Screen.width * 0.2f, buttonHeight), entry.Key, myStyle);
            GUI.Label(new Rect(Screen.width * 0.8f, Screen.height * 0.2f + (i * buttonHeight), Screen.width * 0.2f, buttonHeight), entry.Value.ToString(), myStyle);
            i++;
            //We only need the first 10 highest score
            if (i > 10)
            {
                break;
            }
        }

    }


    private void LoadData ()
    {
        string path = Application.persistentDataPath + "/Eternalia";
        string fileName = path + "/CurrentProfile.dat";
        //     Debug.Log(fileName);
        if (File.Exists(fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fileName, FileMode.Open);
            SaveScores data = (SaveScores)bf.Deserialize(file);
            file.Close();
            initialData  = data.saveData;

            Debug.Log(initialData);
            //          Debug.Log(mainPlayerName);
        }
    }

    //We save data with a binary formatter. We save it as string as "Key1*Value1/Key2*Value2...." etc. 
    private void SaveData ()
    {
        string path = Application.persistentDataPath + "/Eternalia";
   //     Debug.Log(path);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        BinaryFormatter bf = new BinaryFormatter();
        string fileName = path + "/CurrentProfile.dat";
        FileStream file = File.Create(fileName);
        SaveScores data = new SaveScores();
        data.saveData = initialData + nameInput + "*" + score + "/";
        bf.Serialize(file, data);
        file.Close();
    }


    private void RestartLevel ()
    {
        
        if (myDict.ContainsKey(nameInput))
        {
            if (myDict[nameInput] <= score)
            {
                myDict[nameInput] = score;
            }
        }
        SaveData();
        GetComponent<GameController>().enabled = true;
        foreach (GameObject go in myGui.players)
        {
            Animator anim = go.GetComponent<Animator>();
            anim.SetFloat("Forward", 0);
            anim.SetFloat("Turn", 0);
            go.GetComponent<ThirdPersonUserControl>().enabled = true;

        }
        myGui.timer1 = myGui.initialTime;
        myGui.timer2 = myGui.initialTime;
        myGui.score1 = 0;
        myGui.score2 = 0;
        myGui.enabled = true;
        GetComponent<GameController>().enabled = true;
        Transform orders = transform.Find("Canvas/Orders");
        orders.gameObject.SetActive(true);
        foreach (Transform ta in orders)
        {
            if (ta.gameObject.activeSelf == false)
            {
                ta.gameObject.SetActive(true);
            }

              this.enabled = false;

        }

    }
}


//This class is used to save data across different rounds or even game session. Used for the binary formatter way of saving information. 

[Serializable]
class SaveScores
{
    public string saveData;
}
