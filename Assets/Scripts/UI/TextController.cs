using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	
    public void Setup(int position,string name,string score)
    {

        transform.Find("Position").gameObject.GetComponent<Text>().text = GetPositionText(position);
        transform.Find("Name").gameObject.GetComponent<Text>().text = name;
        transform.Find("Score").gameObject.GetComponent<Text>().text = score;
    }

    public void Colorize()
    {
        Color yellow = Color.yellow;
        transform.Find("Position").gameObject.GetComponent<Text>().color = yellow;
        transform.Find("Name").gameObject.GetComponent<Text>().color = yellow;
        transform.Find("Score").gameObject.GetComponent<Text>().color = yellow;
    }

    public static string GetPositionText(int position)
    {
        switch (position)
        {
            case 0:
                return "";
            case 1:
                return "1st";
            case 2:
                return "2nd";
            case 3:
                return "3rd";
            default:
                return position.ToString() + "th";
        }
    }

}
