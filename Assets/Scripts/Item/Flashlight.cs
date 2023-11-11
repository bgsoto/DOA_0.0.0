using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class Flashlight : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] public ItemData flashLightData;
    [SerializeField] public GameObject point, spot;
    [SerializeField] public bool on;
    [SerializeField] public bool isEquipped;
    [SerializeField] public Material lens;
    [SerializeField] private string actionText;
    [SerializeField] private bool pickable = true;

    private void Start()
    {
     
        
        FlashLightPosition();
        on = false;
    }

    public void Update()

    {
        checkifEquipped();

        if (isEquipped)
        {


            if (Input.GetKeyDown(KeyCode.Mouse0))

            {

                if (on == false)

                {

                    on = true;
                    TurnOn();


                }

                else
                {

                    on = false;
                    TurnOff();
                }

            }

            if (transform.parent == true)
            {
                FlashLightPosition();
            }
        }




    }


    public void TurnOn()
    {
        point.SetActive(true);
        spot.SetActive(true);
        lens.EnableKeyword("_EMISSION");
    }

    public void TurnOff()
    {
        point.SetActive(false);
        spot.SetActive(false);
        lens.DisableKeyword("_EMISSION");
    }

    void FlashLightPosition()

    {
            transform.localPosition = new Vector3(0.08f, -0.1f, 0.17f);
            transform.localRotation = Quaternion.Euler(-95f, -86.91f, 0f);
        
    }

    public void checkifEquipped()
    {
        if (this.transform.parent != null ) 
        {
            
            isEquipped = true;
        }
        else
        {
            isEquipped = false;
        }
    }

    public void Interact()
    {
        if (pickable) { Destroy(gameObject); }
    }

    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }

    public ItemData itemData { get { return flashLightData; } set { flashLightData = value; } }

}
