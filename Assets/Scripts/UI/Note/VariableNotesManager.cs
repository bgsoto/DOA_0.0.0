using UnityEngine;

public class VariableNotesManager : MonoBehaviour
{
    [SerializeField] private NoteDisplay codeNote;
    [SerializeField] private NoteDisplayConditional birthdayNote;
    [SerializeField] private NoteDisplay keycardNote;

    private void OnEnable()
    {
        ObjectiveManager.onGeneratedCode += UpdateCodeNote;
        PlanetsManager.birthdayCrew += UpdateBirthdayNote;
    }
    private void OnDisable()
    {
        ObjectiveManager.onGeneratedCode -= UpdateCodeNote;
        PlanetsManager.birthdayCrew -= UpdateBirthdayNote;
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
    void UpdateKeycardNote(string variable)
    {
        if (keycardNote != null)
        {

        }
    }
}
