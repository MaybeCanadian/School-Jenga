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

        foreach(BlockStackLayer layer in stackData.GetStackLayers())
        {
            foreach(BlockData block in layer.blocks)
            {
                if(block == null)
                {
                    continue;
                }

                CreatePhysicalBlock(block);
            }
        }
    }
    private void CreatePhysicalBlock(BlockData data)
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

        PhysicalBlock newBlockScript = newBlock.AddComponent<PhysicalBlock>();
        newBlockScript.SetBlockData(data);
    }
}
