using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject.tag == "Pocket")
        {
            FindObjectOfType<GameManager>().balls.Remove(gameObject);
            FindObjectOfType<ScoreManager>().ScoreUp();
            Destroy(gameObject);
        }
    }
}
