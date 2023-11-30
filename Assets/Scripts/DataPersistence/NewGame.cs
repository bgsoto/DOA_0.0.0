using UnityEngine;

public class NewGame : MonoBehaviour, IInteractable
{
    [SerializeField] private string actionText = "New Game";
    /* Not used */
    private ItemData itemData;
    private bool pickable;
    public void Interact()
    {
        DataPersistenceManager.Instance.NewGame();
    }

    public void Use() { return; }
    public ItemData ItemData { get { return itemData; } set { itemData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
}

