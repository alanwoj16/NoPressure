using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour {

    public bool isActive;
    public float lastUsed;
    public float PlatformCooldown;
    public BoxCollider2D platformCollider;
    public SpriteRenderer sprite;
    private Color originalColor;
    private Color transparentColor;

	// Use this for initialization
	void Start () {
        isActive = false;
        platformCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;

        transparentColor = originalColor;

        transparentColor.a = 0.3f;

        sprite.color = transparentColor;

        platformCollider.isTrigger = true;

	    lastUsed = 0f;

        gameObject.layer = 10;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float time = Time.time;
        if (time < lastUsed + PlatformCooldown) { return; }

        isActive = false;

        sprite.color = transparentColor;

        platformCollider.isTrigger = true;

        gameObject.layer = 10;


		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SniperBullet")
        {
            lastUsed = Time.time;

            isActive = true;

            gameObject.layer = 8;

            sprite.color = originalColor;

            platformCollider.isTrigger = false;
        }
        
    }


    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "SniperBullet"){

            isActive = true;

            sprite.color = originalColor;
        }

    }
    */
}
