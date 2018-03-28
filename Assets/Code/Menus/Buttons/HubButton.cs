using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HubButton : MonoBehaviour {

    public Button Hub;


    void Start()
    {
        Button btn = Hub.GetComponent<Button>();
        btn.onClick.AddListener(GoToHub);
    }

    void Update()
    {

    }

    public void GoToHub()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Hub");
    }
}
