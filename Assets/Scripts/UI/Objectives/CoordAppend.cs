using System;
using System.Collections;
using UnityEngine;

public class CoordAppend : MonoBehaviour
{
    [SerializeField] private string textToAppend1; //the text to add to the end of the tmp_text
    [SerializeField] private string textToAppend2;
    [SerializeField] private int objectiveToUpdateOn; //the objective stage you are listening for
    [SerializeField] private int objectiveToUpdateOn2;

    public static Action<int, string> AppendObjective;

    private void OnEnable()
    {
        NoteDisplay.AppendToNote += Append;

    }
    private void OnDisable()
    {
        NoteDisplay.AppendToNote -= Append;
    } //subscribes and unsubscribes on enable/disable

    private void Append(bool objective2, int appendStage)
    {
        if (appendStage != objectiveToUpdateOn && appendStage != objectiveToUpdateOn2) { return; }
        Debug.Log("appending!");
        StartCoroutine(Appending(objective2, appendStage));
    }
    IEnumerator Appending(bool objective2, int appendStage)
    {
        yield return new WaitForSeconds(0.5f);//delayed so objective can update
        var stage = objective2 ? 2 : 1;
        if (!objective2 && appendStage == objectiveToUpdateOn) { AppendObjective?.Invoke(stage, textToAppend1); Debug.Log("appending" + textToAppend1); }
        else if (objective2 && appendStage == objectiveToUpdateOn2) { AppendObjective?.Invoke(stage, textToAppend2); Debug.Log("appending" + textToAppend2); }
        yield return null;
    }
}
