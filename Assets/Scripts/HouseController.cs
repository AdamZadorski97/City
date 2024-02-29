using System.Collections;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    public int currentPopulation;
    public int maxPopulation = 20;
    public GameObject human;
    public float spawnRadius = 1f; // Radius around the house to spawn humans

    void Start()
    {
        StartCoroutine(IncreasePopulationOverTime());
    }

    IEnumerator IncreasePopulationOverTime()
    {
        while (currentPopulation < maxPopulation && GetComponent<BuildingCore>().isBuildingReady)
        {
            yield return new WaitForSeconds(0.1f); // Wait for 1 second
            SpawnHumanInCircle();
            currentPopulation++; // Increase the population by 1
            PopulationController.Instance.UpdatePopulation();
        
        }
    }

    void SpawnHumanInCircle()
    {
        // Calculate a random angle to spawn the human
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        // Calculate the x and y position based on the angle and spawn radius
        float x = Mathf.Cos(angle) * spawnRadius;
        float y = Mathf.Sin(angle) * spawnRadius;
        // Create a new position vector, offsetting by the house's position
        Vector3 spawnPosition = new Vector3(x, 0, y) + transform.position; // Assuming human objects move along the XZ plane, adjust if necessary
        // Instantiate the human object at the calculated position
      GameObject humanInstance =  Instantiate(human, spawnPosition, Quaternion.identity);
        HumanGlobalController.Instance.freeHuman.Add(humanInstance.GetComponent<HumanController>());
    }
}