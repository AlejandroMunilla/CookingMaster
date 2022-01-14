using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Alejandro Munilla, January 12th, 2022
/// this class is attached to pick ups to handle 1) random instantiate 2) visual effect of spinning the pick up
/// </summary>

public class PickUps : MonoBehaviour
{

    public string boost = "Time";                      //Default Time boost, however at the time of spawning, this is changed by GameController
    public string player = "Player1";                  //Default Player1, although it is changed by GameController at the time this object is instantiated
                                                       //player variable tells which Player may pick up this boost. 


    void OnEnable()
    {
        //This spawn the object randomly in the walkable square. 
        //Another way to set this up could be by adding four points to the scenary, those 4 gameobjects children of GameController object, and
        //the spawning point would be the coordinates inside those four gameobjects. That way if our level designer or artist moved around the 
        //scenario, it would still work. 
        int x = Random.Range(0, 6);
        int z = Random.Range(0, 3);


        transform.position = new Vector3(x, 0.3f, z);

    }

    // Update is called once per frame
    void Update()
    {
        //This create the visual effect or spinning the pickup. 
        transform.RotateAround(transform.position, transform.right, Time.deltaTime * 180f);
    }
}
