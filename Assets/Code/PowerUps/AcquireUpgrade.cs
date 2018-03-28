using System.Collections;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;

public class AcquireUpgrade : MonoBehaviour {

    public static Player player;
    public static Tutorial tutorial;
    public string _tag;
    public string _name;
    public bool keepAlive = false;

    public AudioClip upgrade;


    void Start()
    {
        player = FindObjectOfType<Player>();
        tutorial = FindObjectOfType<Tutorial>();
        _tag = gameObject.tag;
        if (_tag == "EnergyCell")
        {
            _name = gameObject.name;
        }

        upgrade = (AudioClip) Resources.Load("upgrade");
    }


    void Update()
    {

    }

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.instance.PlaySingle(upgrade);
            Acquire();
            if (!keepAlive)
            {
                gameObject.SetActive(false);
            }
        }
    }
    
    private void Acquire()
    {
        switch (_tag)
        {
            case "NormalGun":
                player.AcquirePowerUp(1);
                tutorial.AcquirePowerUp(1);
                break;
            case "Rocket":
                player.AcquirePowerUp(2);
                tutorial.AcquirePowerUp(2);
                break;
            case "Shotgun":
                player.AcquirePowerUp(3);
                tutorial.AcquirePowerUp(3);
                break;
            case "Sniper":
                player.AcquirePowerUp(4);
                tutorial.AcquirePowerUp(4);
                break;
            case "Boots":
                player.AcquirePowerUp(5);
                tutorial.AcquirePowerUp(5);
                break;
            case "Oxygen":
                player.AcquirePowerUp(6);
                tutorial.AcquirePowerUp(6);
                break;
            case "EnergyCell":
                tutorial.AcquirePowerUp(7);
                AcquireCell();
                break;
            default:
                break;
        }
    }

    private void AcquireCell()
    {
        switch (_name)
        {
            case "EnergyCell0":
                Player.hasEnergyCell[0] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell1":
                Player.hasEnergyCell[1] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell2":
                Player.hasEnergyCell[2] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell3":
                Player.hasEnergyCell[3] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell4":
                Player.hasEnergyCell[4] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell5":
                Player.hasEnergyCell[5] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell6":
                Player.hasEnergyCell[6] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell7":
                Player.hasEnergyCell[7] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell8":
                Player.hasEnergyCell[8] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell9":
                Player.hasEnergyCell[9] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell10":
                Player.hasEnergyCell[10] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell11":
                Player.hasEnergyCell[11] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell12":
                Player.hasEnergyCell[12] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell13":
                Player.hasEnergyCell[13] = true;
                player.SetEnergyText();
                break;
            case "EnergyCell14":
                Player.hasEnergyCell[14] = true;
                player.SetEnergyText();
                break;
            default:
                break;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
