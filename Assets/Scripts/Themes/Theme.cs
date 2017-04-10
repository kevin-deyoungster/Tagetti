using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Theme : MonoBehaviour {

    public string themeName;
    public Color backgroundColor;
    public Color accentColor;
    public Color fontColor;
    public Color leader;

    public Color mild;

    public float price;
    public bool hasBeenBought = false;
    public bool equipped = false;

    private void Start()
    {
        SetName(themeName);
        SetInfo(price.ToString());
        SetColor();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { Buy(); });
    }
    
    void SetName(string theName)
    {
        transform.Find("Name").GetComponent<Text>().text = theName;
    }

    void SetInfo(string theInfo)
    {
        transform.Find("Info").GetComponent<Text>().text = theInfo;
    }

    void SetColor()
    {
        GetComponent<Image>().color = backgroundColor;
    }

    public void Buy()
    {
        FindObjectOfType<ColorManager>().SetTheme(themeName);
        if (hasBeenBought)
        {
            Equip();
        }
        else
        {
            //Buy first
            print("Buying");
            Equip();
            hasBeenBought = true;
        }
    }

    public void Equip()
    {
        SetInfo("Unequip");
    }

    public void UnEquip()
    {
        SetInfo("Equip");
    }
     
}
