using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoBackButton : MonoBehaviour {

    public Button goBack;
    public UIManager UIM;
    public string sceneName;

    void Start () {

        Button btn = goBack.GetComponent<Button>();
        UIM = FindObjectOfType<UIManager>();
        btn.onClick.AddListener(GoBack);
        sceneName = SceneManager.GetActiveScene().name;
    }

    
	void Update (){

    }

    public void GoBack()
    {
        if (sceneName == "mainMenu")
        {
            UIM.SpawnMain();
            if (GameObject.FindObjectOfType<ControlsMenu>())
            {
                GameObject.FindObjectOfType<ControlsMenu>().Die();
            }
            else
            {
                GameObject.FindObjectOfType<InvertedMenu>().Die();
            }
            
        }
        else
        {
            UIM.SpawnPause();
            if (GameObject.FindObjectOfType<ControlsMenu>())
            {
                GameObject.FindObjectOfType<ControlsMenu>().Die();
            }
            else
            {
                GameObject.FindObjectOfType<InvertedMenu>().Die();
            }
        }
    }
}
