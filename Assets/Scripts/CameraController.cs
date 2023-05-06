using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform currentViewTarget;

    public string startingViewTarget;

    public Vector3 oldMousePos;
    public float rotationSpeed = 10.0f;
    public float scrollRate = 10.0f;

    public float minScrollDist = 10.0f;
    public float maxScrollDist = 100.0f;

    public float currentDist;

    #region Init Functions
    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        oldMousePos = Input.mousePosition;
    }
    private void OnEnable()
    {
        ConnectEvents();
    }
    private void OnDisable()
    {
        DisconnectEvents();
    }
    private void ConnectEvents()
    {
        PhysicalBlockStackManager.OnPhysicalStacksGenerated += OnPhysicalStacksGenerated;
    }
    private void DisconnectEvents()
    {
        PhysicalBlockStackManager.OnPhysicalStacksGenerated-= OnPhysicalStacksGenerated;
    }
    #endregion

    #region Targeting
    public void SetViewTarget(string name)
    {
        Transform newTarget = PhysicalBlockStackManager.GetStackCenter(name);

        if(newTarget == null)
        {
            Debug.LogError("ERROR - New view target is null.");
            return;
        }

        if(currentViewTarget == null)
        {
            currentViewTarget = newTarget;
            return;
        }

        Vector3 displacement = newTarget.position - currentViewTarget.position;

        transform.position += displacement;

        currentViewTarget = newTarget;
    }
    #endregion

    #region Updates
    private void Update()
    {
        if(currentViewTarget == null)
        {
            return;
        }

        currentDist = (transform.position - currentViewTarget.position).magnitude;

        if (Input.GetMouseButton(1))
        {
            Vector3 mouseDelta = Input.mousePosition - oldMousePos;

            transform.RotateAround(currentViewTarget.position, Vector3.up, rotationSpeed * mouseDelta.x * Time.deltaTime);
            transform.RotateAround(currentViewTarget.position,  -1.0f * transform.right, rotationSpeed * mouseDelta.y * Time.deltaTime);
        }

        if(Input.mouseScrollDelta.magnitude != 0)
        {
            if(Input.mouseScrollDelta.y < 0)
            {
                if(currentDist < maxScrollDist)
                {
                    transform.position += transform.forward * -1.0f * scrollRate * Time.deltaTime;
                }
            }
            else
            {
                if(currentDist > minScrollDist)
                {
                    transform.position += transform.forward * scrollRate * Time.deltaTime;
                }
            }
        }

        oldMousePos = Input.mousePosition;
    }
    private void LateUpdate()
    {
        transform.LookAt(currentViewTarget);
    }
    #endregion

    #region Event Recievers
    private void OnPhysicalStacksGenerated()
    {
        SetViewTarget(startingViewTarget);
    }
    #endregion
}
