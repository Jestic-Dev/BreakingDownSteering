using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBall : MonoBehaviour
{
    
    void Update()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit the player");
    }
}
