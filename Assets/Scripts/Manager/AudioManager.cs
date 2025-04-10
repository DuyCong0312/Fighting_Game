using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    public AudioClip background;
    public AudioClip jump;
    public AudioClip dash; 
    public AudioClip touchGround;
    public AudioClip step1;
    public AudioClip step3;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        } 
        DontDestroyOnLoad(gameObject);
    }

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
