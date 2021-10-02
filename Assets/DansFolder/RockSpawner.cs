using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] bool autoSpawn;
    [SerializeField] float spawnRate;
    [SerializeField] Rock rockPrefab;

    void Start()
    {
        if(autoSpawn)
        StartCoroutine(AutoSpawn());
    }

    IEnumerator AutoSpawn()
    {
        yield return new WaitForSeconds(spawnRate);

        Spawn(transform.position);

        StartCoroutine(AutoSpawn());
    }

    public void Spawn(Vector3 spawnPosition)
    {
        Rock newRock = Instantiate(rockPrefab);
        
        newRock.transform.position = spawnPosition;
    }


}
