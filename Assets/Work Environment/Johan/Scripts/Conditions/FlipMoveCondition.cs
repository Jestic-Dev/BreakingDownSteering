using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipMoveCondition : TestCondition
{
    public float moveSpeed;
    public float modeModifier;
    public float maxAngleInput;

    public override void UpdateCondition(float handRotation, float movementBounds, float deadzoneAngle)
    {
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

        if (handRotation > maxAngleInput)
        {
            handRotation = maxAngleInput;
        }
        else if (handRotation < -maxAngleInput)
        {
            handRotation = -maxAngleInput;
        }

        objectToMove.position += new Vector3(handRotation * (moveSpeed + modeModifier * (GameMaster.speedModifier - 1)) * Time.deltaTime, 0, 0);

        CheckObjectBounds(movementBounds);
    }
}
