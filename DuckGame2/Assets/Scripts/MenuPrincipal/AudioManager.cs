using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instanceAudioManager;

    [Header("----Audio Source----")]
    [SerializeField] AudioSource musicaSource;
    [SerializeField] AudioSource sfxSource;

    [Header("----Audio Clip----")]
    public AudioClip musicaFondo;
    public AudioClip muerte;


    [Header("----Sliders----")]
    public Slider VolumeMusicaSlider;
    public Slider VolumeSFXSlider;




    /*[SerializeField] AudioMixer audioMixerInst;
    public AudioMixerGroup grupoMusicaMixer;*/


    private void Start()
    {
        instanceAudioManager = this;
        PlayMusic(musicaFondo);

        InizializadorDeControlDeVolumenes();
        

    }

    private void InizializadorDeControlDeVolumenes()
    {
        if (PlayerPrefs.GetFloat("VolumenMusica") != 0)
        {
            //grupoMusicaMixer.audioMixer.SetFloat("Musica", PlayerPrefs.GetFloat("Volumen"));
            VolumeMusicaSlider.value = PlayerPrefs.GetFloat("VolumenMusica");
            musicaSource.volume = PlayerPrefs.GetFloat("VolumenMusica");
        }
        AjustarVolumenMusica(VolumeMusicaSlider.value);

        if (PlayerPrefs.GetFloat("VolumenSFX") != 0)
        {
            VolumeSFXSlider.value = PlayerPrefs.GetFloat("VolumenSFX");
            sfxSource.volume = PlayerPrefs.GetFloat("VolumenSFX");
        }
        AjustarVolumenSFX(VolumeMusicaSlider.value);
    }

    public void AjustarVolumenMusica(float value)
    {
        //grupoMusicaMixer.audioMixer.SetFloat("Musica", value);
        musicaSource.volume = value;
        PlayerPrefs.SetFloat("VolumenMusica", value);

        Debug.Log(PlayerPrefs.GetFloat("VolumenMusica"));
    }

    public void AjustarVolumenSFX(float value)
    {
        //grupoMusicaMixer.audioMixer.SetFloat("Musica", value);
        sfxSource.volume = value;
        PlayerPrefs.SetFloat("VolumenSFX", value);

        Debug.Log(PlayerPrefs.GetFloat("VolumenSFX"));
    }


    public void PlayMusic(AudioClip clip)
    {
        musicaSource.clip = clip;
        musicaSource.Play();
        
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
