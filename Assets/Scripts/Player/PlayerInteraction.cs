
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour
{
    public UnityEngine.UI.Image reticle;
    public float Rlength;
    public GameObject player;
    public bool onTarget = false;
    public GameObject Inventory;
    public Transform itemdropPoint;
    GameObject Currentlyhelditem;
    public GameObject[] itemslots;
    public Transform pickupPoint;
    public GameObject parentLoc;
    public GameObject childLoc;
    public GameObject equippedItem;
    public MonoBehaviour[] scriptArray;
    RaycastHit hit;

    private void Start()
    {
        itemslots = new GameObject[4];
        scriptArray = new MonoBehaviour[4];
    }

    void Update()
    {
        DropKey();

        // Create a ray that follows the mouse cursor's position on the screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit interactionHit, Rlength))
        {
            if (interactionHit.collider.gameObject.CompareTag("Item"))
            {
                reticle.color = Color.green;
            }
            else
            {
                reticle.color = Color.white;
            }
        }
        else
        {
            reticle.color = Color.white;
        }

        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Rlength))
        {
            string tagName = hit.transform.gameObject.tag;
            Debug.Log(tagName);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            onTarget = true;

            // player picks up item by pressing E while looking at object with item tag
            if (hit.transform.gameObject.CompareTag("Item") && Input.GetKeyDown(KeyCode.E))
            {
                GameObject selectedItem = hit.transform.gameObject;
                Currentlyhelditem = selectedItem;
                Placeinslot();
                itemSelector();
            }
        }
        else
        {
            Debug.Log("nothing");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            onTarget = false;
        }
        itemSelector();
    }

    void Placeinslot()
    {
        for (int i = 0; i < itemslots.Length; i++)
        {
            if (itemslots[i] == null)
            {
                itemslots[i] = Currentlyhelditem;
                itemslots[i].SetActive(false);
                ShowPickedUpItem();
                break;
            }
        }
    }

    void itemSelector()
    {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("pressed 1");
            if (itemslots[0] != null)
            {
                itemDeselector();
                itemslots[0].gameObject.SetActive(true);
                itemslots[0].gameObject.transform.SetParent(parentLoc.transform, true);
                itemslots[0].gameObject.GetComponent<Rigidbody>().isKinematic = true;
                itemslots[0].gameObject.GetComponent<BoxCollider>().enabled = false;
              
                
            }


            else if (itemslots[0] = null)
            {
                return;
            }


        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (itemslots[1] != null)
            {
                itemDeselector();
                itemslots[1].gameObject.SetActive(true);
                itemslots[1].gameObject.transform.SetParent(parentLoc.transform, true);
                itemslots[1].gameObject.GetComponent<Rigidbody>().isKinematic = true;
                itemslots[1].gameObject.GetComponent<BoxCollider>().enabled = false;
               
               
                Debug.Log(" pressed 2 ");

            }
            else if (itemslots[1] = null)
            {
                return;
            }



        }
        
        
        // 
          
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Debug.Log("pressed 3");
            if (itemslots[2] != null)
            {
                itemDeselector();
                itemslots[2].gameObject.SetActive(true);
                itemslots[2].gameObject.transform.SetParent(parentLoc.transform, true);
                itemslots[2].gameObject.GetComponent<Rigidbody>().isKinematic = true;
                itemslots[2].gameObject.GetComponent<BoxCollider>().enabled = false;
              

            }


            else if (itemslots[2] = null)
            {
                return;
            }


        }


       
        if (Input.GetKey(KeyCode.Alpha4))
        {
            Debug.Log("pressed 4");
            if (itemslots[3] != null)
            {
                itemDeselector();
                itemslots[3].gameObject.SetActive(true);
                itemslots[3].gameObject.transform.SetParent(parentLoc.transform, true);
                itemslots[3].gameObject.GetComponent<Rigidbody>().isKinematic = true;
                itemslots[3].gameObject.GetComponent<BoxCollider>().enabled = false;
               

            }


            else if (itemslots[3] = null)
            {
                return;
            }


        }



    }

    void itemDeselector()
    {
        //go through each itemslot 
        for (int i = 0; i < itemslots.Length; i++)
        {
            // if item slot is filled 
            if (itemslots[i] is not null || itemslots[i] == true)
            {

                //set it to false
                itemslots[i].SetActive(false);


            }

        }





    }

    // on R button press, go through itemslots to find the active item and drop it at the players feet
    void DropKey()
    {
        // Detect player R input 
        if (Input.GetKeyDown(KeyCode.R))
        {

            // loop to go through itemslots length
            for (int d = 0; d < itemslots.Length; d++)
            {
                // if itemslot is is not empty and is active within the hierarchy remove that item.
                if (itemslots[d] != null && itemslots[d].activeInHierarchy)
                {
                    itemslots[d].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    itemslots[d].gameObject.GetComponent<BoxCollider>().enabled = true;
                    itemslots[d].gameObject.transform.position = itemdropPoint.position;
                    itemslots[d].gameObject.transform.parent = null;
                    
                    itemslots[d] = null;
                    break;


                }
               

            }
        }

    }

    // method to have item picked up to place in your hand 
    void ShowPickedUpItem()
    {

        itemDeselector();
        Currentlyhelditem.gameObject.SetActive(true);
        Currentlyhelditem.gameObject.transform.SetParent(parentLoc.transform, true);
        Currentlyhelditem.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Currentlyhelditem.gameObject.GetComponent<BoxCollider>().enabled = false;
        



    }
   



}
