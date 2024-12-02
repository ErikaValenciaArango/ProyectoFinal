using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    [SerializeField] AudioSource musicSource, effectsSource;
    public AudioMixer master;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip music, bool loop)
    {
        musicSource.Stop();
        musicSource.clip = music;
        musicSource.Play();
        musicSource.loop = loop;
    }

    public void MuteMusic()
    {
        musicSource.volume = 0;
        effectsSource.volume = 0;
    }

    public void VolumeMusic()
    {
        musicSource.volume = 0.4f;
        effectsSource.volume = 0.8f;
    }
}
