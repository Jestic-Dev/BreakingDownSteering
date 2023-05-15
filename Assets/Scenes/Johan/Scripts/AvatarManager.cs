using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I got hit");
    }
}
