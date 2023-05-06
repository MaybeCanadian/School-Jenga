using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBlockStack : MonoBehaviour
{
    public BlockStack stackData;
    public List<PhysicalBlock> blocks = new List<PhysicalBlock>();

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
    }
    private void GenerateStackLayer(BlockStackLayer layer, int layerNum)
    {
        Vector3 centerPos = transform.position;
        centerPos.y += stackData.layerSpaceOffset * layerNum;

        for (int i = 0; i < layer.blocks.Length; i++)
        {
            if (layer.blocks[i] == null)
            {
                continue;
            }

            CreatePhysicalBlock(layer.blocks[i], centerPos);
        }
    }
    private void CreatePhysicalBlock(BlockData data, Vector3 pos)
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

        newBlock.transform.position = pos;

        PhysicalBlock newBlockScript = newBlock.AddComponent<PhysicalBlock>();
        newBlockScript.SetBlockData(data);
    }
}
