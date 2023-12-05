using EPOOutline;
using System;
using TMPro;
using UnityEngine;

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
    [SerializeField] private string variableText;

    public static Action NoteGathered;
    public static Action<bool, int> AppendToNote;

    private ObjectiveManager objectiveManager;

    /* Not used */
    private ItemData itemData;
    private bool pickable;

    private void Awake()
    {
        objectiveManager = GameObject.FindGameObjectWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
        noteText.text = noteToDisplay.noteText;
        noteTitle.text = noteToDisplay.noteTitle;
    }
    public void Interact()
    {
        if (!noteCollected)
        {
            if (GetComponent<Outlinable>() != null)
            {
                GetComponent<Outlinable>().enabled = false;
            }
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

    public void UpdateNoteText(string variable)
    {
        variableText += variable;
        noteText.text = noteToDisplay.noteText;
        noteText.text += variableText + noteToDisplay.noteText2;
        //Debug.Log(noteText.text);
    }

    public void Use() { return; }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}