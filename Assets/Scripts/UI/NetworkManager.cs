using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

   public Dictionary<string, string> header = new Dictionary<string, string>();

    void Start()
    {
        string userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
        header.Add("User-Agent", userAgent);

    }
  

    public void CreateUser(string name)
    {
            WWW www = new WWW("https://tagetti.herokuapp.com/create/" + name + "/0", null, header);
            StartCoroutine(CreateUserConnection(www,name));
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
                SceneManager.GetInstance().LoadStatsPage();
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
    

    public void UpdateUserScore()
    {
        WWW www = new WWW("https://tagetti.herokuapp.com/update/" + PlayerPrefs.GetString("Username") + "/" + PlayerPrefs.GetFloat("High Score"), null, header);
        UpdateUserScoreConnection(www);
    }
    IEnumerator UpdateUserScoreConnection(WWW www)
    {
        yield return www;

        if (www.error != null) { Alert("Error in Connection. Couldn't Update High Score Online.", "Status"); };

    }
 

    public void GetOnlineInfo()
    {
        WWW www = new WWW("https://tagetti.herokuapp.com/leaderboard", null, header);
        StartCoroutine(GetOnlineInfoConnection(www));
    }
    IEnumerator GetOnlineInfoConnection(WWW www)
    {
        yield return www;
        
        if (www.error == null)
        {
            List<string> usernames = new List<string>();
            List<string> scores = new List<string>();

            string[] people = www.text.Split('|');

            for (int i = 0; i < people.Length - 1; i++)
            {
                usernames.Add(people[i].Split('*')[0].Replace("\"", string.Empty));
                scores.Add(people[i].Split('*')[1]);
             
                //Get Position
                if (usernames[i] == PlayerPrefs.GetString("Username"))
                {
                    FindObjectOfType<StatsPanelManager>().setLabelText(GameObject.Find("Username"), PlayerPrefs.GetString("Username") + " : " + TextController.GetPositionText(i + 1).ToString());
                }
            }

            //Get Global High Score
            PlayerPrefs.SetString("Global Score", scores[0]);
            GameObject.Find("GlobalScore").GetComponent<Text>().text = scores[0];
            GameObject.Find("TopScorer").GetComponent<Text>().text = usernames[0];

            FindObjectOfType<StatsPanelManager>().PrepareLeaderBoard(usernames, scores, 5);
        }
        else
        {
            GameObject.Find("GlobalScore").GetComponent<Text>().text = "X";
            Alert("Please Connect to Internet. Error in Connection", "Status");
        }

    }


    public void Alert(string info, string textName)
    {
        Text status = GameObject.Find(textName).GetComponent<Text>();
        status.text = info;
    }

}
