using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntelCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject intelButtonPrefab;
    [SerializeField] private GameObject intelButtonHolder;
    [SerializeField] private TMP_Text intelDesc;
    [SerializeField] private TMP_Text intelTitle;
    [SerializeField] private TMP_Text intelAuthor;
    [SerializeField] private List<Intel> intelList = new(); //order this list in the order intel appears
    private bool firstIntelSet = false;

    private void OnEnable()
    {
        IntelButton.intelPressed += UpdateText;
    }
    private void OnDisable()
    {
        IntelButton.intelPressed -= UpdateText;
    }
    private void Start()
    {
        StartCoroutine(Initialize());
    }

    void UpdateText(Intel intel)
    {
        intelDesc.text = intel.intelText;
        intelTitle.text = intel.intelTitle;
        intelAuthor.text = intel.intelAuthor;
    }
    public IEnumerator Initialize()
    {
        for (int i = 0; i < intelList.Count; i++)
        {
            GameObject instanceGo = Instantiate(intelButtonPrefab, intelButtonHolder.transform);
            instanceGo.GetComponent<IntelButton>().intel = intelList[i];
            instanceGo.GetComponent<Button>().interactable = IntelCollectionManager.collectedIntel.Contains(intelList[i]) ? true : false;
            instanceGo.GetComponentInChildren<TMP_Text>().text = IntelCollectionManager.collectedIntel.Contains(intelList[i]) ? intelList[i].intelTitle : "???????";
            instanceGo.transform.SetAsLastSibling();
            if (firstIntelSet == false && IntelCollectionManager.collectedIntel.Contains(intelList[i]))
            {
                UpdateText(intelList[i]);
                firstIntelSet = true;
            }
        }
        yield return null;
    }
}
