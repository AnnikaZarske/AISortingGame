using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverMouse : MonoBehaviour
{
    public string workerCost;
    public TextMeshProUGUI displayText;

    private void Start()
    {
        displayText.text = "Cost: " + workerCost;
    }

    public void Hover()
    {
        displayText.gameObject.SetActive(true);
    }

    public void ExitHover()
    {
        displayText.gameObject.SetActive(false);
    }
}
