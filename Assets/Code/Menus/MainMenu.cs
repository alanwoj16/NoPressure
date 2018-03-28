using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Die()
    {
        GameObject.Destroy(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            GameObject.FindObjectOfType<Cheat>().cheating = false;
        }
    }
}
