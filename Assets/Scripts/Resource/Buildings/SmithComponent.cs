using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithComponent : MonoBehaviour
{
    public int numTools;
    public GameObject numberPopup;
    public Vector3 numberPos;

    private void Start()
    {
        numberPos = new Vector3(0.4f, 2.2f, 0);
    }

    public void DisplayText(int numberAmount)
    {
        NumberPopup.Create(numberPos, numberAmount, numberPopup);
    }
}
