using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float smoothMovementTime = 0.3f;
    public float smoothRotationTime = 0.1f;
    public float smoothPivotTime = 0.3f;

    public Vector3 minBounds;
    public Vector3 maxBounds;

    private Transform pivotTransform;
    private Vector3 targetPivotPosition;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector3 velocity;
    private Vector3 pivotVelocity;
   

    void Start()
    {
        pivotTransform = new GameObject("Pivot").transform;
        targetPosition = transform.position;
        targetPivotPosition = pivotTransform.position;
    }

    void Update()
    {
        MoveCamera();
        UpdatePivotPoint();
        RotateCamera();
        SmoothMovement();
        SmoothRotation();
        SmoothPivotTransition();
    }

    void MoveCamera()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forwardMovement = transform.forward * verticalInput;
        Vector3 rightMovement = transform.right * horizontalInput;

        Vector3 movementDirection = (forwardMovement + rightMovement).normalized * moveSpeed * Time.deltaTime;

        float yMovement = 0f;
        if (Input.GetKey(KeyCode.Q)) yMovement = -moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) yMovement = moveSpeed * Time.deltaTime;

        Vector3 movement = new Vector3(movementDirection.x, yMovement, movementDirection.z);

        Vector3 newPosition = transform.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
        newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

        targetPosition = newPosition;
    }

    void UpdatePivotPoint()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetPivotPosition = hit.point;
            }
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed;

        pivotTransform.Rotate(Vector3.up, mouseX, Space.World);
        pivotTransform.Rotate(Vector3.right, mouseY, Space.World);

        targetRotation = Quaternion.LookRotation(pivotTransform.position - transform.position);
    }

    void SmoothMovement()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothMovementTime);
    }

    void SmoothRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothRotationTime);
    }

    void SmoothPivotTransition()
    {
        if (pivotTransform.position != targetPivotPosition)
        {
            pivotTransform.position = Vector3.SmoothDamp(pivotTransform.position, targetPivotPosition, ref pivotVelocity, smoothPivotTime);
        }
        targetRotation = Quaternion.LookRotation(pivotTransform.position - transform.position);
    }
}