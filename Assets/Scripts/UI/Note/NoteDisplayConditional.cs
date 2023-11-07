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
    private int currentObjectiveStage;
    private int objectiveStage;
    private bool noteCollected = false;

    private void Start()
    {
        noteText.text = noteToDisplay.noteText;
        noteTitle.text = noteToDisplay.noteTitle;
    }
    private void OnEnable()
    {
        ObjectiveUpdater.UpdateObjectives += ObjectiveUpdated;
    }
    private void OnDisable()
    {
        ObjectiveUpdater.UpdateObjectives -= ObjectiveUpdated;
    }

    public void Interact()
    {
        if (!noteCollected)
        {
            if (currentObjectiveStage >= objectiveToCheck) { objectiveStage = altObjectiveToSet; }
            else { objectiveStage = noteToDisplay.objectiveStage; }
            noteCollected = true;
            if (noteToDisplay.isObjective2) { ObjectiveUpdater.UpdateObjectives?.Invoke(true, objectiveStage); }
            else { ObjectiveUpdater.UpdateObjectives?.Invoke(false, objectiveStage); }//sets either objective 1 or 2 depending on what note effects
            NoteDisplay.NoteGathered?.Invoke();
            Destroy(this); //removes interactable.
        }
    }

    public void ObjectiveUpdated(bool Objective2, int questStage)
    {
        if (objective2 == Objective2)
        {
            currentObjectiveStage = questStage;
        }
    }
}