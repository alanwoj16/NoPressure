using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager {

    private readonly Transform _holder;

    /// <summary>
    /// Bullet prefab. Use GameObject.Instantiate with this to make a new bullet.
    /// </summary>
    private readonly Object _bullet;
    private readonly Object _shotgunBullet;
    private readonly Object _sniperBullet;

    public BulletManager(Transform holder)
    {
        _holder = holder;
        _bullet = Resources.Load("Bullet");
        _shotgunBullet = Resources.Load("Bullet 1");
        _sniperBullet = Resources.Load("Bullet 2");
    }

    
    public void ForceSpawn(Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime)
    {

        var newBullet = (GameObject)Object.Instantiate(_bullet, pos, rotation, _holder);

        Bullet temp = newBullet.GetComponent<Bullet>();

        temp.Initialize(velocity, deathtime);
    }

    
    public void ForceSpawnShotgun(Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime)
    {

        var newBullet = (GameObject)Object.Instantiate(_shotgunBullet, pos, rotation, _holder);

        Bullet temp = newBullet.GetComponent<Bullet>();

        temp.Initialize(velocity, deathtime);
    }


    
    public void ForceSpawnSniper(Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime)
    {

        var newBullet = (GameObject)Object.Instantiate(_sniperBullet, pos, rotation, _holder);

        Bullet temp = newBullet.GetComponent<Bullet>();

        temp.Initialize(velocity, deathtime);
    }



}
