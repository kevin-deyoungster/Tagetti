using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

    public GameObject leaderboard;
    public GameObject leaderlist;
    public GameObject leaderClose;
    public RectTransform myPanel;

    public Text globalScore;

    public Dictionary<string, string> header = new Dictionary<string, string>();

    void Start()
    {
        string userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
        header.Add("User-Agent", userAgent);

    }
    void Alert(string info,string textName)
    {
        Text status = GameObject.Find(textName).GetComponent<Text>();
        status.text = info;
    }
    public bool ValidateTextBoxes()
    {
        bool response = true;
        InputField[] inputs = FindObjectsOfType<InputField>();
        
        foreach(InputField input in inputs)
        {
            if(input.text.Contains(" ")){
                Alert("No Spaces Allowed in Username","Status");
                response = false;
            }
            else if (string.IsNullOrEmpty(input.text)) { 
                Alert("Please type in something", "Status");
                response = false;
            }
            else
            {
                Alert("", "Status");
                response = true;
            }
        }
        return response;
    }


    public void CreateUser(string name)
    {
        if (ValidateTextBoxes())
        {
            WWW www = new WWW("https://tagetti.herokuapp.com/create/" + name + "/0", null, header);
            StartCoroutine(CreateUserConnection(www,name));
        }
    }
    IEnumerator CreateUserConnection(WWW www,string name)
    {
        yield return www;

        if(www.error == null)
        {
            if(www.text == "true")
            {
                Alert("Successfully Created", "Status");
                PlayerPrefs.SetString("Username", name);
                SceneManager.LoadScene("StatsPage");
            }
            else
            {
                Alert("Name Already Exists", "Status");
            }
        }
        else
        {
            Alert("Error in Connection. Please Turn On Internet", "Status");
        }
    }
    

    public void UpdateUser()
    {
        WWW www = new WWW("https://tagetti.herokuapp.com/update/" + PlayerPrefs.GetString("Username") + "/" + PlayerPrefs.GetFloat("High Score"), null, header);
        UpdateUserConnection(www);
    }
    IEnumerator UpdateUserConnection(WWW www)
    {
        yield return www;

        if (www.error != null) { Alert("Error in Connection. Couldn't Update High Score Online.", "Status"); };

    }
 

    public void GetLeaderBoard(int limit)
    {
        UpdateUser();
        WWW www = new WWW("https://tagetti.herokuapp.com/leaderboard/" + limit, null, header);
        StartCoroutine(GetLeaderBoardConnection(www,limit));
    }
    IEnumerator GetLeaderBoardConnection(WWW www,int limit)
    {
        yield return www;

        if (www.error == null)
        {
            leaderboard.SetActive(true);
            leaderClose.SetActive(true);

            string[] Players = new string[limit];

            GameObject newTextBox = (GameObject)Resources.Load("TextPrefab");

            string[] people = www.text.Split('|');

            for (int i = 0; i < people.Length - 1; i++)
            {
                string[] userData = people[i].Split('*');

                Players[i] = userData[0].Replace("\"",string.Empty).PadRight(10,' ') + " : " + userData[1];
            }

            for (int i = 0; i < Players.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(Players[i]))
                {
                    GameObject newText = Instantiate(newTextBox);
                    newText.GetComponent<RectTransform>().sizeDelta = new Vector2(newText.GetComponent<RectTransform>().rect.width * 2, newText.GetComponent<RectTransform>().rect.height);
                    newText.transform.SetParent(myPanel);
                    newText.GetComponent<TextController>().Setup((i + 1), Players[i].Split(':')[0], int.Parse(Players[i].Split(':')[1]));

                    try
                    {
                        if (Players[i].Contains(PlayerPrefs.GetString("Username")))
                        {
                            newText.GetComponent<TextController>().Colorize();
                        }
                    }
                    catch (Exception e) { }
                }
            }
        }
        else
        {
            Alert("Error in Connection", "LeaderBoardStatus");
        }
    }


    public void GetGlobalHighScore()
    {
        WWW www = new WWW("https://tagetti.herokuapp.com/leaderboard/1", null, header);
        StartCoroutine(GetGlobalHighScoreConnection(www));
    }
    IEnumerator GetGlobalHighScoreConnection(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            string playerScore = www.text.Split('*')[1].Replace("|", "").Replace("\"", "");
            GameObject.Find("Global_Score").GetComponent<Text>().text = playerScore;
            PlayerPrefs.SetInt("Global Score", int.Parse(playerScore));
        }
        else
        {
            GameObject.Find("Global_Score").GetComponent<Text>().text = "X";
            Alert("Error in Connection. Couldn't update Global Score", "Status");
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
