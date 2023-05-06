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

            }
        }
    }
    private void CreatePhysicalBlock(BlockData data)
    {
        GameObject blockOBJ = new GameObject();
        blockOBJ.name = data.standardid;
        blockOBJ.transform.parent = transform;


    }
}
