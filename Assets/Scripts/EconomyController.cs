using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyController : MonoBehaviour
{
    // Singleton instance
    public static EconomyController Instance { get; private set; }

    [SerializeField] private int currentGold;
    [SerializeField] private int currentWood;
    [SerializeField] private int currentStone;
    [SerializeField] private int currentIron;


    private void Awake()
    {
        // Ensure there is only one instance of this object in the game
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep this object alive when loading new scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Set Methods
    public void SetGold(int money) => currentGold = money;
    public void SetWood(int wood) => currentWood = wood;
    public void SetStone(int stone) => currentStone = stone;
    public void SetIron(int iron) => currentIron = iron;

    // Get Methods
    public int GetGold() => currentGold;
    public int GetWood() => currentWood;
    public int GetStone() => currentStone;
    public int GetIron() => currentIron;

    // Check if there are enough resources
    public bool HasEnoughResources(ResourcesScriptable cost)
    {
        return currentGold >= cost.gold && currentWood >= cost.wood && currentStone >= cost.stone && currentIron >= cost.iron;
    }

    // Add resources
    public void AddResources(int gold, int wood, int stone, int iron)
    {
        currentGold += gold;
        currentWood += wood;
        currentStone += stone;
        currentIron += iron;
        UIResourcesController.Instance.UpdateUI();
    }

    // Use resources
    public bool UseResources(ResourcesScriptable cost)
    {
        if (!HasEnoughResources(cost))
        {
            return false; // Not enough resources
        }

        currentGold -= cost.gold;
        currentWood -= cost.wood;
        currentStone -= cost.stone;
        currentIron -= cost.iron;
        UIResourcesController.Instance.UpdateUI();
        return true; // Resources used successfully
    }
}