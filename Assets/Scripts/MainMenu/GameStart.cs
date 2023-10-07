using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [Header("Scene Loading")]
    [Tooltip("Enter the name of the scene to load.")]
    public string sceneToLoad; // Name of the scene to load, set in the Inspector

    public void LoadScene()
    {
        if(sceneToLoad == null || sceneToLoad == "")
        {
            Debug.Log("No Scene set on the " +gameObject.name + " gameobject.");
            return;
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
