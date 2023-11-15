using DG.Tweening;
using UnityEngine;

public class UISettings : MonoBehaviour
{ //holds settings for individual UI menus, must be placed on the UI gameobject.
    public bool doAnim;
    public bool doSound;
    public AudioClip clip;

    public Ease ease;
    public float duration;
}
