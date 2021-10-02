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

    void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Rock"))
        {
           lastRock = null;
        }
    }

    void Update()
    {
        if(lastRock != null)
        {
            
        }
    }

}
