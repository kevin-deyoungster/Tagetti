using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerConnector : MonoBehaviour {

    public GameObject leaderboard;
    public GameObject leaderlist;
    public GameObject leaderClose;
    public RectTransform myPanel;
    public Text globalScore;

    public void CreateUser(string name)
    {
        GameObject status = GameObject.Find("Status");
        status.GetComponent<Text>().text = "";

        if (name.Contains(" "))
        {
            status.GetComponent<Text>().text = "No Spaces Allowed in Username";
        }
        else if(string.IsNullOrEmpty(name))
        {
            status.GetComponent<Text>().text = "Please type in something";
        }
        else
        {
            StartCoroutine(WWWCreateUser(name,(myReturnValue) =>
            {
                if (myReturnValue)
                {
                    status.GetComponent<Text>().text = "Success";
                    SceneManager.LoadScene("StatsPage");
                }
                else
                {
                   status.GetComponent<Text>().text = "Error in Connection";
                }
            }));
  
        }
    }

    IEnumerator WWWCreateUser(string name,System.Action<bool> callback)
    {
        Dictionary<string, string> header = new Dictionary<string, string>();
        string userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
        header.Add("User-Agent", userAgent);
        WWW w = new WWW("https://tagetti.herokuapp.com/create/" + name + "/0", null, header);
        yield return w;

        if(w.error == null)
        {
            PlayerPrefs.SetString("Username", name);
            callback(true);
        }
        else
        {
            callback(false);
        }
        //yield return new WaitForSeconds(2f);
    }

    public void updateUser()
    {
        StartCoroutine(WWWUpdateUser());
    }
    IEnumerator WWWUpdateUser()
    {
        Dictionary<string, string> header = new Dictionary<string, string>();
        string userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
        header.Add("User-Agent", userAgent);
        WWW w = new WWW("https://tagetti.herokuapp.com/update/" + PlayerPrefs.GetString("Username") + "/" + PlayerPrefs.GetFloat("High Score"), null, header);
        yield return w;
    }

    public void showLeaderBoard()
    {
        StartCoroutine(WWWLeaderboard());
        updateUser();
    }

    IEnumerator WWWLeaderboard()
    {
        Dictionary<string, string> header = new Dictionary<string, string>();
        string userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
        header.Add("User-Agent", userAgent);
        WWW w = new WWW("https://tagetti.herokuapp.com/leaderboard", null, header);
        yield return w;

        if (w.error == null)
        {
            leaderboard.SetActive(true);
            leaderClose.SetActive(true);
            string[] Players = new string[10];

            GameObject newTextBox = (GameObject)Resources.Load("TextPrefab");

            string[] people = w.text.Split('|');
            for (int i = 0; i < people.Length - 1; i++)
            {
                string[] userData = people[i].Split('*');
                Players[i] = userData[0].Replace("\"",string.Empty) + " : " + userData[1];
            }

            for (int i = 0; i < Players.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(Players[i]))
                {
                    GameObject newText = Instantiate(newTextBox);
                    newText.GetComponent<RectTransform>().sizeDelta = new Vector2(newText.GetComponent<RectTransform>().rect.width * 2, newText.GetComponent<RectTransform>().rect.height);
                    newText.transform.SetParent(myPanel);
                    newText.GetComponent<Text>().text = (i + 1) + " " + Players[i];

                    try
                    {
                        if (Players[i].Contains(PlayerPrefs.GetString("Username")))
                        {
                            newText.GetComponent<Text>().color = Color.yellow;
                        }
                    }
                    catch (Exception e) { }
                }
            }
        }
        else
        {
            print("Error" + w.error);
        }
    }

    public void GetTopScore()
    {
        StartCoroutine(WWWTopScore((myReturnValue) =>
        {
            topScore = int.Parse(myReturnValue.ToString());
        }));

    }

    private int topScore;
    IEnumerator WWWTopScore(System.Action<string> callback)
    {
        Dictionary<string, string> header = new Dictionary<string, string>();
        string userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
        header.Add("User-Agent", userAgent);
        WWW w = new WWW("https://tagetti.herokuapp.com/leaderboard", null, header);
        yield return w;

        if (w.error == null)
        {

            string[] people = w.text.Split('|');
            string[] userData = people[0].Split('*');
            globalScore.GetComponent<Text>().text = userData[1].ToString();
            PlayerPrefs.SetInt("Global Score", int.Parse(userData[1]));

        }
        else
        {
            print("Error" + w.error);
        }
    }

    public void CloseLeaderboard()
    {
        leaderboard.SetActive(false);
        leaderClose.SetActive(false);
        
        foreach(Transform child in leaderlist.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
