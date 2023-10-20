using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private GameObject objectiveCanvas;
    [SerializeField] private GameObject hamburgerIcon;

    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private string currentObjective;
    [SerializeField] private TMP_Text clueText;
    [SerializeField] private string activeClue;

    [SerializeField] private int questState = 0;

    [SerializeField] private List<ObjectiveInfo> objectiveData;

    private void Start()
    {
        //set objective and clue text to defaults
        UpdateObjective(0);
    }
    private void OnEnable()
    {
        ObjectiveUpdater.UpdateObjectives += UpdateObjective;
    }
    private void OnDisable()
    {
        ObjectiveUpdater.UpdateObjectives -= UpdateObjective;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            objectiveCanvas.GetComponent<CanvasGroup>().DOFade(1, 1f);
            hamburgerIcon.GetComponent<CanvasGroup>().DOFade(0, 0);
            UpdateText();
        }
        if (Input.GetKeyUp(KeyCode.Tab)) {
            objectiveCanvas.GetComponent<CanvasGroup>().DOFade(0, 1f); }
            hamburgerIcon.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }
    private void UpdateObjective(int questStage)
    {
        if (questStage >= questState)
        {
            foreach (var obj in objectiveData) { if (questStage == obj.objectiveStage)
                {
                    //sets objectives according to quest state
                    currentObjective = obj.objectiveDescription;
                    questState = questStage;
                    activeClue = obj.clueDescription;
                }
            }
        }

    }
    private void UpdateText()
    {
        //updates display
        objectiveText.text = currentObjective;
        clueText.text = activeClue;
    }
}
