using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PopulationController : MonoBehaviour
{
    public TMP_Text textPopulation;
    public static PopulationController Instance { get; private set; }
    private void Awake()
    {
        // If there is no instance already, this becomes the singleton instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionally keep this object alive when changing scenes
        }
        else if (Instance != this)
        {
            // If an instance already exists and it's not this, destroy this instance
            Destroy(gameObject);
        }
    }


    public int CheckCurrentPopulation()
    {
        int tempCurrentPopulation = 0;
        foreach (BuildingCore buildingCore in BuildController.Instance.buildings)
        {
            if (buildingCore.GetComponent<HouseController>() != null)
            {

                tempCurrentPopulation += buildingCore.GetComponent<HouseController>().currentPopulation;
            }
        }
        return tempCurrentPopulation;
    }

    public int CheckUsedPopulation()
    {
        int tempUsedPopulation = 0;
        foreach (BuildingCore buildingCore in BuildController.Instance.buildings)
        {
            tempUsedPopulation += buildingCore.cost.population;
        }
        return tempUsedPopulation;
    }

    public bool CheckPopulationUsage(int value)
    {
        if (CheckCurrentPopulation() - CheckUsedPopulation() - value < 0)
            return false;
        else return true;

    }

    public void UpdatePopulation()
    {
        textPopulation.text = $"Population: {CheckCurrentPopulation()}/{CheckUsedPopulation()}";
    }
}
