using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip levelMusic;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Mute()
    {
        AudioManager.Instance.MuteMusic();
    }

    public void UnMute()
    {
        AudioManager.Instance.VolumeMusic();
    }
}
