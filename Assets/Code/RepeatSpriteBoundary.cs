using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @NOTE the attached sprite's position should be "top left" or the children will not align properly
// Strech out the image as you need in the sprite render, the following script will auto-correct it when rendered in the game
[RequireComponent(typeof(SpriteRenderer))]

// Generates a nice set of repeated sprites inside a streched sprite renderer
// @NOTE Vertical only, you can easily expand this to horizontal with a little tweaking
public class RepeatSpriteBoundary : MonoBehaviour
{

    SpriteRenderer sprite;
    private Quaternion originalRotation;
    private Quaternion fixedRotation = Quaternion.Euler(0, 0, 0);
    private Color originalColor;


    void Awake()
    {

        // Get the current sprite with an unscaled size
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        originalRotation = transform.rotation;
        transform.rotation = fixedRotation;


        Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);

        if (GetComponentsInChildren<SpriteRenderer>().Length > 1)
        {
            if (GetComponentsInChildren<Teleport>().Length > 0)
            {
                GetComponentsInChildren<SpriteRenderer>()[2].enabled = false;
            }

            else
            {

                //print("doing something");
                GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;

            }

        }




        // Generate a child prefab of the sprite renderer
        GameObject childPrefab = new GameObject();
        SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
        childPrefab.transform.position = transform.position;
        childSprite.sprite = sprite.sprite;
        childPrefab.transform.localScale = new Vector3(childPrefab.transform.localScale.x, 1f, childPrefab.transform.localScale.z);
        //child.transform.localScale = new Vector3(child.transform.localScale.x, 1f, child.transform.localScale.z);

        // Loop through and spit out repeated tiles
        GameObject child;
        for (int i = 1, l = (int)Mathf.Floor(sprite.bounds.size.x); i < l; i++)
        {
            child = Instantiate(childPrefab) as GameObject;
            //print(spriteSize);
            child.transform.position = transform.position + new Vector3(transform.localScale.x / 2, 0, 0) - (new Vector3(spriteSize.x, 0, 0) * i) - new Vector3(spriteSize.x / 2, 0, 0);
            child.GetComponent<SpriteRenderer>().color = originalColor;

            //print(child.transform.position);
            child.transform.parent = transform;
            child.transform.localScale = new Vector3(child.transform.localScale.x, 1f, child.transform.localScale.z);
        }

        //Add tile at the beginning and the end to accomodate, accept some overlap
        child = Instantiate(childPrefab) as GameObject;
        child.transform.position = transform.position - new Vector3(transform.localScale.x / 2, 0, 0) + new Vector3(spriteSize.x / 2, 0, 0);
        child.transform.parent = transform;
        child.transform.localScale = new Vector3(child.transform.localScale.x, 1f, child.transform.localScale.z);
        child.GetComponent<SpriteRenderer>().color = originalColor;

        child = Instantiate(childPrefab) as GameObject;
        child.transform.position = transform.position + new Vector3(transform.localScale.x / 2, 0, 0) - new Vector3(spriteSize.x / 2, 0, 0);
        child.transform.parent = transform;
        child.transform.localScale = new Vector3(child.transform.localScale.x, 1f, child.transform.localScale.z);
        child.GetComponent<SpriteRenderer>().color = originalColor;



        // Set the parent last on the prefab to prevent transform displacement
        childPrefab.transform.parent = transform;

        // Disable the currently existing sprite component since its now a repeated image
        sprite.enabled = false;

        transform.rotation = originalRotation;


    }
}
