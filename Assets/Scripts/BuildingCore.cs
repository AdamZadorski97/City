using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCore : MonoBehaviour
{
    public ResourcesScriptable cost;
    public bool isBuildingReady = false;
    public Action onDeleteBuilding;
  

    public void PlaceBuilding()
    {
        isBuildingReady = true;
    }

   public void DeleteBuilding()
    {
        onDeleteBuilding();
        EconomyController.Instance.AddResources(cost);
        BuildController.Instance.buildings.Remove(this);
        PopulationController.Instance.UpdatePopulation();
        Destroy(gameObject);
    }
}