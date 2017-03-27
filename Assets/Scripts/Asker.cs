using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Asker : MonoBehaviour {

    public InputField username;
    public NetworkManager serveConnect;
    public bool serverConnected = false;

    public void SetUserName()
    {
        serveConnect.CreateUser(username.text);
    }
}
