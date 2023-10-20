using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteDisplay : MonoBehaviour, IInteractable
{
    [SerializeField] private Note noteToDisplay;
    [SerializeField] private TMP_Text noteText;
    [SerializeField] private TMP_Text noteTitle;
    private bool noteCollected;

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
        }
    }
}
