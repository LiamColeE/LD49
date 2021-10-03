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

              
            }
        }
    }

}
