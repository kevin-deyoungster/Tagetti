using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Asker : MonoBehaviour {

    public InputField username;   
    public void SetUserName()
    {
        if (!string.IsNullOrEmpty(username.text))
        {
            PlayerPrefs.SetString("Username", username.text);
            SceneManager.LoadScene("StatsPage");
        }
       
    }
}
