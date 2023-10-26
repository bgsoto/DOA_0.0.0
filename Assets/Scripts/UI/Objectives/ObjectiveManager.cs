using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    [Header ("Relationships")]
    [SerializeField] private GameObject objectiveCanvas;
    [SerializeField] private GameObject hamburgerIcon;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private string currentObjective;
    [SerializeField] private TMP_Text clueText;
    [SerializeField] private string activeClue;
    [SerializeField] private Text noteNotif;
    [SerializeField] private GameObject objectiveNotif;

    [SerializeField] private int questState = 0;
    private bool isFaded = false;
    

    [SerializeField] private List<ObjectiveInfo> objectiveData;

    private void Start()
    {
        //set objective and clue text to defaults
        UpdateObjective(0);
    }
    private void OnEnable()
    {
        ObjectiveUpdater.UpdateObjectives += UpdateObjective;
        NoteDisplay.NoteGathered += NoteGatheredNotif;
    }
    private void OnDisable()
    {
        ObjectiveUpdater.UpdateObjectives -= UpdateObjective;
        NoteDisplay.NoteGathered -= NoteGatheredNotif;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && !isFaded )
        {
            objectiveCanvas.GetComponent<CanvasGroup>().DOFade(1, 1f);
            hamburgerIcon.GetComponent<CanvasGroup>().DOFade(0, 0);
            objectiveNotif.SetActive(false);
            isFaded = true;
            UpdateText();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            objectiveCanvas.GetComponent<CanvasGroup>().DOFade(0, 1f);
            hamburgerIcon.GetComponent<CanvasGroup>().DOFade(1, 1f);
            isFaded = false;
        }
    }
    private void UpdateObjective(int questStage)
    {
        if (questStage > questState)
        {
            objectiveNotif.SetActive(true);
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

    private void NoteGatheredNotif()
    {
        noteNotif.DOText("Note Gathered", 1f, false, ScrambleMode.Custom, "10").OnComplete(() =>
        {
            noteNotif.DOText("         ", 1f, false, ScrambleMode.Custom, "10").SetDelay(0.5f);
        }); 
    }
    }