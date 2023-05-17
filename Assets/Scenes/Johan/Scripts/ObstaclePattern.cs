using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePattern : MonoBehaviour
{
    public int patternID;

    private GameLogging gameLogger;
    private ObstacleManager obstacleSpawner;

    // Start is called before the first frame update
    void Start()
    {
        if (gameLogger == null)
        {
            gameLogger = FindObjectOfType<GameLogging>();
        }
        if (obstacleSpawner == null)
        {
            obstacleSpawner = FindObjectOfType<ObstacleManager>();
        }
    }

    public void RegisterHit()
    {
        Debug.Log("Pattern hit");
        gameLogger.RegisterHit(patternID);
        obstacleSpawner.CountHit();
    }
}
