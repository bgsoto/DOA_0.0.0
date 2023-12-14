using DG.Tweening;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ItemHolder : NetworkBehaviour
{
    [Header("Relationships")]
    [SerializeField] private List<GameObject> itemList = new List<GameObject>();
    [SerializeField] private Transform itemDropTransform;
    [SerializeField] private AudioSource inventoryAudioSource;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip dropSound;


    [SerializeField] private int itemIndex = 0;
    private bool inMenu = false;
    private void OnEnable()
    {
        SettingsOpener.PausedGame += DisableInteract;
        IntelMonitorManger.inIntelMenu += DisableInteract;
        ShowKeypad.DisableControls += DisableInteract;
    }
    private void OnDisable()
    {
        SettingsOpener.PausedGame -= DisableInteract;
        IntelMonitorManger.inIntelMenu -= DisableInteract;
        ShowKeypad.DisableControls -= DisableInteract;
    }
    private void Update()
    {
        if (!IsOwner) return;
        gameObject.transform.rotation = Camera.main.transform.rotation;
        if (inMenu) { return; }
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                itemIndex--;
                if (itemIndex < 0) { itemIndex = 0; }
                //Debug.Log(itemIndex);
                ShowItem(itemIndex);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                itemIndex++;

                if (itemIndex > itemList.Count - 1)
                {
                    itemIndex = itemList.Count - 1;
                    if (itemIndex < 0) { itemIndex = 0; }
                }

                //Debug.Log(itemIndex);
                ShowItem(itemIndex);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (itemList.Count > 0)
                {
                    itemList[itemIndex].GetComponentInChildren<IInteractable>().Use();
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                DropItem();
            }
        }
    }

    public void AddToInventory(ItemData itemData)
    {
        if (!IsOwner) return;
        GameObject newItem = Instantiate(itemData.itemPrefab, transform);
        newItem.gameObject.name = itemData.name;
        newItem.transform.localPosition = itemData.equippedLocalPosition;
        newItem.transform.localRotation = Quaternion.Euler(itemData.equippedLocalRotation);
        newItem.GetComponentInChildren<Rigidbody>().isKinematic = true;
        newItem.GetComponentInChildren<BoxCollider>().enabled = false;
        newItem.SetActive(false);
        inventoryAudioSource.PlayOneShot(pickupSound);
        itemList.Add(newItem);
        ShowItem(itemIndex);
    }

    public void RemoveFromInventory(ItemData itemData)
    {
        if (!IsOwner) return;
        if (itemList != null)
        {
            foreach (GameObject itemObject in itemList)
            {
                if (itemObject.GetComponentInChildren<IInteractable>().ItemData.itemID == itemData.itemID)
                {
                    itemObject.transform.parent = null;
                    itemList.Remove(itemObject);
                    return;
                }
            }
        }
    }

    private void ShowItem(int itemIndex)
    {
        if (!IsOwner) return;
        if (itemList.Count > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i == itemIndex)
                {
                    itemList[i].SetActive(true);
                }
                else
                {
                    if (itemList[i].GetComponentInChildren<IInteractable>().ItemData.itemID == 0)
                    {
                        itemList[i].GetComponentInChildren<Rig>().ResetControls();
                    }

                    itemList[i].SetActive(false);
                }
            }
        }
    }

    private void DropItem()
    {
        if (!IsOwner) return;
        if (itemList.Count > 0 && itemList != null)
        {
            GameObject currentItem = itemList[itemIndex];

            if (currentItem.GetComponentInChildren<IInteractable>().ItemData.itemID == 0)
            {
                currentItem.GetComponentInChildren<Rig>().ResetControls();
            }

            currentItem.transform.parent = null;
            currentItem.GetComponentInChildren<Rigidbody>().isKinematic = false;
            currentItem.GetComponentInChildren<BoxCollider>().enabled = true;
            inventoryAudioSource.PlayOneShot(dropSound);
            currentItem.GetComponentInChildren<Rigidbody>().DOJump(
                endValue: itemDropTransform.position,
                jumpPower: 0.1f,
                numJumps: 1,
                duration: 0.3f).SetEase(Ease.InOutSine);

            RemoveFromInventory(itemList[itemIndex].GetComponentInChildren<IInteractable>().ItemData);
            itemIndex++;

            if (itemList.Count <= 0)
            {
                itemIndex = 0;
            }
            else
            {
                if (itemIndex > itemList.Count) { itemIndex = itemList.Count - 1; }
            }

            ShowItem(itemIndex);
        }
    }
    private void DisableInteract(bool value)
    {
        if (!IsOwner) return;
        inMenu = value;
    }
}
