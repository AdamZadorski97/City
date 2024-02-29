using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkController : MonoBehaviour
{
    [SerializeField]private List<HumanController> humanControllers = new List<HumanController>();
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GetComponent<BuildingCore>().isBuildingReady);
        GetComponent<BuildingCore>().onDeleteBuilding += RemoveWorkingHumans;
        GetPeopleToWork();
    }

    public void GetPeopleToWork()
    {
        humanControllers = HumanGlobalController.Instance.GetFreeHumans(GetComponent<BuildingCore>().cost.population);
        foreach (var controller in humanControllers)
        {
            controller.GoTo(transform.position);
        }
    }

    public void RemoveWorkingHumans()
    {
        foreach (var controller in humanControllers)
        {
            HumanGlobalController.Instance.AddHuman(controller);
        }
        humanControllers.Clear();
    }
}
