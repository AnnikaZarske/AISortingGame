using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void ToDisplay(GameObject buttonsToDisplay)
    {
        buttonsToDisplay.SetActive(true);
    }
    
    public void ToHide(GameObject buttonsToHide)
    {
        buttonsToHide.SetActive(false);
    }
}
