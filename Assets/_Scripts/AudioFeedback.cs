using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeedback : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource targetAudioSource = null;
    [Range(0, 1)]
    public float volume = 1;

    private void Start()
    {
        if (targetAudioSource == null)
        {
            targetAudioSource = FindObjectOfType<AudioSource>();
        }
    }
    public void PlayClip()
    {
        if (clip == null)
            return;
        if (targetAudioSource == null)
            return;
        targetAudioSource.volume = this.volume;
        targetAudioSource.PlayOneShot(clip);
    }

    public void PlaySpecificClip(AudioClip clipToPlay = null)
    {
        if (clipToPlay == null)
            clipToPlay = clip;
        if (clipToPlay == null)
            return;
        targetAudioSource.volume = this.volume;
        targetAudioSource.PlayOneShot(clipToPlay);
    }
}
