using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    private static SoundController instance;

    public float backgroundVolume;
    public float fxVolume;

    public AudioSource gameOverSound;
    public AudioSource shootSound;
    public AudioSource backgroundSound;


    void Awake()
    {
        if(instance!= null && instance != this)
        {
            //Destroy(gameObject);
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

    public void Shoot()
    {
        shootSound.Play();
    }

    public void GameOver()
    {
        gameOverSound.Play();
    }

    public void Background()
    {
        backgroundSound.Play();
    }
}
