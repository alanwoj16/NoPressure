using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

    public Button restartButton;
    public string sceneName;

    void Start()
    {
        Button btn = restartButton.GetComponent<Button>();
        btn.onClick.AddListener(RestartGame);
        Scene _scene = SceneManager.GetActiveScene();
        sceneName = _scene.name;
    }


    void Update()
    {

    }

    public void RestartGame()
    {
        switch (sceneName)
        {
            case "Area0":
                Time.timeScale = 1f;
                SceneManager.LoadScene("Area0");
                break;
            case "Hub":
                Time.timeScale = 1f;
                SceneManager.LoadScene("Hub");
                break;
            case "Area1":
                Time.timeScale = 1f;
                SceneManager.LoadScene("Area1");
                break;
            case "Area2":
                Time.timeScale = 1f;
                SceneManager.LoadScene("Area2");
                break;
            case "Area3":
                Time.timeScale = 1f;
                SceneManager.LoadScene("Area3");
                break;
            case "Area4":
                Time.timeScale = 1f;
                SceneManager.LoadScene("Area4");
                break;
            default:
                break;
        }
    }
}
