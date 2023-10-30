using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class NoteDisplay : MonoBehaviour, IInteractable
{
    [SerializeField] private Note noteToDisplay;
    [SerializeField] private TMP_Text noteText;
    [SerializeField] private TMP_Text noteTitle;
    private int objectiveStage;
    private bool noteCollected = false;

    public static Action NoteGathered;
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
            if (noteToDisplay.isObjective2) { ObjectiveUpdater.UpdateObjectives2?.Invoke(objectiveStage); }
            else { ObjectiveUpdater.UpdateObjectives?.Invoke(objectiveStage); }//sets either objective 1 or 2 depending on what note effects
            NoteGathered?.Invoke();
            Destroy(this); //removes interactable.
        }
    }
}