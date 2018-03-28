using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOnDestroy : MonoBehaviour {

    public RocketBoostFall RBF;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        RBF.Move = true;
    }
}
