using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject settings;
	// Use this for initialization
	public void ShowSettings()
    {        
        settings.SetActive(true);
    }

    public void Start()
    {
        settings.SetActive(false);
    }
}
