using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    [Header("Relationships")]
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

    public static Action<string> onGeneratedCode;

    public int questState = -1;
    public int questState2 = -1;
    //private bool isFaded = false;

    private bool areAllKeysCollected = false;
    private bool isArtifactCollected = false;
    public string rigCode;

    [SerializeField] private List<ObjectiveInfo> objectiveData;

    private void Start()
    {
        rigCode = GenerateCode(rigCode);
        onGeneratedCode?.Invoke(rigCode);
        Debug.Log(rigCode);
        
        Debug.Log("Invoked onGeneratedCode event with code: " + rigCode);

        //set objective and clue text to defaults
        UpdateObjective(false, 0);
        UpdateObjective(true, 0);
        UpdateText();
    }
    private void OnEnable()
    {
        PlayerInteraction.onAllKeysCollected += UpdateObjective;
        PlayerInteraction.onArtifactCollected += UpdateObjective;
        PlayerInteraction.onAllKeysCollected += keysCollected;
        PlayerInteraction.onArtifactCollected += artifactCollected;
        ObjectiveUpdaterUI.UpdateObjectivesUI += UpdateObjective;
        CoordAppend.AppendObjective += AppendText;
        NoteDisplay.NoteGathered += NoteGatheredNotif;
    }
    private void OnDisable()
    {
        PlayerInteraction.onAllKeysCollected -= UpdateObjective;
        PlayerInteraction.onArtifactCollected -= UpdateObjective;
        PlayerInteraction.onAllKeysCollected -= keysCollected;
        PlayerInteraction.onArtifactCollected -= artifactCollected;
        ObjectiveUpdaterUI.UpdateObjectivesUI -= UpdateObjective;
        CoordAppend.AppendObjective -= AppendText;
        NoteDisplay.NoteGathered -= NoteGatheredNotif;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            objectiveCanvas.GetComponent<CanvasGroup>().DOFade(1, 1f);
            hamburgerIcon.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
            objectiveNotif.SetActive(false);
            //UpdateText();
        }
       else if (Input.GetKeyUp(KeyCode.Tab))
        {
            objectiveCanvas.GetComponent<CanvasGroup>().DOFade(0, 1f);
            hamburgerIcon.GetComponent<CanvasGroup>().DOFade(1, 1f);
        }
    }

    public void UpdateObjective(bool objective2, int questStage)
    {
        if (objective2 ? questStage > questState2 : questStage > questState) //checks if objective 2 is true, if true then checks if questStage is greater than questState2. if obj2 not true, checks questStage1
        {
            objectiveNotif.SetActive(true);
            foreach (var obj in objectiveData)
            {
                if (questStage == obj.objectiveStage && obj.isObjective2 == objective2)
                {
                    //sets objectives according to quest state
                    if (objective2)
                    {
                        currentObjective2 = obj.objectiveDescription; questState2 = questStage;
                        activeClue2 = obj.clueDescription;
                        UpdateText();
                    }
                    else
                    {
                        currentObjective = obj.objectiveDescription; questState = questStage;
                        activeClue = obj.clueDescription;
                        UpdateText();
                    }

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

    private void AppendText(int objectiveToAppend, string textToAppend)
    {
        if (objectiveToAppend == 1)
        {
            currentObjective += textToAppend; //if objectiveToAppend is 1, add the string and return
            Debug.Log(currentObjective);
            UpdateText();
            return;
        }//else do that for objective2
        currentObjective2 += textToAppend;
        UpdateText();
        //Debug.Log(currentObjective2);
    }

    private void NoteGatheredNotif()
    {
        noteNotif.DOText("Note Gathered", 1f, false, ScrambleMode.Custom, "10").OnComplete(() =>
        {
            noteNotif.DOText("         ", 1f, false, ScrambleMode.Custom, "10").SetDelay(0.5f);
        });
    }

    private void keysCollected(bool objective2, int questStage) { areAllKeysCollected = true; }
    private void artifactCollected(bool objective2, int questStage) { isArtifactCollected = true; }

    private string GenerateCode(string value)
    {
        for (int i = 0; i < 5; i++)
        {
            string randomIntString = Mathf.FloorToInt(UnityEngine.Random.Range(0, 9)).ToString();
            value += randomIntString;
        }

        return value;
    }

    public bool AreAllKeysCollected { get { return areAllKeysCollected; } set { areAllKeysCollected = value; } }
    public bool IsArtifactCollected { get { return isArtifactCollected; } set { isArtifactCollected = value; } }
}