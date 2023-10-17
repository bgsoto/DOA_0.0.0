using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveCanvasEnable : MonoBehaviour
{
    [SerializeField] LoadLevel levelloader;
    private void OnEnable()
    {
        levelloader.Interact(); //checks whenever menu is pulled up
    }
}
