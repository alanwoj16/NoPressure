using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvertedButton : MonoBehaviour {

    public Button invertButton;
    public UIManager UIM;


    void Start()
    {
        Button btn = invertButton.GetComponent<Button>();
        UIM = FindObjectOfType<UIManager>();
        btn.onClick.AddListener(GoToInvert);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void GoToInvert()
    {
        UIM.SpawnInverted();
        GameObject.FindObjectOfType<ControlsMenu>().Die();
    }
}
