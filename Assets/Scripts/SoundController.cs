using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    private static SoundController instance;

    public float backgroundVolume;
    public float fxVolume;

	void Awake()
    {
        if(instance!= null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static SoundController GetInstance()
    {
        return instance;
    }
}
