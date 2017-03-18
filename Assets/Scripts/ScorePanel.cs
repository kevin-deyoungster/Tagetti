using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour {

    public GameObject lblScore;
    public GameObject lblhighScore;
    public GameObject lbldeaths;

	// Use this for initialization
	void Start () {
        setLabelText(lblScore, PlayerPrefs.GetFloat("Current Score").ToString());
        setLabelText(lblhighScore, PlayerPrefs.GetFloat("High Score").ToString());
        setLabelText(lbldeaths, PlayerPrefs.GetFloat("Deaths").ToString());
    }

    void setLabelText(GameObject label, string text)
    {
        label.GetComponent<Text>().text = text;
    }

    public static float updateHighScore(float score)
    {
        if(score > PlayerPrefs.GetFloat("High Score"))
        {
            PlayerPrefs.SetFloat("High Score",score);
        }
        return PlayerPrefs.GetFloat("High Score");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level01");
    }

    public void ClearUserData()
    {
        PlayerPrefs.DeleteAll();

    }


   
}

