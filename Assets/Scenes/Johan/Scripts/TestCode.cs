using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCode : MonoBehaviour
{
    public InputActionProperty rightHandRotation;

    

    // Update is called once per frame
    void Update()
    {
        Quaternion curRota = rightHandRotation.action.ReadValue<Quaternion>();

        float Xrot = curRota.eulerAngles.x;
        float Yrot = curRota.eulerAngles.y;
        float Zrot = curRota.eulerAngles.z;

        if(Zrot > 180)
        {
            Zrot -= 360;
        }

        Debug.Log("" + Xrot + " : " + Yrot + " : " + Zrot);
        Debug.Log(curRota.eulerAngles);
    }
}
