using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
To use this script, 
simply attach it to a game object in your scene and assign the objectToSpawn and spawnPoint variables in the inspector.
You can also adjust the spawnInterval to change the time interval between spawn

""
*/

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public SpawnArea spawnBounds;
    public float spawnInterval = 1f;
    public float spawnRange = 5f;
    public float lifeDuration = 5f;
    public float overlapRadius = 0.5f;
    private float nextSpawnTime;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnObject()
    {
        Vector3 randomPos = spawnBounds.GetRandomPointInBounds();
        bool isOverlapping = Physics.CheckSphere(randomPos, overlapRadius);

        while (!spawnBounds.bounds.Contains(randomPos) || isOverlapping)
        {
            randomPos = spawnBounds.GetRandomPointInBounds();
            isOverlapping = Physics.CheckSphere(randomPos, overlapRadius);
        }

        GameObject newObject = Instantiate(objectToSpawn, randomPos, Quaternion.identity);
        Destroy(newObject, lifeDuration);
    }
}