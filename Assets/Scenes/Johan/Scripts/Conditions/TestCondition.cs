using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestCondition : MonoBehaviour
{
    private GameLogging gameLogger;
    public Transform objectToMove;

    private void Start()
    {
        if(gameLogger == null)
        {
            gameLogger = FindAnyObjectByType<GameLogging>();
        }
    }

    public abstract void UpdateCondition(float handRotation, float movementBounds, float deadzoneAngle);

    public void CheckObjectBounds(float movementBounds)
    {
        if (objectToMove.position.x > movementBounds)
        {
            objectToMove.position = new Vector3(movementBounds, objectToMove.position.y, objectToMove.position.z);
        }

        if (objectToMove.position.x < -movementBounds)
        {
            objectToMove.position = new Vector3(-movementBounds, objectToMove.position.y, objectToMove.position.z);
        }

        if(gameLogger != null)
        {
            gameLogger.SetAvatarPosition(objectToMove.position.x);
        }
    }
}
