using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public Button startButton;
    public UIManager UIM;

    void Start()
    {
        Button btn = startButton.GetComponent<Button>();
        UIM = GameObject.FindObjectOfType<UIManager>();
        btn.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        UIM.SpawnIntro();
        GameObject.FindObjectOfType<MainMenu>().Die();
        
    }
}

