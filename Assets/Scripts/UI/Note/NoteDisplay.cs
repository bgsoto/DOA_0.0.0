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
    private int objectiveStage;
    private bool noteCollected = false;

    public static Action NoteGathered;

    /* Not used */
    private ItemData itemData;
    private bool pickable;
    private string actionText;

    private void Start()
    {
        noteText.text = noteToDisplay.noteText;
        noteTitle.text = noteToDisplay.noteTitle;
    }
    public void Interact()
    {
        if (!noteCollected)
        {
            noteCollected = true;
            objectiveStage = noteToDisplay.objectiveStage;
            if (noteToDisplay.isObjective2) { ObjectiveUpdater.UpdateObjectives?.Invoke(true, objectiveStage); }
            else { ObjectiveUpdater.UpdateObjectives?.Invoke(false, objectiveStage); }//sets either objective 1 or 2 depending on what note effects
            NoteGathered?.Invoke();
            Destroy(this); //removes interactable.
        }
    }

    public void Use() { return; }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}