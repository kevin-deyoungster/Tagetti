using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float bulletSpeed;
	    
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed);
	}

    void OnCollisionEnter2D(Collision2D other)
    {   
        if(other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

}
