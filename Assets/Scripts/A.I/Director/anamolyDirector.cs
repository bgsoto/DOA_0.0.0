using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;

public class anamolyDirector : MonoBehaviour
{

    public GameObject playerPin;

    public int listIndex;

    public float pinpointTimer;
    public float elapsedTime;

    public GameObject playerObject;

    public Transform playerLocation;
    public Transform playerNode;


    public List<Transform> playerlocationpins;

    public GameObject Lure;

    public GameObject MonsterStateMachine;
    
    public bool atCheckpoint;

    public Transform[] rooms;
    public int chosenRoom;
    




    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        PlayerLocNode();

    }

    public void PlayerLocNode()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= pinpointTimer)
        {

            playerLocation = playerObject.GetComponent<player>().Getter;

            playerlocationpins.Add(playerLocation);

            Lure.transform.position = playerlocationpins[listIndex].position;

            //reset timer
            elapsedTime = 0f;

            Debug.Log(playerlocationpins[listIndex]);

        }
    }

    

    public void UpdateInitialLurePosition()
    {
        listIndex++;
    }

    public void nextRoom()
    {

    }

    public void randRoom()
    {
       // chosenRoom = Random.Range(0, rooms.Length);
    }
    
}
