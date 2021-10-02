using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightTracker : MonoBehaviour
{
    [SerializeField] Transform ground;

    Rock lastRock;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Rock"))
        {
           lastRock = collider.GetComponent<Rock>();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider == lastRock)
        {
            Debug.Log("You in there bro");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Rock"))
        {
           lastRock = null;
        }
    }

}
