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
        public int sequenceID;
        public float speedModifier;
        public float avatarPosition;
        public float handRotation;
        public float totalHandRotation;
        public float handPositionChange;
        public float totalHandPositionChange;
        public bool isIntermission = false;
        public bool hitDetected = false;
        public int hitPatternID = 0;
        public int hitTotal = 0;
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
            {"SequenceID", logData.sequenceID},
            {"SpeedModifier", logData.speedModifier.ToString()},
            {"AvatarPosition", logData.avatarPosition.ToString()},
            {"HandRotation", logData.handRotation.ToString()},
            {"TotalHandRotation", logData.totalHandRotation.ToString()},
            {"HandPositionChange", logData.handPositionChange.ToString()},
            {"TotalHandPositionChange", logData.totalHandPositionChange.ToString()},
            {"HitDetected", logData.hitDetected},
            {"HitPatternID", logData.hitPatternID},
            {"HitTotal", logData.hitTotal}
        };

        loggingManager.Log("TestTracking", loggingData);

        logData.hitDetected = false;
        logData.hitPatternID = 0;
    }

    public void StartLogging()
    {
        loggingManager.CreateLog("TestTracking", 
            headers: new List<string>() {
                "SteeringType",
                "SequenceID",
                "SpeedModifier",
                "AvatarPosition",
                "HandRotation",
                "TotalHandRotation",
                "HandPositionChange",
                "TotalHandPositionChange",
                "HitDetected", 
                "HitPatternID",
                "HitTotal"
            });

        logData.totalHandPositionChange = 0;
        logData.totalHandRotation = 0;
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

    public void SetSequenceID(int sequenceID)
    {
        logData.sequenceID = sequenceID;
    }

    public void SetSpeedModifierLevel(float speedModifier)
    {
        logData.speedModifier = speedModifier;
        Debug.Log("Logging speedmodifier is: " + speedModifier);
    }

    public void SetHandRotation(float handRotation)
    {
        logData.handRotation = handRotation;
        logData.totalHandRotation += handRotation;
    }

    public void SetHandPositionChange(float handPositionChange)
    {
        logData.handPositionChange = handPositionChange;
        logData.totalHandPositionChange += handPositionChange;
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
        logData.hitPatternID = patternID;
        logData.hitTotal++;
    }
}
