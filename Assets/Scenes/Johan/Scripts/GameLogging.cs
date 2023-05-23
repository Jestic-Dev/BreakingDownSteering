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
        public int currentPatternID = 0;
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
            {"AvatarPosition", logData.avatarPosition},
            {"AvatarPositionStr", logData.avatarPosition.ToString()},
            {"HandRotation", logData.handRotation},
            {"HandRotationStr", logData.handRotation.ToString()},
            {"TotalHandRotation", logData.totalHandRotation},
            {"TotalHandRotationStr", logData.totalHandRotation.ToString()},
            {"HandPositionChange", logData.handPositionChange},
            {"HandPositionChangeStr", logData.handPositionChange.ToString()},
            {"TotalHandPositionChange", logData.totalHandPositionChange},
            {"TotalHandPositionChangeStr", logData.totalHandPositionChange.ToString()},
            {"HitDetected", logData.hitDetected},
            {"HitPatternID", logData.hitPatternID},
            {"HitTotal", logData.hitTotal},
            {"IsIntermission", logData.isIntermission},
            {"CurrentPatternID", logData.currentPatternID}
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
                "AvatarPositionStr",
                "HandRotation",
                "HandRotationStr",
                "TotalHandRotation",
                "TotalHandRotationStr",
                "HandPositionChange",
                "HandPositionChangeStr",
                "TotalHandPositionChange",
                "TotalHandPositionChangeStr",
                "HitDetected", 
                "HitPatternID",
                "HitTotal",
                "IsIntermission",
                "CurrentPatternID"
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

    public void RegisterPatternStart(int patternID)
    {
        logData.currentPatternID = patternID;
    }

    public void RegisterPatternEnd()
    {
        logData.currentPatternID = 0;
    }
}
