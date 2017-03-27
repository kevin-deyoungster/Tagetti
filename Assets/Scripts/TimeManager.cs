using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public float timeLeft = 30;
    public GameObject timeLabel;
    public bool stopCounting = false;

	// Use this for initialization
	void Start () {
        timeLabel.GetComponent<Text>().color = Color.white;
    }

    // Update is called once per frame
    void Update () {
        if (!stopCounting)
        {
            timeLeft -= Time.deltaTime;
            timeLabel.GetComponent<Text>().text = "Time: " + (int)timeLeft;
            if (timeLeft < 0)
            {
                FindObjectOfType<GameManager>().GameOver();
            }
        }
	}

    public void ResetTimer()
    {
        timeLeft = 5;
    }
}
