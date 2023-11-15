using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnomalyNoteUnlock : MonoBehaviour
{
    [SerializeField] private GameObject noteContainer;
    [SerializeField] private Vector3 movePos = new Vector3();
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip clip;
    private void OnEnable()
    {
        PlanetsManager.planetCorrect += UnlockNote;
    }
    private void OnDisable()
    {
        PlanetsManager.planetCorrect -= UnlockNote;
    }

    void UnlockNote()
    {
        audiosource.gameObject.SetActive(true);
        noteContainer.gameObject.transform.DOLocalMove(movePos, 1f);
        audiosource.PlayOneShot(clip);
    }
}