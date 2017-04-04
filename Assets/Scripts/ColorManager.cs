using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour {

    public Dictionary<string, Color> colorMatching = new Dictionary<string, Color>();

    public Color backgroundColor;
    public Color accentColor;
    public Color fontColor;
    public Color leader;

    void Start () {
        backgroundColor = HexStringToColor("#512E67");
        accentColor = HexStringToColor("#C54C82");
        fontColor = HexStringToColor("#FAFAFA");
        leader = HexStringToColor("#FFE98A");

        colorMatching.Add("Main Camera", backgroundColor);
        colorMatching.Add("SideLeft", accentColor);
        colorMatching.Add("SideRight", accentColor);
        colorMatching.Add("Title", fontColor);
        colorMatching.Add("Footer", fontColor);
        colorMatching.Add("btnPlay", fontColor);
        colorMatching.Add("btnToggleSound", fontColor);

        Colorize();
        //GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = backgroundColor;
        
	}
	
    void Colorize()
    {
        foreach(string obj in colorMatching.Keys){
            GameObject obj1 = GameObject.Find(obj);

            //If its a camera 
            try
            {
                if (obj1.GetComponent<Camera>() != null)
                {
                    obj1.GetComponent<Camera>().backgroundColor = colorMatching[obj];
                }
                else if(obj1.GetComponent<Text>() != null)
                {
                    obj1.GetComponent<Text>().color = colorMatching[obj];
                }
                else
                {
                    obj1.GetComponent<Image>().color = colorMatching[obj];
                }
            }
            catch (System.Exception e)
            {
                print(obj1.name);
                print(e);
            }
          
        }
    }

    Color HexStringToColor(string hexName)
    {
        Color tempColor;
        ColorUtility.TryParseHtmlString(hexName, out tempColor);
        return tempColor;
    }

	void Update () {
		
	}
}
