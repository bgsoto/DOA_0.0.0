using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIMenuDoTweenAnims : MonoBehaviour
{
    public GameObject menuToOpen;
    public Ease ease;
    public float duration;
    public void OpenMenuAnim()
    {
        menuToOpen.transform.DOScale(1, duration).SetEase(ease);
        menuToOpen.GetComponent<CanvasGroup>().DOFade(1, duration);
    }
    public void CloseMenuAnim()
    {
        menuToOpen.transform.DOScale(0, duration).SetEase(ease);
        menuToOpen.GetComponent<CanvasGroup>().DOFade(0, duration);
        menuToOpen.SetActive(false);
    }
}
