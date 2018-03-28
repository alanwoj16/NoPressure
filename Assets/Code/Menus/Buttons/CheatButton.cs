using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheatButton : MonoBehaviour {

    public Button CheatBut;

	void Start () {
        Button btn = CheatBut.GetComponent<Button>();
        btn.onClick.AddListener(StartWithCheats);

    }

    public void StartWithCheats()
    {
        GameObject.FindObjectOfType<Cheat>().cheating = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Area0");
    }

 
}
