using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setup(int position,string name,int score)
    {
        transform.Find("Position").gameObject.GetComponent<Text>().text = position.ToString();
        transform.Find("Name").gameObject.GetComponent<Text>().text = name;
        transform.Find("Score").gameObject.GetComponent<Text>().text = score.ToString();
    }

    public void Colorize()
    {
        Color yellow = Color.yellow;
        transform.Find("Position").gameObject.GetComponent<Text>().color = yellow;
        transform.Find("Name").gameObject.GetComponent<Text>().color = yellow;
        transform.Find("Score").gameObject.GetComponent<Text>().color = yellow;
    }
}
