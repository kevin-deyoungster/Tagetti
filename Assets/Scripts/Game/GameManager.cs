using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Pause Menu
    private List<GameObject> pausePanel = new List<GameObject>();
    public bool gamePaused = false;
    public GameObject pauseButton;

    //Balls
    public GameObject ballPrefab;
    public List<GameObject> balls = new List<GameObject>();
    private int numberOfBalls = 1;

    public void Start()
    {
        //Setup the Pause Overlay
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("PauseMenu"))
        {
            pausePanel.Add(item);
            item.SetActive(false);
        }

        //Randomly Place the balls on stage
        GenerateBalls(numberOfBalls);
    }


    public void Update()
    {
        //Constantly check if the balls get finished. If so level up
       if(balls.Count == 0)
        {
            FindObjectOfType<LevelManager>().LevelUp();
            numberOfBalls += 2;
            GenerateBalls(numberOfBalls);
        }

        ReadInput();
    }

    public void ReadInput()
    {
        // Toggle the Pause Menu
        if (Input.GetKeyDown(KeyCode.P) && gamePaused == false)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.P) && gamePaused == true)
        {
            ResumeGame();
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

    public void GameOver()
    {
        PlayerPrefs.SetFloat("Current Score", FindObjectOfType<ScoreManager>().currentScore);
        PlayerPrefs.SetFloat("High Score", FindObjectOfType<ScoreManager>().GetHighScore());
      
        if (PlayerPrefs.HasKey("Deaths"))
        {
            PlayerPrefs.SetFloat("Deaths", PlayerPrefs.GetFloat("Deaths") + 1f);
        }
        else
        {
            PlayerPrefs.SetFloat("Deaths", 0);
        }
       
        SceneManager.GetInstance().LoadStatsPage(); 
    }

    public void PauseGame()
    {
        foreach (GameObject item in pausePanel)
        {
            item.SetActive(true);
        }
        pauseButton.SetActive(false);

        Time.timeScale = 0;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        foreach (GameObject item in pausePanel){
            item.SetActive(false);
        }
        pauseButton.SetActive(true);


        Time.timeScale = 1;
        gamePaused = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.GetInstance().LoadMainMenu();
    }

    public void LoadStatsPage()
    {
        Time.timeScale = 1;
        SceneManager.GetInstance().LoadStatsPage();
    }

    public void ToggleSound()
    {
        FindObjectOfType<Settings>().ToggleBackgroundMusic();
    }
}
