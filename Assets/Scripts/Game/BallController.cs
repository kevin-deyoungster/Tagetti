using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    // When ball enters a pocket increase score and remove the ball
    void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject.tag == "Pocket")
        {
            FindObjectOfType<GameManager>().balls.Remove(gameObject);
            FindObjectOfType<ScoreManager>().IncreaseScore(1);
            Destroy(gameObject);
        }
    }
}
