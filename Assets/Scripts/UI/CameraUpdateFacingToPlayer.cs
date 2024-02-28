using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpdateFacingToPlayer : MonoBehaviour
{
    void Start()
    {
        // Ensure there is a parent object with a mesh renderer
        if (transform.parent != null && transform.parent.GetComponent<MeshFilter>() != null)
        {
            Mesh parentMesh = transform.parent.GetComponent<MeshFilter>().mesh;

            if (parentMesh != null)
            {
                Vector3[] vertices = parentMesh.vertices;

                // Find the highest Y-coordinate among all vertices
                float maxY = float.MinValue;
                foreach (Vector3 vertex in vertices)
                {
                    if (vertex.y > maxY)
                    {
                        maxY = vertex.y;
                    }
                }

                // Set the object's position one unit higher than the highest vertex
                transform.position = transform.parent.position + new Vector3(0f, maxY +  0.2f, 0f);
            }
            else
            {
                Debug.LogError("Parent object does not have a valid mesh!");
            }
        }
        else
        {
            Debug.LogError("No parent object found or parent object does not have a mesh filter!");
        }
    }

    private void Update()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Calculate the direction from the object to the camera
            Vector3 lookDirection = mainCamera.transform.position - transform.position;

            // Ensure the object is always facing the camera
            transform.rotation = Quaternion.LookRotation(-lookDirection, Vector3.up); // Negating lookDirection
        }
        else
        {
            Debug.LogError("No main camera found in the scene!");
        }
    }
}
