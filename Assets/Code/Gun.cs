using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    private static float FireCooldown = 0.25f;
    private float _lastfire;
    private int shoot_right = 1;
    private int shoot_up = 0;
    private Player p;

    public AudioClip atomizer;
    public AudioClip phaser;
    public AudioClip ionizer;
   
    private static int currentWeaponIndex = 0;

    private SpriteRenderer sprite;
    //private Color originalColor;
    private static bool firstColorChange = false;

    private int gravityFlipped = 1;

    private Quaternion spriteRotation;

    /* old syntax
    private bool weapon1 = true;
    private bool weapon2 = false;
    private bool weapon3 = false;
    */

    public void Start()
    {
        atomizer = (AudioClip)Resources.Load("atomizer");
        phaser = (AudioClip)Resources.Load("phaser");
        ionizer = (AudioClip)Resources.Load("ionizer");

        p = FindObjectOfType<Player>();

        sprite = p.GetComponent<SpriteRenderer>();

        if ((currentWeaponIndex == 0) && Player.hasNormalGun)
        {
            sprite.color = new Color32(123, 233, 151, 255);
            //originalColor = sprite.color;
        }
        else if (currentWeaponIndex == 1)
        {
            sprite.color = new Color32(241, 255, 0, 255);
        }
        else if (currentWeaponIndex == 2)
        {
            sprite.color = new Color32(102, 119, 238, 255);
        }
        
    }

    public void HandleDirectionalShooting(KeyCode x)
    {

        if (x == KeyCode.J)
        {

            spriteRotation = Quaternion.Euler(0, 0, 90f);


            if (p.wasInverted)
            {
                shoot_right = 1;
                shoot_up = 0;
            }

            else
            {
                shoot_right = -1;
                shoot_up = 0;
            }



            //shoot_right = -1;
            //shoot_up = 0;


        }

        else if (x == KeyCode.L)
        {

            spriteRotation = Quaternion.Euler(0, 0, -90f);

            if (p.wasInverted)
            {
                shoot_right = -1;
                shoot_up = 0;
            }

            else
            {
                shoot_right = 1;
                shoot_up = 0;
            }



            //shoot_right = 1;
            //shoot_up = 0;


        }

        else if (x == KeyCode.I)
        {

            spriteRotation = transform.rotation;

            shoot_up = 1;
            shoot_right = 0;

        }

        else if (x == KeyCode.K)
        {
            spriteRotation = Quaternion.Euler(0, 0, 180f);

            shoot_up = -1;
            shoot_right = 0;

        }
    }


    public void Update()
    {
        //HandleDirectionalShooting(); old syntax
        if (!firstColorChange && Player.hasNormalGun)
        {
            
            sprite.color = new Color32(123, 233, 151, 255);
            firstColorChange = true;
            //originalColor = sprite.color;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Player.hasShotGun)
            {
                if (currentWeaponIndex == 0)
                {
                    sprite.color = new Color32(241, 255, 0, 255);

                    currentWeaponIndex++;
                    FireCooldown = 0.1f;
                }

                else if (Player.hasSniper)
                {
                    if (currentWeaponIndex == 1)
                    {

                        //sprite.color = new Color32(0, 33, 255, 255);
                        sprite.color = new Color32(102, 119, 238, 255);

                        currentWeaponIndex++;
                        FireCooldown = 1f;
                    }

                    else if (currentWeaponIndex == 2)
                    {
                        sprite.color = new Color32(123, 233, 151, 255);

                        currentWeaponIndex = 0;
                        FireCooldown = 0.25f;
                    }
                }

                else if (currentWeaponIndex == 1)
                {
                    sprite.color = new Color32(123, 233, 151, 255);

                    currentWeaponIndex = 0;
                    FireCooldown = 0.25f;
                }
            }
        }
    }


    public void Fire(KeyCode x)
    {
        HandleDirectionalShooting(x);

        float time = Time.time;
        if (time < _lastfire + FireCooldown) { return; }

        _lastfire = time;

        if (p.wasInverted)
        {
            gravityFlipped = -1;
        }

        else
        {
            gravityFlipped = 1;
        }

        if (Player.hasShotGun && (currentWeaponIndex == 1))
        {
            Game.Bullets.ForceSpawnShotgun(
                transform.position + (transform.right * 0.7f * shoot_right) + (transform.up * shoot_up * 1.3f) * gravityFlipped,
                spriteRotation,
                shoot_right * transform.right * 40f + shoot_up * transform.up * 40f * gravityFlipped,
                time + Bullet.Lifetime);

            SoundManager.instance.PlaySingle(phaser);

        }


        else if (Player.hasSniper && (currentWeaponIndex == 2))
        {
            Game.Bullets.ForceSpawnSniper(
                transform.position + (transform.right * 0.7f * shoot_right) + transform.up * shoot_up * 1.3f * gravityFlipped,
                    spriteRotation,
                    shoot_right * transform.right * 50f + shoot_up * transform.up * 50f * gravityFlipped,
                    time + Bullet.Lifetime);

            SoundManager.instance.PlaySingle(ionizer);
        }

        else if (Player.hasNormalGun && (currentWeaponIndex == 0))
        {
            Game.Bullets.ForceSpawn(
                transform.position + (transform.right * 0.7f * shoot_right) + transform.up * shoot_up * 1.3f * gravityFlipped,
                spriteRotation,
                shoot_right * transform.right * 40f + shoot_up * transform.up * 40f * gravityFlipped,
                time + Bullet.Lifetime);

            SoundManager.instance.PlaySingle(atomizer);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            FireCooldown = 0.25f;
            currentWeaponIndex = 0;
            firstColorChange = false;
        }
    }
}
