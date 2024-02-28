using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIResourcesController : MonoBehaviour
{
    public static UIResourcesController Instance { get; private set; }

    [SerializeField] private TMP_Text textIronAmount;
    [SerializeField] private TMP_Text textGoldAmount;
    [SerializeField] private TMP_Text textWoodAmount;
    [SerializeField] private TMP_Text textStoneAmount;

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
    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        textIronAmount.text = $"Iron: {EconomyController.Instance.GetIron()}";
        textGoldAmount.text = $"Gold: {EconomyController.Instance.GetGold()}";
        textWoodAmount.text = $"Wood: {EconomyController.Instance.GetWood()}";
        textStoneAmount.text = $"Stone: {EconomyController.Instance.GetStone()}";
    }



}
