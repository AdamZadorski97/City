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


    public int CheckPopulation()
    {
        int tempPopulation = 0;
        foreach (BuildingCore buildingCore in BuildController.Instance.buildings)
        {
            if (buildingCore.GetComponent<HouseController>() != null)
            {

                tempPopulation += buildingCore.GetComponent<HouseController>().currentPopulation;
                Debug.Log(tempPopulation);
            }
        }
        return tempPopulation;
    }

    public void UpdatePopulation()
    {
        textPopulation.text = $"Population: {CheckPopulation()}";
    }
}
