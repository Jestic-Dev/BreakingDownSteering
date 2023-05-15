using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestCondition : MonoBehaviour
{
    public Transform objectToMove;

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
    }
}
