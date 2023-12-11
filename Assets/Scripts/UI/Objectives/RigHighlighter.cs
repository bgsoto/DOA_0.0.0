using EPOOutline;
using UnityEngine;

public class RigHighlighter : MonoBehaviour
{
    [SerializeField] private Outlinable rigOutline;
    public ObjectiveManager manager;
    private bool outlineToggle = false;
    private void OnEnable()
    {
        KeypadDisplayManager.OnExitKeypad += OnCodeEntered;
        Rig.RigPlaced += OnRigPlaced;
    }
    private void OnDisable()
    {
        KeypadDisplayManager.OnExitKeypad -= OnCodeEntered;
        Rig.RigPlaced -= OnRigPlaced;
    }
    private void Start()
    {
        manager = FindObjectOfType<ObjectiveManager>();
        rigOutline.enabled = false;
    }

    private void Update()
    {
        if (manager.questState < 40 || manager.questState2 < 40)
        {
            return;
        }
        else if(outlineToggle == false)
        {
            rigOutline.enabled = true;
        }
    }

    void OnRigPlaced(bool value)
    {
        outlineToggle = value;
        rigOutline.enabled = false;
    }

    void OnCodeEntered(bool value)
    {
        if (value == true)
        {
            manager.UpdateObjective(false, 50);
            manager.UpdateObjective(true, 50);
        }
    }
}