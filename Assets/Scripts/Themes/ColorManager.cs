using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorManager : MonoBehaviour {

    public Dictionary<string, Color> colorMatching = new Dictionary<string, Color>();
    public Theme[] themes;

    public Color backgroundColor;
    public Color accentColor;
    public Color fontColor;
    public Color leader;
    public Color mild;

    public string themename;

    public void SetTheTheme()
    {
        SetTheme(themename);
    }

    void Start () { 
        themes = transform.GetComponentsInChildren<Theme>();
        SetTheTheme();
    }

	public void SetTheme(string themeName)
    {
        LoadTheme(themeName);
        SetList();
        Colorize();
    }

    void LoadTheme(string themeName)
    {
        foreach(Theme theme in themes)
        {
            if (theme.themeName == themeName)
            {
                backgroundColor = theme.backgroundColor;
                accentColor = theme.accentColor;
                fontColor = theme.fontColor;
                leader = theme.leader;
                mild = theme.mild;
            }
        }
    }

    public void SetList()
    {
        colorMatching.Clear();
        colorMatching.Add("Main Camera", backgroundColor);
        colorMatching.Add("SideLeft", accentColor);
        colorMatching.Add("SideRight", accentColor);
        colorMatching.Add("Title", fontColor);
        colorMatching.Add("Footer", fontColor);
        colorMatching.Add("btnPlay", fontColor);
        colorMatching.Add("btnToggleSound", fontColor);
        colorMatching.Add("Pocket-BottomRight", mild);
        colorMatching.Add("Pocket-TopLeft", mild);
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
                else if(obj1.GetComponent<Button>() != null)
                {
                    obj1.GetComponent<Image>().color = colorMatching[obj];
                    Button b = obj1.GetComponent<Button>();
                    ColorBlock cb = b.colors;
                    cb.highlightedColor = backgroundColor;
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

    public static Color HexStringToColor(string hexName)
    {
        Color tempColor;
        ColorUtility.TryParseHtmlString(hexName, out tempColor);
        return tempColor;
    }
}
