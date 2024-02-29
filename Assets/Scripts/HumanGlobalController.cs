using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HumanGlobalController : MonoBehaviour
{
    public static HumanGlobalController Instance { get; private set; }

    public List<HumanController> freeHuman = new List<HumanController>();

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


    public void AddHuman(HumanController human)
    {
        freeHuman.Add(human);
    }

  public  List<HumanController> GetFreeHumans(int humansToGet = 1)
    {
        List<HumanController> tempFreeHuman = new List<HumanController>();


        for (int i = 0; i < humansToGet; i++)
        {
            tempFreeHuman.Add(freeHuman[i]);
        }
       foreach (HumanController humans in tempFreeHuman)
        {
            freeHuman.Remove(humans);
        }
        return tempFreeHuman;
    }

    public void SetPosition(int humansAmount, Vector3 position)
    {
        // Create a LayerMask that only includes the terrain layer
        LayerMask terrainLayerMask = 1 << 6;
        // Cast a ray downwards from the object's position
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, Mathf.Infinity, terrainLayerMask))
        {
            // If the raycast hits the terrain, use the hit point as the position
            Vector3 position2 = hit.point;
            // Move each free human to the hit position

            List<HumanController> humans = GetFreeHumans(humansAmount);
            foreach (HumanController controller in humans)
            {
                controller.GoTo(position2);
            }
        }
        else
        {
            Debug.Log("No terrain was hit by the raycast.");
        }
    }

}
