using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBarrel : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    float mySpeed = 0;

    private void Start()
    {
        mySpeed = moveSpeed * GameMaster.speedModifier;
             
    }

    void Update()
    {
        transform.position -= new Vector3(0, 0, mySpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 1 * mySpeed, 0));
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
