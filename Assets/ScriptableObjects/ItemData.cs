using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public int itemID;
    public string itemName;
    public Image itemImage;
    public GameObject itemPrefab;
    public Vector3 equippedLocalPosition;
    public Vector3 equippedLocalRotation;
}
