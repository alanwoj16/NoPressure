using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour {

    public static Transform Canvas { get; set; }

    void Start () {
		
	}

    void Awake()
    {
        Canvas = GameObject.Find("MenuCanvas").transform;
    }
	

	void Update () {
		
	}

    public void SpawnMain()
    {
        var menu = (GameObject)Object.Instantiate(Resources.Load("Menus/MainMenuPanel"));
        menu.transform.SetParent(Canvas, false);
    }

    public void SpawnPause()
    {
        var menu = (GameObject)Object.Instantiate(Resources.Load("Menus/PausePanel"));
        menu.transform.SetParent(Canvas, false);
    }

    public void SpawnControl()
    {
        var menu = (GameObject)Object.Instantiate(Resources.Load("Menus/ControlPanel"));
        menu.transform.SetParent(Canvas, false);
    }

    public void SpawnInverted()
    {
        var menu = (GameObject)Object.Instantiate(Resources.Load("Menus/InvertedPanel"));
        menu.transform.SetParent(Canvas, false);
    }

    public void SpawnGameOver()
    {
        var menu = (GameObject)Object.Instantiate(Resources.Load("Menus/GameOverPanel"));
        menu.transform.SetParent(Canvas, false);
    }

    public void SpawnIntro()
    {
        var menu = (GameObject)Object.Instantiate(Resources.Load("Menus/IntroPanel"));
        menu.transform.SetParent(Canvas, false);
    }

    public void SpawnWin()
    {
        var menu = (GameObject)Object.Instantiate(Resources.Load("Menus/WinPanel"));
        menu.transform.SetParent(Canvas, false);
    }
}
