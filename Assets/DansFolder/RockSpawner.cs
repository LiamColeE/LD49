using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] bool autoSpawn;
    [SerializeField] float spawnRate;
    [SerializeField] Rock rockPrefab;
    [SerializeField] int spawnLimit;

    List<Rock> rocks = new List<Rock>();

    int mySpawnTotal;
    void Start()
    {
        if(autoSpawn)
        StartCoroutine(AutoSpawn());
    }

    IEnumerator AutoSpawn()
    {
        yield return new WaitForSeconds(spawnRate);

        CleanList();
        if(rocks.Count < spawnLimit)
            Spawn(transform.position);

        StartCoroutine(AutoSpawn());
        
    }

    public void Spawn(Vector3 spawnPosition)
    {
        if(mySpawnTotal < spawnLimit)
        {
            Rock newRock = Instantiate(rockPrefab);
            
            newRock.transform.position = spawnPosition;

            rocks.Add(newRock);
        }
    }

    void CleanList()
    {
        List<Rock> newRocks = new List<Rock>();
        foreach(Rock rock in rocks)
        {
            if(rock != null)
                newRocks.Add(rock);
        }
        rocks.Clear();
        rocks = newRocks;
    }



}
