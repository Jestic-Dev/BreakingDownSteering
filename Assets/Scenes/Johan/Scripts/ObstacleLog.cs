using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLog : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    float mySpeed = 0;
    public Transform pivotPoint;

    private void Start()
    {
        mySpeed = moveSpeed * GameMaster.speedModifier;
             
    }

    void Update()
    {
        transform.position -= new Vector3(0, 0, mySpeed * Time.deltaTime);
        transform.RotateAround(pivotPoint.position, transform.right, -1 * mySpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit the player");

        ObstaclePattern thisPattern = GetComponentInParent<ObstaclePattern>();

        if (thisPattern != null)
        { 
            thisPattern.RegisterHit();
        }
    }
}
