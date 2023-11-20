/* Interface for any pickable objects that can be interacted by the player. */
using UnityEngine;

public interface IInteractable
{
    public ItemData ItemData { get; set; }
    public void Interact();
    public void Use();
    public bool Pickable { get; set; }
    public string ActionText { get; set; }
   
   
}
