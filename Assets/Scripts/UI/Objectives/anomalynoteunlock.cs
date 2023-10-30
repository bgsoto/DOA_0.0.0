using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class anomalynoteunlock : MonoBehaviour
{
    [SerializeField] private GameObject noteToUnlock;
    [SerializeField] private Vector3 movePos = new Vector3();
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
        noteToUnlock.gameObject.transform.DOMove(movePos, 1f);
    }
}
