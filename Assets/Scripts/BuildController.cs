using UnityEngine;


public class BuildController : MonoBehaviour
{
    public static BuildController Instance { get; private set; }
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private Material canBuildMaterial;
    [SerializeField] private Material cantBuildMaterial;
    [SerializeField] private float scrollSensitivity = 10f;

    private GameObject tempBuildingInstance;
    private Vector3 lastMousePosition;
    private void Awake()
    {
        // If there is no instance already, this becomes the singleton instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionally keep this object alive when changing scenes
        }
        else if (Instance != this)
        {
            // If an instance already exists and it's not this, destroy this instance
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        bool mouseMoved = Input.mousePosition != lastMousePosition;
        if (mouseMoved)
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (tempBuildingInstance != null && CheckCanBuild() && EconomyController.Instance.UseResources(tempBuilding.GetComponent<BuildingCore>().cost))
            {
                PlaceBuilding();
                Destroy(tempBuildingInstance);
                tempBuildingInstance = null;
            }
        }

        if (mouseMoved && tempBuildingInstance != null)
        {
            UpdateTempBuildingPosition();
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (tempBuildingInstance != null && scroll != 0)
        {
            RotateTemporaryBuilding(scroll);
        }

        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)) && tempBuildingInstance != null)
        {
            Destroy(tempBuildingInstance); // Destroy the temporary instance
            tempBuildingInstance = null; // Ensure reference is cleared
            Debug.Log("Building placement cancelled.");
        }
    }

    private string GetLayerUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("TemporaryBuilding");
        layerMask = ~layerMask;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            var layer = hit.collider.gameObject.layer;
            string layerName = LayerMask.LayerToName(layer);
            return layerName;
        }
        return "None";
    }

    private bool CheckCanBuild()
    {
        if (GetLayerUnderMouse() != "Terrain" || !IsAreaClearForBuilding())
        {
            Debug.Log("Cannot build here.");
            return false;
        }

        Debug.Log("Can build here.");
        return true;
    }

    private bool IsAreaClearForBuilding()
    {
        Collider collider = tempBuildingInstance.GetComponent<Collider>();
        if (collider == null)
        {
            Debug.LogError("Temporary building does not have a collider.");
            return false;
        }

        Vector3 center = collider.bounds.center;
        Vector3 halfExtents = collider.bounds.extents;
        Quaternion rotation = tempBuildingInstance.transform.rotation;
        int layerMask = ~LayerMask.GetMask("TemporaryBuilding", "Terrain");

        Collider[] hitColliders = Physics.OverlapBox(center, halfExtents, rotation, layerMask);
        if (hitColliders.Length > 0)
        {
            return false;
        }
        return true;
    }
    private GameObject tempBuilding;
    public void StartPlacingBuilding(GameObject building)
    {

        tempBuilding = building;
        tempBuildingInstance = Instantiate(building, Vector3.zero, Quaternion.identity);
        tempBuildingInstance.layer = LayerMask.NameToLayer("TemporaryBuilding");
        UpdateTempBuildingPosition();

    }

    private void UpdateTempBuildingPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = ~LayerMask.GetMask("TemporaryBuilding");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && tempBuildingInstance != null)
        {
            Collider buildingCollider = tempBuildingInstance.GetComponent<Collider>();
            if (buildingCollider != null)
            {
                float bottomOffset = buildingCollider.bounds.extents.y;
                Vector3 adjustedPosition = hit.point + new Vector3(0, bottomOffset, 0);
                tempBuildingInstance.transform.position = adjustedPosition;
            }
            else
            {
                tempBuildingInstance.transform.position = hit.point;
            }
            UpdateBuildingMaterial(CheckCanBuild());
        }
    }

    private void RotateTemporaryBuilding(float scrollAmount)
    {
        if (tempBuildingInstance != null && scrollAmount != 0)
        {
            tempBuildingInstance.transform.Rotate(Vector3.up, scrollAmount * scrollSensitivity, Space.World);
        }
    }

    private void UpdateBuildingMaterial(bool canBuild)
    {
        Material newMaterial = canBuild ? canBuildMaterial : cantBuildMaterial;
        Renderer[] renderers = tempBuildingInstance.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] mats = new Material[renderer.materials.Length];
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = newMaterial;
            }
            renderer.materials = mats;
        }
    }

    private void PlaceBuilding()
    {
        if (tempBuildingInstance != null)
        {
            Collider buildingCollider = tempBuildingInstance.GetComponent<Collider>();
            if (buildingCollider != null)
            {
                Vector3 placementPosition = tempBuildingInstance.transform.position;
                Quaternion placementRotation = tempBuildingInstance.transform.rotation;
                GameObject buildingInstance = Instantiate(tempBuilding, placementPosition, placementRotation);
            }
            else
            {
                GameObject buildingInstance = Instantiate(tempBuilding, tempBuildingInstance.transform.position, Quaternion.identity);
            }
            Destroy(tempBuildingInstance);
            tempBuildingInstance = null;
        }
    }
}