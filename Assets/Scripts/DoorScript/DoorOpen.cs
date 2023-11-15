using UnityEngine;
using DG.Tweening;

public class DoorOpen : MonoBehaviour
{
    private Vector3 defaultPosL;
    private Vector3 defaultPosR;
    private Vector3 movePosL = new Vector3(-1.11000001f, 0, -0.246000007f);
    private Vector3 movePosR = new Vector3(-6.13100004f, 0, -0.246000007f);
    private AudioSource doorSource;
    private bool isEmpty;
    [SerializeField] private float duration = 1f;
    [SerializeField] private GameObject wingL;
    [SerializeField] private GameObject wingR;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    void Start()
    {
        defaultPosL = wingL.transform.localPosition;
        defaultPosR = wingR.transform.localPosition;
        doorSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {//tries to set isempty to true
        isEmpty = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) //if other gameobject is on the layer "Player" (anomaly and player are on this)
        {
            wingL.transform.DOLocalMove(movePosL, duration);
            wingR.transform.DOLocalMove(movePosR, duration);
            doorSource.PlayOneShot(openClip);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 7) //if collider in trigger, trigger not empty
        {
            isEmpty = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7 && isEmpty) //checks if no colliders in trigger, closes if true
        {
            wingL.transform.DOLocalMove(defaultPosL, duration);
            wingR.transform.DOLocalMove(defaultPosR, duration);
            doorSource.PlayOneShot(closeClip);
        }
    }
}
