using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private GameObject objectiveCanvas;

    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private string currentObjective;
    [SerializeField] private TMP_Text clueText;
    [SerializeField] private string activeClue;

    [SerializeField] private int questState = 0;
    [SerializeField] private int clueState = 0;

    [SerializeField] private ObjectiveInfo objectiveData;

    private void Start()
    {
        //set objective and clue text to defaults
        UpdateObjective(0);
        UpdateClue(0);
    }
    private void OnEnable()
    {
        ObjectiveUpdater.UpdateObjectives += UpdateObjective;
        ObjectiveUpdater.UpdateClues += UpdateClue;
    }
    private void OnDisable()
    {
        ObjectiveUpdater.UpdateObjectives -= UpdateObjective;
        ObjectiveUpdater.UpdateClues -= UpdateClue;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            objectiveCanvas.GetComponent<CanvasGroup>().DOFade(1, 1f);
            UpdateText();
        }
        if(Input.GetKeyUp(KeyCode.Tab)) { 
            objectiveCanvas.GetComponent<CanvasGroup>().DOFade(0, 1f); }
    }
    private void UpdateObjective(int questStage)
    {
        if (questStage >= questState)
        {
            //sets objectives according to quest state
            currentObjective = objectiveData.objectiveDesc[questStage];
             questState = questStage;
        }
    }
    private void UpdateClue(int clueIndex)
    {
        if (clueIndex >= clueState)
        {
            //sets clues according to unlocks
            activeClue = objectiveData.clueDesc[clueIndex];
            clueState = clueIndex;
        }
    }
    private void UpdateText()
    {
        //updates display
        objectiveText.text = currentObjective;
        clueText.text = activeClue;
    }
}
