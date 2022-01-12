using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public List<string> orders = new List<string>();
    public List<float> timeClients = new List<float>();
    public List<float> timerSpeed = new List<float>();
    private Transform clients;
    private MyGUI myGui;
    // Start is called before the first frame update

    void Start()
    {
        myGui  = GetComponent<MyGUI>();
        clients = transform.Find("Canvas/Clients");
        orders.Add("ACD");
        orders.Add("BEF");
        orders.Add("CE");
        orders.Add("ADF");
        orders.Add("ACF");
        timeClients.Add(120);
        timeClients.Add(120);
        timeClients.Add(120);
        timeClients.Add(120);
        timeClients.Add(120);

        for (int i = 0; i < 5; i++)
        {
            timerSpeed.Add(1.0f);
        }
    }

    public void CheckDelivery(GameObject playerNo, string deliverOrder, int clientNo)
    {
        Debug.Log(playerNo + "/" + deliverOrder + "/" + clientNo);
        if (playerNo.name == "Player1")
        {
            myGui.mix1 = "No";
            myGui.score1 += 20; 
        }
        else if (playerNo.name == "Player1")
        {
            myGui.mix2 = "No";
            myGui.score2 += 20;
        }

        Text myText = clients.transform.Find(clientNo.ToString() + "/Text").gameObject.GetComponent<Text>();
        myText.text = deliverOrder;

        if (deliverOrder == orders[clientNo])
        {
            float seventyPercent = (orders[clientNo].Length * 40) * 0.7f;
            if (timeClients[clientNo] >= seventyPercent)
            {
                Debug.Log("Won Pickup");
                InstantiatePickUp(playerNo.name);
            }
            Debug.Log("Correct");
            myText.color = Color.green;
            timerSpeed[clientNo] = 1.0f;
            timeClients[clientNo] = orders[clientNo].Length * 40;
        }
        else
        {

            Debug.Log("Wrong");
            timerSpeed[clientNo] = 2.0f;
            myText.color = Color.red;
        }

    }


    private void InstantiatePickUp (string playerName)
    {
        GameObject go = Resources.Load<GameObject>("PickUp");
        PickUps pickUps = go.AddComponent<PickUps>();   
        pickUps.player = playerName;        
        int z = Random.Range(0, 3);
        if (z == 0)
        {
             pickUps.boost = "Speed";

        }
        else if (z == 1)
        {
            pickUps.boost = "Score";
        }

        go.GetComponent<PickUps>().enabled = true;
    }


}
