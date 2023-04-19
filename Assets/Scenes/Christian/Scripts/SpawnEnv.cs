using System.Collections.Generic;
using UnityEngine;
public class SpawnEnv : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int numberOfObstacles = 10;
    public float spawnRadius = 10f;
    public Vector3 spawnDirection = Vector3.forward;
    public float obstacleSpeed = 5f;
    public float destroyDistance = 50f; // Distance at which obstacles will be destroyed
    public float spawnInterval = 1f; // Time interval between spawns
    private float spawnTimer = 0f; // Time elapsed since last spawn

    private List<GameObject> spawnedObstacles = new List<GameObject>(); // List to store references to spawned obstacles

    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            Rigidbody obstacleRigidbody = obstacle.GetComponent<Rigidbody>();
            if (obstacleRigidbody != null)
            {
                obstacleRigidbody.useGravity = false;
                obstacleRigidbody.velocity = spawnDirection.normalized * obstacleSpeed;
                spawnedObstacles.Add(obstacle);
            }
            else
            {
                Debug.LogError("No Rigidbody component found on obstacle prefab");
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        float spawnAngle = Random.Range(0f, 360f);
        float spawnDistance = Random.Range(0f, spawnRadius);
        spawnPosition = transform.position + new Vector3(spawnDistance * Mathf.Cos(spawnAngle * Mathf.Deg2Rad), 0f, spawnDistance * Mathf.Sin(spawnAngle * Mathf.Deg2Rad));
        return spawnPosition;
    }

    void Update()
    {
        // Destroy obstacles that have exceeded the destroy distance
        for (int i = spawnedObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = spawnedObstacles[i];
            if (Vector3.Distance(obstacle.transform.position, transform.position) > destroyDistance)
            {
                spawnedObstacles.RemoveAt(i);
                Destroy(obstacle);
            }
        }

        // Spawn a new obstacle if the spawn interval has elapsed
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnObstacles();
            spawnTimer = 0f;
        }
    }
}