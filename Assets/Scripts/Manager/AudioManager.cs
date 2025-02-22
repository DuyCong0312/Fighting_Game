using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    public AudioClip background;
    public AudioClip jump;
    public AudioClip attack;
    public AudioClip dash; 
    public AudioClip touchGround;
    public AudioClip step1;
    public AudioClip step3;

    private void Start()
    {
        PlaySound(background);
    }

    public void PlaySound(AudioClip clip)
    {
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxAudioSource.PlayOneShot(sfxClip);
    }
}
