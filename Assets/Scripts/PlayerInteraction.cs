using lists;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour
{

   public float Rlength;
   public GameObject player;
   public bool onTarget = false;
    public GameObject Inventory;
    public Transform itemdropPoint;
    GameObject Currentlyhelditem;
       RaycastHit hit;

    private void Start()
    {
       
    }

    void Update()
    {
      
        //project raycast onto item your looking at
          if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Rlength)) 
        {
            string tagName = hit.transform.gameObject.tag;
            
            Debug.Log(tagName);

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            onTarget = true;
           
            // player pick up item
            if (hit.transform.gameObject.CompareTag("Item") && Input.GetKeyDown(KeyCode.E))
            {
                GameObject target = hit.transform.gameObject;
 
                target.SetActive(false);

                // GameObject Inventory = GetComponentInChildren<GameObject>();
                PickUp();
                Currentlyhelditem = target;
            }           
        }
        else
        {
            Debug.Log("nothing");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            onTarget = false;
        }
          //item drop button
        if(Input.GetKeyDown(KeyCode.R))
        {
            DropItem();
        }
    }

    void PickUp()
    {

      Inventory.SetActive(true);

    }

    void DropItem()
    {

        Inventory.SetActive(false);
        Currentlyhelditem.SetActive(true);
        Currentlyhelditem.transform.position = itemdropPoint.position;

    }

}
