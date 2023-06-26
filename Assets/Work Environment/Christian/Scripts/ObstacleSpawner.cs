using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Christian.Scripts
{
    public class ObstacleSpawner : MonoBehaviour
    {
        public GameObject obstaclePrefab;
        public int spawnAmount;
        public float spawnRadius;
        private Vector3 moveDirection = -Vector3.forward;
        public float obstacleSpeed;
        public float destroyDistance; // Distance at which obstacles will be destroyed
        public float spawnInterval; // Time interval between spawns
        private float spawnTimer; // Time elapsed since last spawn

        private List<GameObject> spawnedObstacles = new List<GameObject>(); // List to store references to spawned obstacles

        public List<ObstaclePattern> easyPatterns;
        public List<ObstaclePattern> mediumPatterns;
        public List<ObstaclePattern> hardPatterns;

        [System.Serializable]
        public class ObstaclePattern
        {
            public float patternInterval;
            public List<GameObject> patternObstacles;
        }

        void SpawnObstacles()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
                obstacle.transform.parent = transform;
                Rigidbody obstacleRigidbody = obstacle.GetComponent<Rigidbody>();
                if (obstacleRigidbody != null)
                {
                    obstacleRigidbody.velocity = moveDirection.normalized * obstacleSpeed;
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
            Vector3 spawnPosition;
            float spawnAngle = Random.Range(0f, 360f);
            float spawnDistance = Random.Range(0f, spawnRadius);
            spawnPosition = transform.position + new Vector3(spawnDistance * Mathf.Cos(spawnAngle * Mathf.Deg2Rad), 0f, spawnDistance * Mathf.Sin(spawnAngle * Mathf.Deg2Rad));
            return spawnPosition;
        }

        void Update()
        {
            // Spawn a new obstacle if the spawn interval has elapsed
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnObstacles();
                spawnTimer = 0f;
            }

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

        }
    }
}