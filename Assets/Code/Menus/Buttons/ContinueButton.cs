using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour {

    public Button continueButton;

    void Start () {
        Button btn = continueButton.GetComponent<Button>();
        btn.onClick.AddListener(ContinueGame);
    }
	
	
	void Update () {
		
	}

    public void ContinueGame()
    {
        SceneManager.LoadScene("Area0");
    }
}
