using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBlock : MonoBehaviour
{
    public BlockData blockData;
    private Color startColor;
    private MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        startColor = GetComponent<Renderer>().material.color;
    }
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
}
