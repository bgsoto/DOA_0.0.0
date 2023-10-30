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
    [SerializeField] private TMP_Text objectiveText2;

    [SerializeField] private string currentObjective;
    [SerializeField] private string currentObjective2;

    [SerializeField] private TMP_Text clueText;
    [SerializeField] private TMP_Text clueText2;

    [SerializeField] private string activeClue;
    [SerializeField] private string activeClue2;

    [SerializeField] private Text noteNotif;
    [SerializeField] private GameObject objectiveNotif;
    [SerializeField] private AudioSource objectiveUpdatedSound;
    [SerializeField] private AudioClip objectiveUpdatedClip;

    [SerializeField] private int questState = -1;
    [SerializeField] private int questState2 = -1;
    private bool isFaded = false;
    

    [SerializeField] private List<ObjectiveInfo> objectiveData;

    private void Start()
    {
        //set objective and clue text to defaults
        UpdateObjective(0);
        UpdateObjective2(0);
        UpdateText();
    }
    private void OnEnable()
    {
        ObjectiveUpdater.UpdateObjectives += UpdateObjective;
        ObjectiveUpdater.UpdateObjectives2 += UpdateObjective2;
        NoteDisplay.NoteGathered += NoteGatheredNotif;
    }
    private void OnDisable()
    {
        ObjectiveUpdater.UpdateObjectives -= UpdateObjective;
        ObjectiveUpdater.UpdateObjectives2 -= UpdateObjective2;
        NoteDisplay.NoteGathered -= NoteGatheredNotif;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && !isFaded )
        {
            objectiveCanvas.GetComponent<CanvasGroup>().DOFade(1, 1f);
            hamburgerIcon.GetComponent<CanvasGroup>().DOFade(0, 0);
            objectiveNotif.SetActive(false);
            UpdateText();
            isFaded = true;
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
            foreach (var obj in objectiveData) { if (questStage == obj.objectiveStage && !obj.isObjective2)
                {
                    //sets objectives according to quest state
                    currentObjective = obj.objectiveDescription;
                    questState = questStage;
                    activeClue = obj.clueDescription;
                    objectiveUpdatedSound.PlayOneShot(objectiveUpdatedClip);
                }
            }
        }
    }
    private void UpdateObjective2(int questStage)
    {
        if (questStage > questState2)
        {
            objectiveNotif.SetActive(true);
            foreach (var obj in objectiveData)
            {
                if (questStage == obj.objectiveStage && obj.isObjective2)
                {
                    //sets objectives according to quest state
                    currentObjective2 = obj.objectiveDescription;
                    questState2 = questStage;
                    activeClue2 = obj.clueDescription;
                    objectiveUpdatedSound.PlayOneShot(objectiveUpdatedClip);
                }
            }
        }
    }
    private void UpdateText()
    {
        //updates display
        objectiveText.text = currentObjective;
        objectiveText2.text = currentObjective2;
        clueText.text = activeClue;
        clueText2.text = activeClue2; 
    }

    private void NoteGatheredNotif()
    {
        noteNotif.DOText("Note Gathered", 1f, false, ScrambleMode.Custom, "10").OnComplete(() =>
        {
            noteNotif.DOText("         ", 1f, false, ScrambleMode.Custom, "10").SetDelay(0.5f);
        }); 
    }
    }