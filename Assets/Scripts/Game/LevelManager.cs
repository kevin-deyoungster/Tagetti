using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public int currentLevel = 1;
    public Text levelLabel;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LevelUp()
    {
        currentLevel++;
        levelLabel.text = "Level " + currentLevel;

        FindObjectOfType<TimeManager>().ResetTimer();


    }
}
