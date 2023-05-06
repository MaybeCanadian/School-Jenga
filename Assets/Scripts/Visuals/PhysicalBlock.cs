using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBlock : MonoBehaviour
{
    public BlockData blockData;
    private Color startColor;
    private MeshRenderer renderer;
    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        startColor = renderer.material.color;
    }
    public void SetBlockData(BlockData data)
    {
        blockData = data;
    }
    public void SelectBlock()
    {
        Debug.Log("Selected block " + blockData.standardid);

        renderer.material.color = Color.red;
    }
    public void DeselectBlock()
    {
        Debug.Log("Deselected block " + blockData.standardid);

        renderer.material.color = startColor;
    }
}
