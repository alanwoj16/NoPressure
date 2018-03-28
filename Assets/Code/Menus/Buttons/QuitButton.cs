using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;

public class QuitButton : MonoBehaviour {

    public Button quitButton;

	void Start () {
        Button btn = quitButton.GetComponent<Button>();
        btn.onClick.AddListener(QuitGame);
	}
	

	void Update () {
		
	}

    public void QuitGame()
    {

#if UNITY_EDITOR
        //EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
