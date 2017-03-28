using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsPanelManager : MonoBehaviour {

    public GameObject lblScore;
    public GameObject lblhighScore;
    public GameObject lblUsername;
    public GameObject lblGlobalScore;

    public NetworkManager netManager;

	void Start () {

        //Check if user has registered and load details else take user to registration page
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Username")))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Registration");
        }
        else
        {
            setLabelText(lblScore, PlayerPrefs.GetFloat("Current Score").ToString());
            setLabelText(lblhighScore, PlayerPrefs.GetFloat("High Score").ToString());
            setLabelText(lblUsername, PlayerPrefs.GetString("Username"));
            setLabelText(lblGlobalScore, PlayerPrefs.GetString("Global Score").ToString());
        }

        //Take Score from game that just ended and post it online. Get online info as well
        netManager.UpdateUserScore();
        netManager.GetOnlineInfo();
    }

    public void LoadGame()
    {
       SceneManager.GetInstance().LoadGame();
    }

    public GameObject leaderboard;
    public GameObject leaderboardList;
    public GameObject leaderClose;

    //Put data unto the leaderboard
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
            catch (Exception e) { print(e); }
        }
    }

    public void ShowLeaderboard()
    {
        netManager.GetOnlineInfo();
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

    public void setLabelText(GameObject label, string text)
    {
        label.GetComponent<Text>().text = text;
    }


}

