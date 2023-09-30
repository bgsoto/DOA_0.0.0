using System;
using UnityEngine;

public class ActivateRig : MonoBehaviour
{
    [SerializeField] private string dimensionalCoordinates;

    public static Action OnWrongCoor;
    
    private void Awake()
    {
        /*
        * Subscribes to event in DisplayManager script.
        * Invokes: EnableRig()
        */
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
            /*
            * Calls all functions subscribed to this event.
            * Subscription: DisplayManager.
            */
            OnWrongCoor?.Invoke();
        }
    }
}
