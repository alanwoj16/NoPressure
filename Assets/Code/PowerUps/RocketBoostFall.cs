using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBoostFall : MonoBehaviour {

    public bool Move = false;
    public float speed = 20f;


    void Start () {
		
	}
	

	void Update () {
		if (Move)
        {
            float step = speed * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(-12.04f,-.83f,0f), step);
        }
	}
}
