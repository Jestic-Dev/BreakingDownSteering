using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MappingCondition : TestCondition
{
    private float mapAngle { get { return 15 + 15 * GameMaster.speedModifier; } }

    public override void UpdateCondition(float handRotation, float movementBounds, float deadzoneAngle)
    {
        float transformedX = handRotation / mapAngle * movementBounds;

        objectToMove.position = (new Vector3(transformedX, objectToMove.position.y, objectToMove.position.z));

        CheckObjectBounds(movementBounds);
    }
}
