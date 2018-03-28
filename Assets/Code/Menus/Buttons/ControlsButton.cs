using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlsButton : MonoBehaviour {

    public Button controlButton;
    public UIManager UIM;
    public string sceneName;


    void Start () {
        Button btn = controlButton.GetComponent<Button>();
        UIM = FindObjectOfType<UIManager>();
        btn.onClick.AddListener(ControlMenu);
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Update () {
        
    }

    public void ControlMenu()
    {
        if (sceneName == "mainMenu")
        {
            UIM.SpawnControl();
            GameObject.FindObjectOfType<MainMenu>().Die();
        }
        else
        {
            UIM.SpawnControl();
            GameObject.FindObjectOfType<PauseMenu>().Die();
        }
    }
}
