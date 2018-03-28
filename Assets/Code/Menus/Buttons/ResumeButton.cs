using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{

    public Button resumeButton;


    void Start()
    {
        Button btn = resumeButton.GetComponent<Button>();
        btn.onClick.AddListener(ResumeGame);
    }


    void Update()
    {

    }

    public void ResumeGame()
    {
        GameObject.FindObjectOfType<Player>().TogglePause();
        GameObject.FindObjectOfType<PauseMenu>().Die();
    }
}

