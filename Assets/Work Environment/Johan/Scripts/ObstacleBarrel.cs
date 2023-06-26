using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBarrel : Obstacle
{
    public float rotationSpeed;

    void Update()
    {
        Move();
        transform.Rotate(new Vector3(0, 1 * mySpeed, 0));
    }
}
