using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCore : MonoBehaviour
{
    public ResourcesScriptable cost;


   public void DeleteBuilding()
    {
        EconomyController.Instance.AddResources(cost);
        BuildController.Instance.buildings.Remove(this);

        Destroy(gameObject);
    }
}