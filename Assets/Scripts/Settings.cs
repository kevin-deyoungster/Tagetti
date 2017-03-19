using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public bool backgroundMuted = false;
    //public GameObject soundSwitch;
    public Sprite on;
    public Sprite off;
 
    public void ClickedBackground()
    {
        if (backgroundMuted)
        {
            SoundController.GetInstance().volume = 1.0f;
            SoundController.GetInstance().UnMute();
            backgroundMuted = false;
            //soundSwitch.GetComponent<Image>().sprite = on;
            GameObject.FindGameObjectWithTag("SoundButton").GetComponent<Image>().sprite = on;
        }
        else
        {
            SoundController.GetInstance().volume = 0f;
            SoundController.GetInstance().Mute();
            backgroundMuted = true;
            //soundSwitch.GetComponent<Image>().sprite = off;
            GameObject.FindGameObjectWithTag("SoundButton").GetComponent<Image>().sprite = off;
        }
    }
}
