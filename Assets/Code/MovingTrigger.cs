using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrigger : MonoBehaviour {

    private GameObject target = null;
    private Vector3 offset;
    private MovingPlatform attachedPlat;
    void Start()
    {
        attachedPlat = GetComponentInParent<MovingPlatform>();
        target = null;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            target = col.gameObject;
            offset = target.transform.position - transform.position;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            target = null;
        }
    }

    
    void LateUpdate()
    {
        

        
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
        
        
    }
    

    
    private void Update()
    {
        /*
        if (attachedPlat.horizontal != null && attachedPlat.horizontal)
        {
            if (target != null)
            {
                target.transform.position = transform.position + offset;
            }
        }
        */
        
        
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
        
    }
    
}
