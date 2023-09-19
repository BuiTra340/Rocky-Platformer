using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSeting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSLider;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            setMusicVolume();
            setSFXVolume();
        }
    }

    public void LoadVolume()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSLider.value = PlayerPrefs.GetFloat("sfxVolume");
        musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        sfxSource.volume = SFXSLider.value = PlayerPrefs.GetFloat("sfxVolume");
        setMusicVolume();
        setSFXVolume();
    }

    public void setMusicVolume()
    {
        float volume = MusicSlider.value;
        musicSource.volume = volume;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void setSFXVolume()
    {
        float volume = SFXSLider.value;
        sfxSource.volume = volume;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
}
