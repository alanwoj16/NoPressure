using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour {

    public SpriteRenderer sprite;
    private Color originalColor;
    private Color transparentColor;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        transparentColor = originalColor;
        transparentColor.a = 0.3f;
        sprite.color = transparentColor;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player" && !Player.isCheating)
        {
            Player._gravityCoolDown = 0f;
            Player.infiniteGrav = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player" && !Player.isCheating)
        {
            Player._gravityCoolDown = 5f;
            Player.infiniteGrav = false;
        }
    }
}
