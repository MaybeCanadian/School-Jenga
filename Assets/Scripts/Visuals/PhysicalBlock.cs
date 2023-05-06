using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBlock : MonoBehaviour
{
    public BlockData blockData;
    private Color startColor;
    private MeshRenderer meshRenderer;
    private Rigidbody rb;
    private Collider col;
    private Vector3 startPos;
    private Quaternion startRot;

    #region Init Functions
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        startColor = GetComponent<Renderer>().material.color;

        startPos = transform.localPosition;
        startRot = transform.localRotation;
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
        GameController.OnTestMyStack += OnTestMyStack;
        GameController.OnReset += OnReset;
    }
    private void DisconnectEvents()
    {
        GameController.OnTestMyStack -= OnTestMyStack;
        GameController.OnReset -= OnReset;
    }
    #endregion


    #region Event Recievers
    private void OnTestMyStack()
    {
        if(blockData.mastery == 0)
        {
            HideBlock();
        }

        rb.isKinematic = false;
        rb.useGravity = true;
    }
    private void OnReset()
    {
        if(blockData.mastery == 0)
        {
            ShowBlock();
        }

        transform.localPosition = startPos;
        transform.localRotation = startRot;

        rb.isKinematic = true;
        rb.useGravity = false;
    }
    #endregion

    public void SetBlockData(BlockData data)
    {
        blockData = data;
    }
    public void SelectBlock()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
    public void DeselectBlock()
    {
        GetComponent<Renderer>().material.color = startColor;
    }
    private void HideBlock()
    {
        meshRenderer.enabled = false;
        col.enabled = false;
    }
    private void ShowBlock()
    {
        meshRenderer.enabled = true;
        col.enabled = true;
    }
}
