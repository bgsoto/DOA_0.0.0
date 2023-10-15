using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleCall : MonoBehaviour
{
    [SerializeField] private TextAsset subs;
    [SerializeField] private AudioSource sound;
   public void PlayAudio()
    {
        SubtitleDisplayer subtitles = FindObjectOfType<SubtitleDisplayer>();
        subtitles.Subtitle = subs;
        sound.Play();
        StartCoroutine(subtitles.Begin());
    }
}
