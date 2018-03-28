using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleUpgrade : MonoBehaviour
{
    void Start()
    {

        if (this.gameObject.name == "EnergyCell0")
        {
            if (Player.hasEnergyCell[0])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell1")
        {
            if (Player.hasEnergyCell[1])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell2")
        {
            if (Player.hasEnergyCell[2])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell3")
        {
            if (Player.hasEnergyCell[3])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell4")
        {
            if (Player.hasEnergyCell[4])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell5")
        {
            if (Player.hasEnergyCell[5])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell6")
        {
            if (Player.hasEnergyCell[6])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell7")
        {
            if (Player.hasEnergyCell[7])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell8")
        {
            if (Player.hasEnergyCell[8])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell9")
        {
            if (Player.hasEnergyCell[9])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell10")
        {
            if (Player.hasEnergyCell[10])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell11")
        {
            if (Player.hasEnergyCell[11])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell12")
        {
            if (Player.hasEnergyCell[12])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell13")
        {
            if (Player.hasEnergyCell[13])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EnergyCell14")
        {
            if (Player.hasEnergyCell[14])
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "normalGun")
        {
            if (Player.hasNormalGun)
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "phaseGun")
        {
            if (Player.hasShotGun)
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "platformGun")
        {
            if (Player.hasSniper)
            {
                gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "gravityBoots")
        {
            if (Player.hasGravityBoots)
            {
                gameObject.SetActive(false);
            }
        }

    }

}
