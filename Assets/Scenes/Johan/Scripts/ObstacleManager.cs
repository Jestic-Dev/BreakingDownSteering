using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameMaster gameMaster;
    public TestUIManager testUIManager;
    public GameLogging gameLogger;

    public float spawnInterval;
    private bool isSpawningObstacles;

    private int obstacleHitCount = 0;

    public List<ObstacleSequence> obstacleSequences;

    [System.Serializable]
    public class ObstacleSequence
    {
        public List<GameObject> slowPatterns;
        public List<GameObject> mediumPatterns;
        public List<GameObject> fastPatterns;
    }

    public void StartSpawnSequence()
    {
        if (isSpawningObstacles == false)
        {
            StartCoroutine(ObstacleRoutine());
        }
    }

    public void CountHit()
    {
        obstacleHitCount++;
    }

    public IEnumerator ObstacleRoutine()
    {
        isSpawningObstacles = true;

        int sequenceID = Random.Range(0, obstacleSequences.Count);
        ObstacleSequence thisSequence = new ObstacleSequence();
        thisSequence.slowPatterns = new List<GameObject>(obstacleSequences[sequenceID].slowPatterns);
        thisSequence.mediumPatterns = new List<GameObject>(obstacleSequences[sequenceID].mediumPatterns);
        thisSequence.fastPatterns = new List<GameObject>(obstacleSequences[sequenceID].fastPatterns);

        List<Transform> myActiveObstacles = new List<Transform>();

        float routineTimer = 0;
        float modifiedSpawnInterval = spawnInterval;
        float intervalTimer = modifiedSpawnInterval;

        testUIManager.HideInstructions();
        gameLogger.StartLogging();

        //Slow speed
        while(routineTimer < 25)
        {
            if (intervalTimer >= modifiedSpawnInterval && thisSequence.slowPatterns.Count != 0)
            {
                GameObject toSpawn = thisSequence.slowPatterns[0];
                thisSequence.slowPatterns.RemoveAt(0);

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
        myActiveObstacles.Clear();

        //Medium speed
        gameMaster.SetSpeedModifier(2);
        testUIManager.DisplayIntermission(true);
        routineTimer = 0;
        modifiedSpawnInterval = spawnInterval * 0.75f;

        while(routineTimer < 10)
        {
            yield return null;
            routineTimer += Time.deltaTime;
        }

        testUIManager.DisplayIntermission(false);
        routineTimer = 0;

        while (routineTimer < 24)
        {
            if (intervalTimer >= modifiedSpawnInterval && thisSequence.mediumPatterns.Count != 0)
            {
                GameObject toSpawn = thisSequence.mediumPatterns[0];
                thisSequence.mediumPatterns.RemoveAt(0);

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
        myActiveObstacles.Clear();

        //High speed
        gameMaster.SetSpeedModifier(3);
        testUIManager.DisplayIntermission(true);
        modifiedSpawnInterval = spawnInterval * 0.5f;
        routineTimer = 0;

        while (routineTimer < 10)
        {
            yield return null;
            routineTimer += Time.deltaTime;
        }

        testUIManager.DisplayIntermission(false);
        routineTimer = 0;

        while (routineTimer < 22)
        {
            if (intervalTimer >= modifiedSpawnInterval && thisSequence.fastPatterns.Count != 0)
            {
                GameObject toSpawn = thisSequence.fastPatterns[0];
                thisSequence.fastPatterns.RemoveAt(0);

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
        myActiveObstacles.Clear();

        testUIManager.ShowTestComplete(obstacleHitCount.ToString());
        gameLogger.FinishLogging();
        isSpawningObstacles = false;
    }
}