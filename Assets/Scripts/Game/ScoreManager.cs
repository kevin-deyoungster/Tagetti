using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public int currentScore;
    public Text scoreLabel;

    public void IncreaseScore(int points)
    {
        currentScore += points;
        scoreLabel.text = "Score : " + currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreLabel.text = "Score : " + currentScore;
    }

    public float GetHighScore()
    {
        if (currentScore > PlayerPrefs.GetFloat("High Score"))
        {
            PlayerPrefs.SetFloat("High Score", currentScore);
        }
        return PlayerPrefs.GetFloat("High Score");
    }



}
