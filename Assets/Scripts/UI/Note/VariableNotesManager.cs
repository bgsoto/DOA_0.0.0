using UnityEngine;

public class VariableNotesManager : MonoBehaviour
{
    [SerializeField] private NoteDisplay codeNote;
    [SerializeField] private NoteDisplayConditional birthdayNote;
    [SerializeField] private NoteDisplay keycardNote;
    private string cardVar;
    private bool firstCardFire = false;

    private void OnEnable()
    {
        ObjectiveManager.onGeneratedCode += UpdateCodeNote;
        PlanetsManager.birthdayCrew += UpdateBirthdayNote;
        KeycardSpawnManager.cardSpawned += UpdateKeycardNote;
    }
    private void OnDisable()
    {
        ObjectiveManager.onGeneratedCode -= UpdateCodeNote;
        PlanetsManager.birthdayCrew -= UpdateBirthdayNote;
        KeycardSpawnManager.cardSpawned -= UpdateKeycardNote;
    }
    void UpdateCodeNote(string variable)
    {
        if (codeNote != null)
        {
            codeNote.UpdateNoteText(variable);
        }
    }
    void UpdateBirthdayNote(string variable)
    {
        if (birthdayNote != null)
        {
            birthdayNote.UpdateNoteText(variable);
        }
    }
    void UpdateKeycardNote(string location)
    {
        if (keycardNote != null)
        {
            cardVar = location;
            keycardNote.UpdateNoteText(cardVar);
            if (firstCardFire == false) { keycardNote.UpdateNoteText(", "); firstCardFire = true; }
        }
    }
}