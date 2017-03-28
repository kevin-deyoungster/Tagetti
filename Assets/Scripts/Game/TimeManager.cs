using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public GameObject timeLabel;

    public float timeLeft = 30;
    public int resetTime = 30;
    public bool stopCounting = false;

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
        timeLeft = resetTime;
    }
}
