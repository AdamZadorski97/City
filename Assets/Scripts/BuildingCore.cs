using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCore : MonoBehaviour
{
    public ResourcesScriptable cost;


    public bool CheckCanBuy()
    {
        if (EconomyController.Instance.HasEnoughResources(cost))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void Start()
    {
   
    }
}