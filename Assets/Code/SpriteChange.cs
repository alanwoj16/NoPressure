using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChange : MonoBehaviour {

    public Sprite green;
    public Sprite red;
    public Sprite yellow;
    public Sprite purple;
    public Sprite blue;
    public Image image;
    public int color;

	void Start () {

        image = gameObject.GetComponent<Image>();
        image.color = new Color32(35,216,81,255);
        image.sprite = green;
        color = 0;
        InvokeRepeating("CycleSprites", 1f, 1f);
        
	}

    void CycleSprites()
    {
        switch(color)
        {
            case 0:
                image.sprite = red;
                image.color = new Color32(255,0,0,255);
                color = 1;
                break;
            case 1:
                image.sprite = yellow;
                image.color = new Color32(241,255,0,255);
                color = 2;
                break;
            case 2:
                image.sprite = purple;
                image.color = new Color32(248,33,255,255);
                color = 3;
                break;
            case 3:
                image.sprite = blue;
                image.color = new Color32(0,33,255,255);
                color = 4;
                break;
            case 4:
                image.sprite = green;
                image.color = new Color32(35,216,81,255);
                color = 0;
                break;
            default:
                break;

        }
    }

}
