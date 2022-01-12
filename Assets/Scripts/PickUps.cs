using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{

    public string boost = "Time";
    public string player = "Player1";


    void OnEnable()
    {
        int x = Random.Range(0, 6);
        int z = Random.Range(0, 3);


        transform.position = new Vector3(x, 0.3f, z);

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.right, Time.deltaTime * 180f);
    }
}
