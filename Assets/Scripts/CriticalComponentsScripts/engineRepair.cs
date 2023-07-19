using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class engineRepair : MonoBehaviour
{
    [SerializeField] public float repairProgress;
    [SerializeField] public bool repairing;
    [SerializeField] public bool inRepairDistance;
    [SerializeField] public GameObject repairProgressBar;
    [SerializeField] public RectTransform repairBar;
    [SerializeField] public float repairTime;
    [SerializeField] public float repairSpeed;
    [SerializeField] public bool engineRepaired;
    
   

    private void Start()
    {
        repairProgress = 0f;
        repairing = false;
        repairProgressBar.SetActive(false);
        repairBar = repairBar.GetComponent<RectTransform>();
        inRepairDistance = false;

    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.E) && inRepairDistance == true)
        {
            
            repairProgressBar.SetActive(true);
            repairTime = repairSpeed / Time.timeScale;
            repairProgress += Time.deltaTime * repairTime;
            Meter();
            repairing = true;
            OnEngineRepairCompletion();
        }



        else
        {
            repairProgressBar.SetActive(false);

        }
        

    }
     
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRepairDistance = true;
           // Debug.Log("in position");
        }
        
       
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRepairDistance = false;
            //Debug.Log("out position");
        }
    }

    private void Meter()
    {

        float clampedPosition = Mathf.Clamp(repairProgress, 0f, 3.85f);
        repairBar.localScale = new Vector3(clampedPosition, .5f, 1f);
        repairProgress = clampedPosition;

    }

    private void OnEngineRepairCompletion()
    {
        if (repairProgress >= 3.85f)
        {
            Debug.Log("machine fixed");
            engineRepaired = true;
            repairProgressBar.SetActive(false);
        }
    }


}
