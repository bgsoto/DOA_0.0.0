using TMPro;
using UnityEngine;

public class NoteDisplayConditional : MonoBehaviour, IInteractable
{
    //use this script in the event of an objective that is non-linear and can either lead to one objective completion or another depending on when it is found (eg. a note that either updates to 25 or 30).
    [SerializeField] private Note noteToDisplay;
    [SerializeField] private TMP_Text noteText;
    [SerializeField] private TMP_Text noteTitle;
    [SerializeField] private int objectiveToCheck; //objective stage to check against
    [SerializeField] private int altObjectiveToSet; //objective stage to set if current objective is less than check
    [SerializeField] private bool objective2; //if is objective 2, true
    [SerializeField] private string actionText;
    private int currentObjectiveStage;
    private int objectiveStage;
    private bool noteCollected = false;
    private ObjectiveManager objectiveManager;

    /* Not used */
    private ItemData itemData;
    private bool pickable;

    private void Start()
    {
        noteText.text = noteToDisplay.noteText;
        noteTitle.text = noteToDisplay.noteTitle;
        objectiveManager = GameObject.FindGameObjectWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
    }

    public void Interact()
    {
        currentObjectiveStage = objective2 ? objectiveManager.questState2 : objectiveManager.questState;
        if (!noteCollected)
        {
            if (currentObjectiveStage >= objectiveToCheck) { objectiveStage = altObjectiveToSet; }
            else { objectiveStage = noteToDisplay.objectiveStage; }
            noteCollected = true;
            if (noteToDisplay.isObjective2) { objectiveManager.UpdateObjective(true, objectiveStage); }
            else { objectiveManager.UpdateObjective(false, objectiveStage); }//sets either objective 1 or 2 depending on what note effects
            NoteDisplay.NoteGathered?.Invoke();
            Destroy(this); //removes interactable.
        }
    }

    public void Use() { return; }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}