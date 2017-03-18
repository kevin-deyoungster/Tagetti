using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public int currentScore;
    public GameObject scoreLabel;

    public void ScoreUp()
    {
        currentScore++;
        scoreLabel.GetComponent<Text>().text = "Score : " + currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreLabel.GetComponent<Text>().text = "Score : " + currentScore;
    }

    
}
