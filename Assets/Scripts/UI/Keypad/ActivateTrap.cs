using System;
using UnityEngine;

public class ActivateRig : MonoBehaviour
{
    [SerializeField] private string dimensionalCoordinates;

    public static Action OnWrongCoor;
    
    private void Awake()
    {
        DisplayManager.OnCodeEntered += EnableRig;
    }

    public void EnableRig(string coordinates)
    {
        if (coordinates == dimensionalCoordinates)
        {
            Debug.Log("CORRECT CODE");
        }
        else
        {
            OnWrongCoor?.Invoke();
        }
    }
}
