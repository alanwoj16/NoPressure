using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowUV : MonoBehaviour
{
    public GameObject Player;
    public Vector3 Offset;

    public MeshRenderer mr;
    //public SpriteRenderer sr;
    public Material mat;
    public float parallax = 100f;
    public Camera main;
	
	void Start ()
	{
	    //main = Camera.main;
	    mr = GetComponent<MeshRenderer>();
	    mat = mr.material;
        //mr.sortingLayerName = "Foreground";

        //sr = GetComponent<SpriteRenderer>();

	    Offset = new Vector3(7, 4, 10);
	    Player = GameObject.Find("Player");
	    main = Camera.main;
	}
	
	// Update is called once per frame
	void Update()
	{
        Vector2 offset = mat.mainTextureOffset;

	    offset.x += Time.deltaTime / (2 * parallax);
	    offset.y += Time.deltaTime / (3 * parallax);
        

        //offset.x = Player.transform.position.x / parallax;
        //offset.y = Player.transform.position.y / parallax;

        mat.mainTextureOffset = offset;

	}

    void LateUpdate()
    {
        

        if (Player != null)
        {
            gameObject.transform.position = Player.transform.position + Offset;
            //star.transform.position = Player.transform.position + Offset2;
        }
    }
}
