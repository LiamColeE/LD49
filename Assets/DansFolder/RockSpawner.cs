using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] bool autoSpawn;
    [SerializeField] float spawnRate;
    [SerializeField] Rock rockPrefab;
    [SerializeField] int spawnLimit;

    int mySpawnTotal;
    void Start()
    {
        if(autoSpawn)
        StartCoroutine(AutoSpawn());
    }

    IEnumerator AutoSpawn()
    {
        yield return new WaitForSeconds(spawnRate);

        Spawn(transform.position);

        if(mySpawnTotal < spawnLimit)
            StartCoroutine(AutoSpawn());
    }

    public void Spawn(Vector3 spawnPosition)
    {
        if(mySpawnTotal < spawnLimit)
        {
            Rock newRock = Instantiate(rockPrefab);
            
            newRock.transform.position = spawnPosition;

            mySpawnTotal++;
        }
    }



}
