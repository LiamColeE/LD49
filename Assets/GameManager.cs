using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Dictionary<string, GameObject> connectedRocks = new Dictionary<string, GameObject>();
    public float totalHeight;

    public float score;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        GameObject[] rockList = GameObject.FindGameObjectsWithTag("Rock");

        foreach (GameObject rock in rockList)
        {
            //TODO:add connected rocks to connected rocks
        }
    }

    public void SetHeight(float height)
    {
        totalHeight = height;
    }
}
