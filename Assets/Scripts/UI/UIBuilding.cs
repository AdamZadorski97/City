using Unity.VisualScripting;
using UnityEngine;

public class UIBuilding : MonoBehaviour
{
    public RectTransform uiElement;
    public Transform worldObjectToFollow;
    public Camera cameraToUse;
    public Vector2 offset;
    public LayerMask buildingLayer;
    private BuildingCore tempBuildingCore;
    private void Awake()
    {
        if (cameraToUse == null)
        {
            cameraToUse = Camera.main;
        }
        uiElement.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraToUse.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingLayer))
            {
                SetTarget(hit.transform);
                uiElement.gameObject.SetActive(true);
                tempBuildingCore = hit.transform.gameObject.GetComponent<BuildingCore>();
            }
            else
            {
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