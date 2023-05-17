using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestUIManager : MonoBehaviour
{
    public GameObject setupUI;
    public GameObject intermissionUI;
    public GameObject testOverUI;

    private void Start()
    {
        setupUI.SetActive(true);
        intermissionUI.SetActive(false);
        testOverUI.SetActive(false);
    }

    public void HideInstructions()
    {
        setupUI.SetActive(false);
    }

    public void DisplayIntermission(bool trueFalse)
    {
        intermissionUI.SetActive(trueFalse);
    }

    public void ShowTestComplete(string hitCount)
    {
        testOverUI.SetActive(true);
        testOverUI.GetComponentInChildren<TextMeshProUGUI>().text += hitCount;
    }
}
