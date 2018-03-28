using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour {

    public Player player;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MovePlayer(Vector3 location)
    {
        player.gameObject.SetActive(false);
        StartCoroutine(TeleportWait(location));


    }

    IEnumerator TeleportWait(Vector3 tele)
    {
        yield return new WaitForSeconds(.3f);
        player.transform.position = tele;
        player.gameObject.SetActive(true);
    }

}
