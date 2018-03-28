using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public Button mainButton;


    void Start()
    {
        Button btn = mainButton.GetComponent<Button>();
        btn.onClick.AddListener(GoToMain);
    }

    void Update()
    {
        
    }

    public void GoToMain()
    {
        Time.timeScale = 1f;
        SoundManager.instance.FullHealth();
        SceneManager.LoadScene("mainMenu");
    }
}

