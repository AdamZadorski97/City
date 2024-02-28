using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    public int currentPopulation;
    public int maxPopulation = 20;
    // Start is called before the first frame update
    void Start()
    {
        // Start the population increase coroutine when the game starts
        StartCoroutine(IncreasePopulationOverTime());
    }

    IEnumerator IncreasePopulationOverTime()
    {
        while (currentPopulation < maxPopulation) // Infinite loop to keep the coroutine running
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            currentPopulation++; // Increase the population by 1
            PopulationController.Instance.UpdatePopulation();
        }
    }
}