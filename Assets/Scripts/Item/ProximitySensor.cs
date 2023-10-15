using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProximitySensor : MonoBehaviour
{
    [Header("Relationships")]
    [SerializeField] private Transform monsterTransform;

    [Header("Tracker Settings")]
    [SerializeField] private float maxDistance;
    [SerializeField] private List<Image> barImageList = new List<Image>();
    [SerializeField] private TMP_Text xPosText;
    [SerializeField] private TMP_Text yPosText;
    [SerializeField] private TMP_Text zPosText;

    private int barCount;

    private void Update()
    {
        GetDistance();
        ShowPosition();

        for (int i = 0; i < barImageList.Count; i++) 
        {
            if (i <= barCount)
            {
                barImageList[i].enabled = true;
            }
            else
            {
                barImageList[i].enabled = false;
            }
        }
    }

    private void ShowPosition()
    {
        xPosText.text = $"X: {(int)Math.Floor(transform.position.x)}";
        yPosText.text = $"Y: {(int)Math.Floor(transform.position.y)}";
        zPosText.text = $"Z: {(int)Math.Floor(transform.position.z)}";
    }

    private void GetDistance()
    {
        float distance = Vector3.Distance(transform.position, monsterTransform.position);
        
        if (distance > (maxDistance * 1f)) { barCount = 0; }
        else if (distance < (maxDistance * 1f)     && distance >= (maxDistance * 0.875f)) { barCount = 1; }
        else if (distance < (maxDistance * 0.875f) && distance >= (maxDistance * 0.75f))  { barCount = 2; }
        else if (distance < (maxDistance * 0.675f) && distance >= (maxDistance * 0.5f))   { barCount = 3; }
        else if (distance < (maxDistance * 0.5f)   && distance >= (maxDistance * 0.375f)) { barCount = 4; }
        else if (distance < (maxDistance * 0.375f) && distance >= (maxDistance * 0.25f))  { barCount = 5; }
        else if (distance < (maxDistance * 0.25f)  && distance >= (maxDistance * 0.125f)) { barCount = 6; }
        else if (distance < (maxDistance * 0.125f) && distance >= (maxDistance * 0f))     { barCount = 7; }
    }
}
