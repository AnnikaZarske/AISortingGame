using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberPopup : MonoBehaviour
{
    public static NumberPopup Create(Vector3 pos, int numberAmount, GameObject prefab)
    { 
        GameObject nrPopupTransform = Instantiate(prefab, pos, Quaternion.identity);

        NumberPopup numberPopup = nrPopupTransform.GetComponent<NumberPopup>();
        numberPopup.SetUp(numberAmount);

        return numberPopup;
    }

    private TextMeshPro textMesh;
    private float moveYSpeed = 1f;
    private float disappearTimer;
    private float disappearSpeed = 3f;
    private Color textColor;
    
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void SetUp(int numberAmount)
    {
        textMesh.SetText(numberAmount.ToString());
        textColor = textMesh.color;
        disappearTimer = 1f;
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
