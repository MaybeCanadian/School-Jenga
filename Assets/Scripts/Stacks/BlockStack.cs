using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStack
{
    public string grade = "Default Grade";
    int layerSize = 1;

    private List<BlockStackLayer> layers = new List<BlockStackLayer>();
    private List<BlockData> stackBlocks = new List<BlockData>();
    private List<string> domainIds = new List<string>();

    public BlockStack(string grade, int layerSize)
    {
        this.grade = grade;
        this.layerSize = layerSize;
    }

    public void AddBlockToStack(BlockData block)
    {
        if(block == null)
        {
            Debug.LogError("ERROR - Could not add a null block into a stack.");
            return;
        }

        stackBlocks.Add(block);

        if(!domainIds.Contains(block.domainid))
        {
            domainIds.Add(block.domainid);
        }
    }
    public void GenerateLayers()
    {
        domainIds.Sort();

        foreach(string domainID in domainIds)
        {

        }
    }
}

public class BlockStackLayer
{
    public BlockData[] blocks = new BlockData[3];
    public int layerSize = 3;

    public BlockStackLayer(int size)
    {
        layerSize = (size > 0) ? size : 1;
        blocks = new BlockData[layerSize];
    }
    public bool PlaceBlockAny(BlockData data, bool replace = false) 
    {
        for(int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i] == null)
            {
                blocks[i] = data;
                return true;
            }
        }

        if(!replace)
        {
            return false;
        }

        int randomIndex = Random.Range(0, blocks.Length);

        blocks[randomIndex] = data;

        return true;
    }
    public bool PlaceBlockIndex(BlockData data, int index, bool replace = false)
    {
        if(index >= blocks.Length)
        {
            Debug.LogError("ERROR - Could not place block in layer. Index out of range");
            return false;
        }

        if (blocks[index] == null)
        {
            blocks[index] = data;
            return true;
        }

        if(!replace)
        {
            return false;
        }

        blocks[index] = data;
        return true;
    }
}
