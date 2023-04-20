using UnityEngine;

namespace Scenes.Christian.Scripts
{
    public class WallSpawner : MonoBehaviour
    {
        public GameObject wallPrefab;
        public float spawnInterval;
        public float wallSpeed;
        private float lastSpawnTime;

        void Update()
        {
            if (Time.time - lastSpawnTime > spawnInterval) // checks whether enough time has passed since the last wall was spawned.
            {
                SpawnWall();
                lastSpawnTime = Time.time; // based on since the game started,
            }
        }

        void SpawnWall()
        {
            GameObject wall = Instantiate(wallPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = wall.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0, -wallSpeed);
        }
    }
}