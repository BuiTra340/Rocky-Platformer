using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("AudioClip")]
    public AudioClip background;
    public AudioClip attack;
    public AudioClip Ncapthanhcong;
    public AudioClip Ncapthatbai;
    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip audio)
    {
        sfxSource.PlayOneShot(audio);
    }
}
