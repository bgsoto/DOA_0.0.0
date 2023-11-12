using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteDisplay : MonoBehaviour, IInteractable
{
    [SerializeField] private Note noteToDisplay;
    [SerializeField] private TMP_Text noteText;
    [SerializeField] private TMP_Text noteTitle;
    [SerializeField] private string actionText;
    [SerializeField] private bool appendToThisNote;
    private int objectiveStage;
    private bool objective2;
    private bool noteCollected = false;
   
    public static Action NoteGathered;
    public static Action<bool, int> AppendToNote;

    private ObjectiveManager objectiveManager;

    /* Not used */
    private ItemData itemData;
    private bool pickable;
    

    private void Start()
    {
        objectiveManager = GameObject.FindGameObjectWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
        noteText.text = noteToDisplay.noteText;
        noteTitle.text = noteToDisplay.noteTitle;
    }
    public void Interact()
    {
        if (!noteCollected)
        {
            noteCollected = true;
            objectiveStage = noteToDisplay.objectiveStage;
            objective2 = noteToDisplay.isObjective2;
            objectiveManager.UpdateObjective(objective2, objectiveStage);
            NoteGathered?.Invoke();
            if (appendToThisNote)
            {
                AppendToNote?.Invoke(objective2, objectiveStage);
            }
            Destroy(this); //removes interactable.
        }
    }

    public void Use() { return; }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}