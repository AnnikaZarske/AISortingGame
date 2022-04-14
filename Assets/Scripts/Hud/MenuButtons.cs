using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public GameObject workerButtons;

    public void OnClick(GameObject buttons)
    {
        buttons.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
