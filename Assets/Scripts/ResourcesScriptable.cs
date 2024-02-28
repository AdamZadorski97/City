using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
[CreateAssetMenu(fileName = "NewResourcesScriptable", menuName = "Resources")]
public class ResourcesScriptable : ScriptableObject
{
    public int gold;
    public int wood;
    public int stone;
    public int iron;
    public int population;
}
