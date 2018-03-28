using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Escape : MonoBehaviour {

    private Text _countText;
    public Player player;
    public int cells;
    public UIManager UIM;
    public Text tutorial;
    public int winAmount = 12;
    public SpriteRenderer main;
    public SpriteRenderer mainEnd;
    public SpriteRenderer left;
    public SpriteRenderer leftEnd;
    public SpriteRenderer right;
    public SpriteRenderer rightEnd;
    public Animator img;

    void Start () {
        _countText = GameObject.Find("Count Text").GetComponent<Text>();
        _countText.gameObject.SetActive(false);
        player = GameObject.FindObjectOfType<Player>();
        tutorial = GameObject.Find("Tutorial Text").GetComponent<Text>();

        main.enabled = false;
        mainEnd.enabled = false;
        left.enabled = false;
        leftEnd.enabled = false;
        right.enabled = false;
        rightEnd.enabled = false;

    }
	
	void Update () {
		
	}

    internal void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            cells = player.CountCells();
            if (cells >= winAmount)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
                gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                collision.gameObject.SetActive(false);
                main.enabled = true;
                mainEnd.enabled = true;
                left.enabled = true;
                leftEnd.enabled = true;
                right.enabled = true;
                rightEnd.enabled = true;
                StartCoroutine(Win());
            }
            else
            {
                StartCoroutine(ShowText());
            }
        }
    }

    IEnumerator ShowText()
    {
        _countText.gameObject.SetActive(true);
        if ((12 - cells) > 1)
        {
            _countText.text = "You need " + (12 - cells).ToString() + " more cells to escape!";
        }
        else
        {
            _countText.text = "You need " + (12 - cells).ToString() + " more cell to escape!";
        }
        
        yield return new WaitForSeconds(2f);
        _countText.gameObject.SetActive(false);
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(3f);
        tutorial.enabled = false;
        img.Play("FadeOut");
        yield return new WaitForSeconds(.5f);
        player.TogglePause();
        UIM.SpawnWin();
    }
}
