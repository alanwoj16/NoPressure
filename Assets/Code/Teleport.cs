using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public Vector3 teleportTo; //set in inspector
    public Player player;
    

    void Start () {
        player = FindObjectOfType<Player>();
    }
	

	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.ChangeTele(teleportTo);
        }
    }
}
