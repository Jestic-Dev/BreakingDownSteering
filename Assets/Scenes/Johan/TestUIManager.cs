using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIManager : MonoBehaviour
{
    public GameObject intermissionUI;

    private void Start()
    {
        intermissionUI.SetActive(false);
    }

    public void DisplayIntermission(bool trueFalse)
    {
        intermissionUI.SetActive(trueFalse);
    }
}
