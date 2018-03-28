using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginalButton : MonoBehaviour
{

    public Button originalButton;
    public UIManager UIM;


    void Start()
    {
        Button btn = originalButton.GetComponent<Button>();
        UIM = FindObjectOfType<UIManager>();
        btn.onClick.AddListener(GoToOriginal);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToOriginal()
    {
        UIM.SpawnControl();
        GameObject.FindObjectOfType<InvertedMenu>().Die();
    }
}
