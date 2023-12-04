using DG.Tweening;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Vector3 defaultPosL;
    private Vector3 defaultPosR;
    private Vector3 movePosL = new Vector3(-1.11000001f, 0, -0.246000007f);
    private Vector3 movePosR = new Vector3(-6.13100004f, 0, -0.246000007f);
    private AudioSource doorSource;
    private int count = 0;
    private bool isOpen = false;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) //if other gameobject is on the layer "Player" (anomaly and player are on this)
        {
            count++;
            if (!isOpen)
            {
                isOpen = true;
                wingL.transform.DOKill();
                wingR.transform.DOKill();
                wingL.transform.DOLocalMove(movePosL, duration);
                wingR.transform.DOLocalMove(movePosR, duration);
                doorSource.PlayOneShot(openClip);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7) //checks if no colliders in trigger, closes if true
        {
            count--;
            if (count == 0 && isOpen)
            {
                wingL.transform.DOKill();
                wingR.transform.DOKill();
                wingL.transform.DOLocalMove(defaultPosL, duration / 2);
                wingR.transform.DOLocalMove(defaultPosR, duration / 2);
                doorSource.PlayOneShot(closeClip);
                isOpen = false;
            }
        }
    }
}