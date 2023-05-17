using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogging : MonoBehaviour
{
    [SerializeField]
    private LoggingManager loggingManager;
    private bool isLogging = false;

    

    // Start is called before the first frame update
    void Start()
    {
        if(loggingManager == null)
        {
            loggingManager = GetComponent<LoggingManager>();
        }
    }

    private class LogData
    {
        public string steeringType;
        public float speedModifier;
        public float avatarPosition;
        public float handRotation;
        public bool isIntermission = false;
        public bool hitDetected = false;
        public int patternID = 0;
    }
    private LogData logData = new LogData();

    // Update is called once per frame
    void LateUpdate()
    {
        if(loggingManager == null)
        {
            Debug.LogError("Cannot find logging manager");
            return;
        }

        if (isLogging)
        {
            RunDataLogging();
        }
    }

    private void RunDataLogging()
    {
        Dictionary<string, object> loggingData = new Dictionary<string, object>() {
            {"SteeringType", logData.steeringType},
            {"SpeedModifier", logData.speedModifier},
            {"AvatarPosition", logData.avatarPosition},
            {"HandRotation", logData.handRotation},
            {"HitDetected", logData.hitDetected},
            {"PatternID", logData.patternID}
        };

        loggingManager.Log("TestTracking", loggingData);

        logData.hitDetected = false;
        logData.patternID = 0;
    }

    public void StartLogging()
    {
        loggingManager.CreateLog("TestTracking", 
            headers: new List<string>() {
                "SteeringType",
                "SpeedModifier",
                "AvatarPosition",
                "HandRotation",
                "HitDetected", 
                "PatternID"
            });

        isLogging = true;
    }

    public void FinishLogging()
    {
        isLogging = false;
        loggingManager.SaveLog("TestTracking", clear: true, TargetType.CSV);
    }

    public void SetSteeringType(string steeringType)
    {
        logData.steeringType = steeringType;
    }

    public void SetSpeedModifierLevel(float speedModifier)
    {
        logData.speedModifier = speedModifier;
    }

    public void SetHandRotation(float handRotation)
    {
        logData.handRotation = handRotation;
    }

    public void SetIntermission(bool isIntermission)
    {
        logData.isIntermission = isIntermission;
    }

    public void SetAvatarPosition(float avatarPosition)
    {
        logData.avatarPosition = avatarPosition;
    }

    public void RegisterHit(int patternID)
    {
        logData.hitDetected = true;
        logData.patternID = patternID;
    }
}
