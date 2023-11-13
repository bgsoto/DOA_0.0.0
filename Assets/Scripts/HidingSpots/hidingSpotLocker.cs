using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class hidingSpotLocker : MonoBehaviour, IInteractable
{
    [Header("Relationships")]
    [SerializeField] public GameObject player;
    [Header("Settings")]
    [SerializeField] public bool doorisOpen;
    [SerializeField] public Animator animator;
    [SerializeField] private string actionText;
    /* Not used */
    private ItemData lockerData;
    private bool pickable;
    private void Start()
    {
      doorisOpen = false;
    }
    public void Interact()
    {
        if (!doorisOpen)
        {
            
            animator.SetBool("Open", true);
            if (!doorisOpen)
            {
                doorisOpen = true;
            }
        }
        else if (doorisOpen)
        {
            animator.SetBool("Open", false);
            if (doorisOpen)
            {
                doorisOpen = false;
            }
        }
    }
    public void Use() { return;}
    public ItemData ItemData { get { return lockerData; } set { lockerData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}








