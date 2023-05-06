using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBlockStack : MonoBehaviour
{
    public BlockStack stackData;
    public List<PhysicalBlock> blocks = new List<PhysicalBlock>();

    public GameObject stackCenter = null;

    public void SetStackData(BlockStack data)
    {
        stackData = data;
    }
    public void GeneratePhysicalBlockStack()
    {
        if(stackData == null)
        {
            Debug.LogError("ERROR - Stack Data is null, cannot create physical block stack.");
            return;
        }

        int layerNum = 0;

        foreach(BlockStackLayer layer in stackData.GetStackLayers())
        {
            GenerateStackLayer(layer, layerNum);

            layerNum++;
        }

        DetermineCenter();
    }
    private void GenerateStackLayer(BlockStackLayer layer, int layerNum)
    {
        Vector3 centerPos = Vector3.up * stackData.layerSpaceOffset * layerNum;

        Vector3 layerDirection;
        float rotation;

        if (layerNum % 2 == 0)
        {
            layerDirection = Vector3.right;
            rotation = 00.0f;
        }
        else
        {
            layerDirection = Vector3.forward;
            rotation = 90.0f;
        }

        centerPos -= layerDirection * stackData.blockSpaceOffset;

        for (int i = 0; i < layer.blocks.Length; i++)
        {
            if (layer.blocks[i] == null)
            {
                continue;
            }

            Vector3 spawnPos = centerPos + layerDirection * stackData.blockSpaceOffset * i;

            CreatePhysicalBlock(layer.blocks[i], spawnPos, rotation);
        }
    }
    private void CreatePhysicalBlock(BlockData data, Vector3 pos, float rotation)
    {
        if(data == null)
        {
            Debug.LogError("ERROR - Could not create a block, data is null.");
            return;
        }

        GameObject blockPrefab = PhysicalBlockDataBase.GetBlock((BlockTypes)data.mastery);

        GameObject newBlock = Instantiate(blockPrefab);
        newBlock.name = data.standardid;
        newBlock.transform.parent = transform;

        newBlock.transform.localPosition = pos;
        newBlock.transform.localRotation = Quaternion.Euler(0.0f, rotation, 0.0f);

        PhysicalBlock newBlockScript = newBlock.AddComponent<PhysicalBlock>();
        newBlockScript.SetBlockData(data);
    }
    private void DetermineCenter()
    {
        stackCenter = new GameObject();
        stackCenter.transform.parent = transform;
        stackCenter.name = "Stack Center";

        stackCenter.transform.localPosition = Vector3.up * stackData.layerSpaceOffset * stackData.GetStackLayers().Count / 2.0f;
    }

    public Transform GetStackCenter()
    {
        return stackCenter.transform;
    }
}
