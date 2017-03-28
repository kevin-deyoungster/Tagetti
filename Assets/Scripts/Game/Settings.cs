using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public bool backgroundMuted = false;
    public Sprite on;
    public Sprite off;
 
    public void ToggleBackgroundMusic()
    {
        if (backgroundMuted)
        {
            SoundController.GetInstance().volume = 1.0f;
            SoundController.GetInstance().UnMute();
            backgroundMuted = false;
            GameObject.Find("btnToggleSound").GetComponent<Image>().sprite = on;
        }
        else
        {
            SoundController.GetInstance().volume = 0f;
            SoundController.GetInstance().Mute();
            backgroundMuted = true;
            GameObject.Find("btnToggleSound").GetComponent<Image>().sprite = off;
        }
    }
}
