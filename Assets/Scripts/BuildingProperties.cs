using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingCost", menuName = "Building/Cost")]
public class BuildingProperties : ScriptableObject
{
    public int moneyCost;
    public int woodCost;
    public int stoneCost;
    public int ironCost;
}
