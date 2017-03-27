using System;
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
    public GameObject lblGlobalScore;

    public NetworkManager netManager;

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
            setLabelText(lblGlobalScore, PlayerPrefs.GetString("Global Score").ToString());
        }

        netManager.UpdateUser();
        netManager.UpdateScores();
    }

    public void setLabelText(GameObject label, string text)
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


    public GameObject leaderboard;
    public GameObject leaderboardList;
    public GameObject leaderClose;

    public void PrepareLeaderBoard(List<string> usernames, List<string> scores, int size)
    {
        if(usernames.Count < size)
        {
            size = usernames.Count;
        }
        for (int i = 0; i < size; i++)
        {
            GameObject theText = leaderboardList.transform.Find("Player" + (i + 1).ToString()).gameObject;
            theText.GetComponent<TextController>().Setup((i + 1), usernames[i], scores[i]);

            try
            {
                if (usernames[i] == PlayerPrefs.GetString("Username"))
                {
                    theText.GetComponent<TextController>().Colorize();
                }
            }
            catch (Exception e) { }
        }
    }

    public void ShowLeaderboard()
    {
        netManager.UpdateScores();
        leaderboard.SetActive(true);
        leaderClose.SetActive(true);
    }



    public void CloseLeaderboard()
    {
        leaderboard.SetActive(false);
        leaderClose.SetActive(false);

        foreach (Transform child in leaderboard.transform.Find("LeaderBoardStatus").transform)
        {
            if (child.gameObject.tag == "Score")
            {
                child.gameObject.GetComponent<TextController>().Setup(0, " ", "");
            }
        }
    }



}

