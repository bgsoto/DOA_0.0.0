using Unity.VisualScripting;
using UnityEngine;

public class hidingSpotLocker : MonoBehaviour
{
    [Header("Relationships")]
    [SerializeField] public GameObject player;


    [Header("Settings")]
    [SerializeField] public bool doorisOpen;
    [SerializeField] public Animator animator;
    

    private void Start()
    {
      doorisOpen = false;
    }

    private void Update()
    {

        Closelocker();


    }

    private void Closelocker()
    {
      
       if (!doorisOpen && Input.GetKeyDown(KeyCode.E))
        {

            animator.SetBool("Open", true);
           
            if (!doorisOpen)
            {
                doorisOpen = true;
            }
           
        }

        else if (doorisOpen && Input.GetKeyDown(KeyCode.E))
        {

            animator.SetBool("Open", false);

            if (doorisOpen)
            {
                doorisOpen = false;
            }
        }


    }







}
