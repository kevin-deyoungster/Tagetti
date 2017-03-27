using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int level = 1;
    public Text lvlLabel;

    public GameObject ballPrefab;
    public List<GameObject> balls = new List<GameObject>();
    public int initialNumberOfBalls = 1;

    public List<GameObject> pauseItems = new List<GameObject>();
    public bool gamePaused = false;

    public GameObject pauseButton;

    public void Start()
    {
        GenerateBalls(initialNumberOfBalls);

        //Initialize the pause aspect
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("PauseMenu"))
        {
            pauseItems.Add(item);
        }
        foreach(GameObject item in pauseItems)
        {
            item.SetActive(false);
        }
    }

    
    public void Update()
    {
       if(balls.Count == 0)
        {
            LevelUp();
        }

        if (Input.GetKeyDown(KeyCode.P) && gamePaused == false)
        {
            Paused();
        }
        else if(Input.GetKeyDown(KeyCode.P) && gamePaused == true)
        {
            UnPaused();
        }
    }

    void GenerateBalls(int numberOfBalls)
    {
        for(int i = 0; i < numberOfBalls; i++)
        {
            Vector3 ballPosition = new Vector3(UnityEngine.Random.Range(-8.0f,5.4f), UnityEngine.Random.Range(2.88f,-3));

            //To Prevent the ball overlapping over the player. i.e. If ball is too close to player redo the UnityEngine.Random 
            if (Vector3.Distance(ballPosition, FindObjectOfType<PlayerController>().transform.position) < 0.9)
            {
                ballPosition = new Vector3(UnityEngine.Random.Range(-8.0f, 5.4f), UnityEngine.Random.Range(2.88f, -3));
            }

            GameObject ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
            balls.Add(ball);
        }
    }

    public void LevelUp()
    {
        level++;
        lvlLabel.text = "Level " + level;

        FindObjectOfType<TimeManager>().ResetTimer();

        initialNumberOfBalls += 2;
        GenerateBalls(initialNumberOfBalls);
    }

    public void GameOver()
    {
       
        //Show the info screen
        PlayerPrefs.SetFloat("Current Score", FindObjectOfType<ScoreManager>().currentScore);
        PlayerPrefs.SetFloat("High Score", ScorePanel.updateHighScore(FindObjectOfType<ScoreManager>().currentScore));

        if (PlayerPrefs.HasKey("Deaths"))
        {
            PlayerPrefs.SetFloat("Deaths", PlayerPrefs.GetFloat("Deaths") + 1f);
        }
        else
        {
            PlayerPrefs.SetFloat("Deaths", 0);
        }
        //SoundController.GetInstance().GameOver();

        
        LevelManager.GetInstance().LoadStatsPage();
        
    }


    public void Paused()
    {
        foreach (GameObject item in pauseItems)
        {
            item.SetActive(true);
        }
  
        Time.timeScale = 0;
        gamePaused = true;
        pauseButton.SetActive(false);
    }

    public void UnPaused()
    {
        foreach (GameObject item in pauseItems){
            item.SetActive(false);
        }
        
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        gamePaused = false;
    }

    public GameObject settings;

    public void ShowSettings()
    {
        Settings[] a = Resources.FindObjectsOfTypeAll<Settings>();
        foreach(Settings set in a)
        {
            set.gameObject.SetActive(true);
        }
    }

    public void ShowScores()
    {
        Time.timeScale = 1;
        LevelManager.GetInstance().LoadStatsPage();
    }

    public void ShowMenu()
    {
        Time.timeScale = 1;
        LevelManager.GetInstance().LoadMainMenu();
    }

    public void Clicked()
    {
        FindObjectOfType<Settings>().ClickedBackground();
    }

   

}
