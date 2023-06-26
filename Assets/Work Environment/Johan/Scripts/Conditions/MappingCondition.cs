using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MappingCondition : TestCondition
{
    public float baseMapAngle;
    public float mapAngleScaler;
    private List<float> rememberX = new List<float>();
    public float moveThreshold;
    private float rememberRotation;
    public float rotationThreshold;

    private float mapAngle { get { return baseMapAngle + mapAngleScaler * (GameMaster.speedModifier - 1); } }

    public override void UpdateCondition(float handRotation, float movementBounds, float deadzoneAngle)
    {
        float transformedX = handRotation / mapAngle * movementBounds;
        rememberX.Add(transformedX);

        if (Mathf.Abs(transformedX - rememberX[0]) > moveThreshold
            || Mathf.Abs(handRotation - rememberRotation) > rotationThreshold)
        {
            objectToMove.position = (new Vector3(-transformedX, objectToMove.position.y, objectToMove.position.z));
            rememberRotation = handRotation;
        }

        if(rememberX.Count > 7)
        {
            rememberX.RemoveAt(0);
        }

        CheckObjectBounds(movementBounds);
    }
}
