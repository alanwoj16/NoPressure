using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    public Vector3 point;
    public GameObject obj;
    public float speed = 20;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(obj.transform.position, Vector3.forward, speed * Time.deltaTime);
    }
}
