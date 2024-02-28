using Unity.VisualScripting;
using UnityEngine;

public class UIBuilding : MonoBehaviour
{
    public RectTransform uiElement;
    public Transform worldObjectToFollow;
    public Camera cameraToUse;
    public Vector2 offset;
    public LayerMask buildingLayer; // Define the layer mask for buildings
    private BuildingCore tempBuildingCore;
    private void Awake()
    {
        if (cameraToUse == null)
        {
            cameraToUse = Camera.main;
        }
        uiElement.gameObject.SetActive(false); // Initially hide the UI element
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Ray ray = cameraToUse.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingLayer))
            {
                // If raycast hits an object on the building layer
                SetTarget(hit.transform); // Set this object as the target to follow
                uiElement.gameObject.SetActive(true); // Show the UI element
                tempBuildingCore = hit.transform.gameObject.GetComponent<BuildingCore>();
            }
            else
            {
                // If raycast does not hit a building
                Invoke("HideCanvas", 0.3f);
            }
        }

        if (worldObjectToFollow != null)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cameraToUse, worldObjectToFollow.position);
            uiElement.position = screenPoint + offset;
        }
    }
    public void HideCanvas()
    {
        tempBuildingCore = null;
        uiElement.gameObject.SetActive(false);
    }

    public void SetTarget(Transform target)
    {
        worldObjectToFollow = target;
    }

    public void DeleteBuilding()
    {
        if (tempBuildingCore != null)
        {
            tempBuildingCore.GetComponent<BuildingCore>().DeleteBuilding();
            uiElement.gameObject.SetActive(false);
        }
    }

    public void MoveBuilding()
    {
        if (tempBuildingCore != null)
        {
            BuildController.Instance.MoveExistingBuilding(tempBuildingCore.gameObject);
            uiElement.gameObject.SetActive(false);
        }
    }
}