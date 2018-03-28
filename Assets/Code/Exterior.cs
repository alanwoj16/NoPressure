using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exterior : MonoBehaviour {

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public Slider healthBar;
    public bool destroyed;

    public AudioClip hitClip;
    public AudioClip destroyedClip;
    public AudioSource source;

    public Camera main;
    public Color backgroundColor;

    public float wait = 1f;
    public float curr = 0f;
    public float lastChanged;
    public float colorChange = 0.25f;

    // Use this for initialization
    void Start ()
    {
        healthBar = GameObject.Find("ExteriorHealth").GetComponent<Slider>();
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;
        healthBar.value = CalculateHealth();
        destroyed = false;
        main = Camera.main;
    }

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    private void DealDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
        healthBar.value = CalculateHealth();
        if (CurrentHealth <= 0)
            Die();
    }

    void Die()
    {
        CurrentHealth = 0;
        if (!destroyed)
        {
            Debug.Log("You die");
            destroyed = true;
            curr = Time.time;
            source.PlayOneShot(destroyedClip, 2.0f);
            //Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (curr != 0f)
        {
            if (Time.time > curr + wait)
            {
                Destroy(gameObject);
            }
        }

        float time = Time.time;
        if (time < lastChanged + colorChange) { return; }

        main.backgroundColor = backgroundColor;
    }


    internal void OnCollisionEnter2D(Collision2D other)
    {
        var bullet = other.gameObject;
        
        
        
        if (bullet.tag == "Bullet")
        {
            DealDamage(4.0f);
            source.PlayOneShot(hitClip, 0.5f);
            main.backgroundColor = new Color32(101, 91, 91, 0);
            lastChanged = Time.time;
        }
        else if (bullet.tag == "ShotgunBullet")
        {
            DealDamage(5.0f);
            source.PlayOneShot(hitClip, 0.75f);
            main.backgroundColor = new Color32(101, 91, 91, 0);
            lastChanged = Time.time;
        }
        else if (bullet.tag == "SniperBullet")
        {
            DealDamage(25.0f);
            source.PlayOneShot(hitClip, 1.0f);
            main.backgroundColor = new Color32(101, 91, 91, 0);
            lastChanged = Time.time;
        }
    }
}
