using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public TestUIManager testUI;

    private Vector3 moveDirection = -Vector3.forward;
    public float obstacleSpeed;
    public float destroyDistance; // Distance at which obstacles will be destroyed
    public float spawnInterval; // Time interval between spawns
    private bool isSpawningObstacles;

    private List<GameObject> spawnedObstacles = new List<GameObject>(); // List to store references to spawned obstacles

    public List<ObstacleSequence> obstacleSequences;

    [System.Serializable]
    public class ObstacleSequence
    {
        public List<GameObject> obstaclePatterns;
    }

    public void StartSpawnSequence()
    {
        if (isSpawningObstacles == false)
        {
            StartCoroutine(ObstacleRoutine());
        }
    }

    public IEnumerator ObstacleRoutine()
    {
        isSpawningObstacles = true;

        int sequenceID = Random.Range(0, obstacleSequences.Count);
        List<GameObject> thisSequence = new List<GameObject>(obstacleSequences[sequenceID].obstaclePatterns);
        List<Transform> myActiveObstacles = new List<Transform>();

        float routineTimer = 0;
        float intervalTimer = spawnInterval;

        //Slow speed
        while(routineTimer < 30)
        {
            if (intervalTimer >= spawnInterval && thisSequence.Count != 0)
            {
                GameObject toSpawn = thisSequence[0];
                thisSequence.RemoveAt(0);

                myActiveObstacles.Add(Instantiate(toSpawn, transform.position, transform.rotation, transform).transform);
                intervalTimer = 0;
            }

            yield return null;
            routineTimer += Time.deltaTime;
            intervalTimer += Time.deltaTime;
        }

        foreach (Transform t in myActiveObstacles)
        {
            Destroy(t.gameObject);
        }

        //Medium speed
        GameMaster.speedModifier = 2;
        testUI.DisplayIntermission(true);
        routineTimer = 0;

        while(routineTimer < 10)
        {
            yield return null;
            routineTimer += Time.deltaTime;
        }

        testUI.DisplayIntermission(false);
        routineTimer = 0;

        while (routineTimer < 30)
        {

            yield return null;
            routineTimer += Time.deltaTime;
        }

        foreach (Transform t in myActiveObstacles)
        {
            Destroy(t.gameObject);
        }

        //High speed
        GameMaster.speedModifier = 3;
        testUI.DisplayIntermission(true);
        routineTimer = 0;

        while (routineTimer < 10)
        {
            yield return null;
            routineTimer += Time.deltaTime;
        }

        testUI.DisplayIntermission(false);
        routineTimer = 0;

        while (routineTimer < 30)
        {

            yield return null;
            routineTimer += Time.deltaTime;
        }

        foreach (Transform t in myActiveObstacles)
        {
            Destroy(t.gameObject);
        }

        isSpawningObstacles = false;
    }
}