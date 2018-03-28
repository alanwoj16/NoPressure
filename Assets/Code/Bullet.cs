using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public const float Lifetime = 2f; // bullets last this long
    private float _deathtime;
    public GameObject explosionPrefab;

    public DestructibleBox collidingObject;

    public void Initialize(Vector2 velocity, float deathtime)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        _deathtime = deathtime;
    }


    internal void Update()
    {
        if (Time.time > _deathtime) { Die(); }
    }

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        ActivatePlatform platform = collision.gameObject.GetComponent<ActivatePlatform>();

        if (platform != null){

            if(gameObject.tag == "SniperBullet"){
                Die();
            }

        }


    }

    internal void OnCollisionEnter2D(Collision2D other)
    {

        if(other.gameObject != Game.overallPlayer.gameObject)
        {

            collidingObject = other.gameObject.GetComponent<DestructibleBox>();

            if(collidingObject != null){

                if (gameObject.tag == "ShotgunBullet" && collidingObject.NormalBox)
                {
                    //Physics2D.IgnoreCollision(other.gameObject.GetComponent<BoxCollider2D>(), gameObject.GetComponent<CircleCollider2D>());
                    //return;
                }
                
            }


            Die();
        }

    }


    public void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
