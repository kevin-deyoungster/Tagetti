using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public bool backgroundMuted = false;
    public bool fxMuted = false;

    public void Awake()
    {
        transform.Find("SoundSlider").gameObject.GetComponent<Slider>().value = SoundController.GetInstance().backgroundVolume;
        transform.Find("FXSlider").gameObject.GetComponent<Slider>().value = SoundController.GetInstance().fxVolume;
    }
    public void ClickedBackground()
    {
        if (backgroundMuted)
        {
            SoundController.GetInstance().backgroundVolume = 1.0f;
            transform.Find("SoundButton").gameObject.GetComponent<Image>().color = new Color(198,255,255);
            transform.Find("SoundSlider").gameObject.GetComponent<Slider>().value = 1.0f;
            backgroundMuted = false;
        }
        else
        {
            SoundController.GetInstance().backgroundVolume = 0f;
            transform.Find("SoundButton").gameObject.GetComponent<Image>().color = new Color(255,255,255);
            transform.Find("SoundSlider").gameObject.GetComponent<Slider>().value = 0f;
            backgroundMuted = true;
        }
    }

    public void ClickedFX()
    {
        if (fxMuted)
        {
            SoundController.GetInstance().fxVolume = 1.0f;
            transform.Find("FXButton").gameObject.GetComponent<Image>().color = new Color(198, 255, 255);
            transform.Find("FXSlider").gameObject.GetComponent<Slider>().value = 1.0f;
            fxMuted = false;
        }
        else
        {
            SoundController.GetInstance().fxVolume = 0f;
            transform.Find("FXButton").gameObject.GetComponent<Image>().color = new Color(255,255,255);
            transform.Find("FXSlider").gameObject.GetComponent<Slider>().value = 0f;
            fxMuted = true;
        }
    }

    public void UpdateBackgroundVolume()
    {
        SoundController.GetInstance().backgroundVolume = transform.Find("SoundSlider").gameObject.GetComponent<Slider>().value;
    }

    public void UpdateFXVolume()
    {
        SoundController.GetInstance().fxVolume = transform.Find("FXSlider").gameObject.GetComponent<Slider>().value;
    }

    public void HideSettings()
    {
        gameObject.SetActive(false);
    }
}
