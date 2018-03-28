using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3 startPos;
    public Vector3 finishPos;
    //public Vector3 yfinishPos;
    public bool vertical;
    public bool horizontal;
    public float moveAmount;
    public float speed;

	// Use this for initialization
	void Start () {

        startPos = transform.position;

        if (horizontal)
        {
            finishPos = new Vector3(startPos.x + moveAmount, startPos.y, startPos.z);
        }

        if (vertical)
        {
            finishPos = new Vector3(startPos.x, startPos.y + moveAmount, startPos.z);
        }
        
        
	}
	
	// Update is called once per frame
	void Update () {

        if (horizontal)
        {
            transform.position = new Vector3(Mathf.PingPong(Time.time * speed, finishPos.x - startPos.x) + startPos.x, transform.position.y, transform.position.z);
        }

        if (vertical)
        {
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speed, finishPos.y - startPos.y) + startPos.y, transform.position.z);
        }
        

    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.GetComponent<Teleport>() &&  other.transform.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!gameObject.GetComponent<Teleport>() && other.transform.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
    
    

}
