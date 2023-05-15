using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePattern : MonoBehaviour
{
    public int patternID;

    public GameLogging gameLogger;

    // Start is called before the first frame update
    void Start()
    {
        if (gameLogger == null)
        {
            gameLogger = FindObjectOfType<GameLogging>();
        }
    }

    public void RegisterHit()
    {
        gameLogger.RegisterHit(patternID);
    }
}
