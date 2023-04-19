using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // The prefab of the object to be spawned
    public Transform player; // The transform of the player
    public Vector3 spawnAreaSize; // The size of the spawn area
    public float spawnInterval = 1f; // The interval between spawns
    public float moveSpeed = 10f; // The speed at which the objects move
    public float avoidDistance = 5f; // The minimum distance between objects
    public float destroyRange = 50f; // The maximum range at which objects are destroyed
    public Vector3 flyDirection = Vector3.right; // The direction in which the objects should fly

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition() + transform.position;
            Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f); // generate a random rotation around the Y axis
            GameObject newObject = Instantiate(objectPrefab, spawnPosition, spawnRotation); // set the rotation of the new object to the random rotation
            newObject.transform.rotation = Quaternion.LookRotation(flyDirection);
            spawnedObjects.Add(newObject);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnBoxMin = -spawnAreaSize / 2f;
        Vector3 spawnBoxMax = spawnAreaSize / 2f;

        int attempts = 10;
        while (attempts-- > 0)
        {
            Vector3 randomPosition = new Vector3(Random.Range(spawnBoxMin.x, spawnBoxMax.x),
                                                 Random.Range(spawnBoxMin.y, spawnBoxMax.y),
                                                 Random.Range(spawnBoxMin.z, spawnBoxMax.z));

            bool overlap = false;
            foreach (GameObject obj in spawnedObjects)
            {
                if (Vector3.Distance(randomPosition, obj.transform.position) < avoidDistance)
                {
                    overlap = true;
                    break;
                }
            }

            if (!overlap)
            {
                return randomPosition;
            }
        }

        return Vector3.zero;
    }

    void Update()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = spawnedObjects[i];
            if (Vector3.Distance(obj.transform.position, player.position) > destroyRange)
            {
                Destroy(obj);
                spawnedObjects.RemoveAt(i);
            }
            else
            {
                obj.transform.Translate(flyDirection.normalized * moveSpeed * Time.deltaTime);
            }
        }
    }
}