using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegistrationManager : MonoBehaviour {

    public InputField username;
    public NetworkManager netManager;

    public void SetUserName()
    {
        if (ValidateTextBoxes())
        {
            netManager.CreateUser(username.text);
        }
    }
    public bool ValidateTextBoxes()
    {
        bool response = true;
        InputField[] inputs = FindObjectsOfType<InputField>();

        foreach (InputField input in inputs)
        {
            if (input.text.Contains(" "))
            {
                netManager.Alert("No Spaces Allowed in Username", "Status");
                response = false;
            }
            else if (string.IsNullOrEmpty(input.text))
            {
                netManager.Alert("Please type in something", "Status");
                response = false;
            }
            else
            {
                netManager.Alert("", "Status");
                response = true;
            }
        }
        return response;
    }
}
