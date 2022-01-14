///This class controls the camera. It zoom in/out accordingly to players distance. When they are close camera zoom in moving to the middle
///distance in between player + 4 meters high. 
///Alejandro Munilla, January 14th, 2022


using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 initialPos;
    private Transform player1;
    private Transform player2;
    private float maxPlayerDistance = 5.0f;
    private float counter = 0.5f;
    private float rate = 1;
    
    void Start()
    {
        
        initialPos = transform.position;
        GameObject[] arrayPlayers = GameObject.FindGameObjectsWithTag("Player");
        player1 = arrayPlayers[0].transform;
        player2 = arrayPlayers[1].transform;

    }

    void Update()
    {


        Vector3 middlePosition = player1.position + (player1.position - player2.position) / 2;
        Vector3 middleAtHigh = new Vector3 (middlePosition.x, middlePosition.y + 4, middlePosition.z);
        float distPlayers = Vector3.Distance(player1.transform.position, player2.transform.position);
        rate = (maxPlayerDistance - distPlayers )/ maxPlayerDistance;

        //when distance <3 players are sufficiently close to zoom in. Otherwise, camera resets back to original top position. 
        //In both cases I use a Lerp, so we may fine tune it as we wish to make it nicer. 
        if (distPlayers <= 3)
        {
            transform.position = Vector3.Lerp(initialPos, middleAtHigh, rate - 0.1f);
            counter = rate;
        }
        else
        {
            counter -= Time.deltaTime * 4 ;
            transform.position = Vector3.Lerp(initialPos, middleAtHigh, counter);
        }
        
    }
}
