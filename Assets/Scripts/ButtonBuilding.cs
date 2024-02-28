using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBuilding : MonoBehaviour
{
    public GameObject buildingPrefab;

    public void OnClick()
    {
        BuildingCore buildingCore = buildingPrefab.GetComponent<BuildingCore>();

        if (EconomyController.Instance.HasEnoughResources(buildingCore.cost))
        {
            BuildController.Instance.StartPlacingBuilding(buildingPrefab);
        }
    }
}
