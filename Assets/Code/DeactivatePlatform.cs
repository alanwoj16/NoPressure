using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePlatform : MonoBehaviour
{

    public bool isActive;
    public float lastUsed;
    public float PlatformCooldown;
    public BoxCollider2D platformCollider;
    public SpriteRenderer sprite;
    private Color originalColor;
    private Color transparentColor;

    // Use this for initialization
    void Start()
    {
        isActive = true;
        platformCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;

        transparentColor = originalColor;

        transparentColor.a = 0.3f;
        platformCollider.enabled = true;

        lastUsed = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float time = Time.time;
        if (time < lastUsed + PlatformCooldown) { return; }

        isActive = true;
        platformCollider.enabled = true;
        sprite.color = originalColor;
    }

    internal void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SniperBullet")
        {
            lastUsed = Time.time;
            Destroy(collision.gameObject);
            isActive = false;
            platformCollider.enabled = false;
            sprite.color = transparentColor;
        }
    }
}
