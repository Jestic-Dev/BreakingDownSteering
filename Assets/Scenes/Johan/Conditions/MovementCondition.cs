using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCondition : TestCondition
{
    public float moveSpeed;

    public override void UpdateCondition(float handRotation, float movementBounds, float deadzoneAngle)
    {
        Debug.Log("hi?");
        //If within the deadzone, cancel to 0
        if (Mathf.Abs(handRotation) < deadzoneAngle)
        {
            handRotation = 0;
        }
        else if (handRotation > 0) //above deadzone, reduce to start speed from 0
        {
            handRotation -= deadzoneAngle;
        }
        else //below deadzone, increase to start speed from 0
        {
            handRotation += deadzoneAngle;
        }

        objectToMove.Translate(new Vector3(-handRotation * moveSpeed * GameMaster.speedModifier * Time.deltaTime, 0, 0));

        CheckObjectBounds(movementBounds);
    }
}
