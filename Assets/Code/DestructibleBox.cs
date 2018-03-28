using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBox : MonoBehaviour {

    private int hp = 100;
    public bool NormalBox;
    public bool ShotgunBox;
    public bool SniperBox;
    public bool OrangeBox;
    public SpriteRenderer sprite;
    public Color boxColor;

    public float lastChanged;
    public float colorChange = 0.25f;

    // Use this for initialization
    void Start () {

        sprite = GetComponent<SpriteRenderer>();
        boxColor = sprite.color;
     
	}
	
	// Update is called once per frame
	void Update () {
		
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }

	}

    private void FixedUpdate()
    {
        float time = Time.time;
        if (time < lastChanged + colorChange) { return; }

        sprite.color = boxColor;

    }


    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ShotgunBullet" && ShotgunBox){

            sprite.color = Color.red;

            lastChanged = Time.time;

            hp = hp - 15;

            collision.gameObject.GetComponent<Bullet>().Die();

        }

        if (collision.gameObject.tag == "ShotgunBullet" & OrangeBox)
        {
            collision.gameObject.GetComponent<Bullet>().Die();
        }

    }


    internal void OnCollisionEnter2D(Collision2D other)
    {


        if (other.gameObject.tag == "Bullet" && NormalBox)
        {
            sprite.color = Color.red;

            lastChanged = Time.time;

            hp = hp - 25;
        }

        if (other.gameObject.tag == "ShotgunBullet" && ShotgunBox)
        {
            sprite.color = Color.red;

            lastChanged = Time.time;

            hp = hp - 15;
        }

        if (other.gameObject.tag == "SniperBullet" && SniperBox)
        {
            sprite.color = Color.red;

            lastChanged = Time.time;

            hp = hp - 50;
        }


    }


}
