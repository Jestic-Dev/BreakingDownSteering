using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBall : MonoBehaviour
{
    public float moveSpeed;
    float mySpeed = 0;

    private void Start()
    {
        mySpeed = moveSpeed * GameMaster.speedModifier;
    }

    void Update()
    {
        transform.position -= new Vector3(0, 0, mySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit the player");
    }
}
