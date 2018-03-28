using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour {


    public Image white;
    public Animator anim;
    private string scene;
    public AudioClip portal;

	// Use this for initialization
	void Start () {
        scene = gameObject.tag;
	    portal = (AudioClip) Resources.Load("portal");
	}
	
	// Update is called once per frame
	void Update () {

    }

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Progress();
            SoundManager.instance.PlaySingle(portal);
        }
    }

    public void Progress()
    {
        switch(scene)
        { 
            case "Hub":
                StartCoroutine(Fading());
                //SceneManager.LoadScene("Level-Hub");
                break;
            case "Level1":
                StartCoroutine(Fading2());
                //SceneManager.LoadScene("Level1-Green");
                break;
            case "Level2":
                StartCoroutine(Fading3());
                //SceneManager.LoadScene("Level2-Rocket");
                break;
            case "Level3":
                StartCoroutine(Fading4());
                //SceneManager.LoadScene("Level3-Yellow");
                break;
            case "Level4":
                StartCoroutine(Fading5());
                break;
            default:
                break;
        }
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("Hub");
    }
    IEnumerator Fading2()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("Area1");
    }
    IEnumerator Fading3()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("Area2");
    }
    IEnumerator Fading4()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("Area3");
    }
    IEnumerator Fading5()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("Area4");
    }
}
