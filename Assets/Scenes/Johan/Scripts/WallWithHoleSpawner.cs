using System;
using UnityEngine.Serialization;
using UnityEngine;

namespace Scenes.Christian.Scripts
{
    public class WallWithHoleSpawner : MonoBehaviour
    {
        public GameObject wallPrefab; // the prefab of the wall that will be spawned
        public float spawnInterval; // the interval between wall spawns
        public float wallSpeed; // the speed at which the walls move
        public float spawnArea; // the distance between each wall spawn point on the x-axis
        private float lastSpawnTime; // the time when the last wall was spawned
        private float spawnAreaLeftBoundary; // the left boundary of the spawn area
        private float spawnAreaRightBoundary; // the right boundary of the spawn area
        
        private void Start()
        {
            CalculateSpawnAreaBoundaries(); // calculate the boundaries of the spawn area
        }

        void Update()
        {
            if (Time.time - lastSpawnTime > spawnInterval) // checks whether enough time has passed since the last wall was spawned.
            {
                SpawnWall(); // spawn a wall
                lastSpawnTime = Time.time; // update the time when the last wall was spawned
            }
        }

        void SpawnWall()
        {
            float randomX = UnityEngine.Random.Range(spawnAreaLeftBoundary, spawnAreaRightBoundary); // generate a random x-coordinate within the spawn area
            Vector3 position = transform.position;
            GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity); // instantiate the wall prefab at the spawn position
            wall.transform.parent = transform;

            WallWithHole wallWithHole = wall.GetComponent<WallWithHole>();
            wallWithHole.myMoveDirection = new Vector3(0, 0, -wallSpeed);
            wallWithHole.myMoveSpeed = wallSpeed;
        }
        
        private void CalculateSpawnAreaBoundaries()
        {
            float xPos = transform.position.x; // store the x position in a local variable
            spawnAreaLeftBoundary = xPos - spawnArea / 2; // calculate the left boundary of the spawn area using the local variable
            spawnAreaRightBoundary = xPos + spawnArea / 2; // calculate the right boundary of the spawn area using the local variable
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green; // set the gizmo color to green
            Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea, 1, 1)); // draw a cube gizmo representing the spawn area
        }
    }
}
