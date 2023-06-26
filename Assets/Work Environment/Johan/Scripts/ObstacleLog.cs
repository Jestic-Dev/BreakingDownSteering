using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLog : Obstacle
{
    public float rotationSpeed;
    public Transform pivotPoint;


    void Update()
    {
        Move();
        transform.RotateAround(pivotPoint.position, transform.right, -1 * mySpeed);
    }
}
