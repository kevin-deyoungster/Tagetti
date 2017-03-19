using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour {

    public GameObject lblScore;
    public GameObject lblhighScore;
    public GameObject lbldeaths;
    public GameObject lblUsername;

	// Use this for initialization
	void Start () {

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Username")))
        {
            SceneManager.LoadScene("Ask");
        }
        else
        {
            setLabelText(lblScore, PlayerPrefs.GetFloat("Current Score").ToString());
            setLabelText(lblhighScore, PlayerPrefs.GetFloat("High Score").ToString());
            setLabelText(lbldeaths, PlayerPrefs.GetFloat("Deaths").ToString());
            setLabelText(lblUsername, PlayerPrefs.GetString("Username"));
        }
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
       LevelManager.GetInstance().LoadGame();
    }

    public void ClearUserData()
    {
        PlayerPrefs.DeleteAll();

    }


   
}

