using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    private static SoundController instance;

    public float volume;


    public AudioSource backgroundSound;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public static SoundController GetInstance()
    {
        return instance;
    }


    public void Background()
    {
        backgroundSound.Play();
    }

    public void Mute()
    {
        AudioListener.volume = 0;
    }

    public void UnMute()
    {
        AudioListener.volume = 1;
    }
}
