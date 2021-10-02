using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightTracker : MonoBehaviour
{
    [SerializeField] Transform ground;
    [SerializeField] int points;

    Rock lastRock;
    Rock thisRock;

    void OnTriggerStay(Collider collider)
    {
        if(collider.CompareTag("Rock"))
        {
            if(collider.GetComponent<Rock>().settled)
            {

                Debug.Log(collider.GetComponent<Rock>().settled + "Cairn is at height " + (transform.position.y - ground.transform.position.y) + " and you got " + points + " points!");
            }
        }
    }

}
