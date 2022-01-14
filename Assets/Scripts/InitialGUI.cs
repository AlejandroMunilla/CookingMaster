///This class is the initial menu when app is executed. From here, we either go to the game itself, or quit app

using UnityEngine.SceneManagement;
using UnityEngine;

public class InitialGUI : MonoBehaviour
{

    private Rect backgroundRect;
    private Rect startRect;
    private Rect quitRect;
    private Rect logoRect;
    private Rect veggiesRect;
    private Rect helpRect;
    private int buttonWidth;
    private int buttonHeight;
    private string help = "Player1 keys; WASD for movement, Space to get veggies, E to release them. Player2 Keys: Arrows for movement, blocknumber 2 to get veggines, 3 to release them ";
    private Texture2D background;
    private Texture2D logo;
    private Texture2D veggies;
    private GUIStyle myStyle;

    void Start()
    {
        myStyle = new GUIStyle();
        myStyle.fontSize = (int)(Screen.height * 0.02f);
        myStyle.normal.textColor = Color.black;
        buttonHeight = (int)(Screen.height * 0.08f);
        buttonWidth = (int)(Screen.width * 0.20f);

        backgroundRect = new Rect (0, 0, Screen.width, Screen.height);
        startRect = new Rect (Screen.width * 0.1f, Screen.height * 0.2f, buttonWidth, buttonHeight);
        quitRect = new Rect (Screen.width * 0.1f, Screen.height * 0.2f + buttonHeight, buttonWidth, buttonHeight);
        logoRect = new Rect(Screen.width * 0.1f, Screen.height * 0.8f, Screen.width * 0.12f, Screen.width * 0.12f * 0.8f);
        veggiesRect = new Rect(Screen.width * 0.4f, Screen.height * 0.1f, Screen.width * 0.55f, Screen.width * 0.55f);
        helpRect = new Rect(Screen.width * 0.02f, Screen.height * 0.05f, Screen.width * 0.2f, buttonHeight *4);
        background = (Texture2D) Resources.Load("Background");
        logo = (Texture2D)Resources.Load("Logo");
        veggies = (Texture2D)Resources.Load("Veggies");

    }

    private void OnGUI()
    {
        GUI.DrawTexture(backgroundRect, background);
        GUI.DrawTexture(logoRect, logo);
        GUI.DrawTexture(veggiesRect, veggies);
        if (GUI.Button(startRect, "START"))
        {
            SceneManager.LoadScene("Game");
        }
        if (GUI.Button(quitRect, "QUIT"))
        {
            Application.Quit();
        }
        GUI.Label(helpRect, help, myStyle);
    }
}
