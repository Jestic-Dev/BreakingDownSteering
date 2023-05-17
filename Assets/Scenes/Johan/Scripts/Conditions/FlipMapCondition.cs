using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipMapCondition : TestCondition
{
    public float baseMapAngle;
    public float mapAngleScaler;
    private List<float> rememberX = new List<float>();
    public float moveThreshold;

    private float mapAngle { get { return baseMapAngle + mapAngleScaler * (GameMaster.speedModifier - 1); } }

    public override void UpdateCondition(float handRotation, float movementBounds, float deadzoneAngle)
    {
        float transformedX = handRotation / mapAngle * movementBounds;
        rememberX.Add(transformedX);

        if (Mathf.Abs(transformedX - rememberX[0]) > moveThreshold)
        {
            objectToMove.position = (new Vector3(-transformedX, objectToMove.position.y, objectToMove.position.z));
        }

        if (rememberX.Count > 7)
        {
            rememberX.RemoveAt(0);
        }

        CheckObjectBounds(movementBounds);
    }
}
