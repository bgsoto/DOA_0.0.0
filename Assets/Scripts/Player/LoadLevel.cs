using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadLevel : MonoBehaviour
{
    private string sceneToLoad;
    [SerializeField] private TMP_Text text;

    public void Interact()
    {
        if (sceneToLoad == null || sceneToLoad == "")
        {
            Debug.Log("No Scene set on the " + gameObject.name + " gameobject.");
            text.text = "No Destination Selected! Please Select a Destination on the Navigation Console.";
            return;//prevents loading null scene
        }
        else
        {
            text.text = "Destination is set for the S.C. Curie. Are you sure you wish to depart?";//to be replaced with correct string for each map in future
        }
    }

    public void LoadScene()
    {
        if (sceneToLoad == null || sceneToLoad == "")
        {
            Debug.Log("No Scene set on the " + gameObject.name + " gameobject.");
            return;
        }
        SceneManager.LoadScene(sceneToLoad);
    }

    public void UpdateSceneToLoad(string destinationScene)
    {
        sceneToLoad = destinationScene; 
    }
}
