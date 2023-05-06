using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStack
{
    public string grade = "Default Grade";
    int layerSize = 1;

    //domainID, than cluster name, than standard id
    private Dictionary<string, Dictionary<string, List<BlockData>>> stackBlocks;

    private List<BlockStackLayer> layers;
    private List<string> domaindIDs;
    private List<string> clusterIDs;

    public BlockStack(string grade, int layerSize)
    {
        this.grade = grade;
        this.layerSize = layerSize;

        layers = new List<BlockStackLayer>();

        domaindIDs = new List<string>();
        clusterIDs = new List<string>();

        stackBlocks = new Dictionary<string, Dictionary<string, List<BlockData>>>();
    }

    public void AddBlockToStack(BlockData block)
    {
        if(block == null)
        {
            Debug.LogError("ERROR - Could not add a null block into a stack.");
            return;
        }
        
        if(!stackBlocks.ContainsKey(block.domainid))
        {
            Dictionary<string, List<BlockData>> newDomain = new Dictionary<string, List<BlockData>>();

            stackBlocks.Add(block.domainid, newDomain);

            domaindIDs.Add(block.domainid);
        }

        if(!stackBlocks[block.domainid].ContainsKey(block.cluster))
        {
            List<BlockData> newCluster = new List<BlockData>();

            stackBlocks[block.domainid].Add(block.cluster, newCluster);

            clusterIDs.Add(block.cluster);
        }

        stackBlocks[block.domainid][block.cluster].Add(block);
        
    }
    public void GenerateLayers()
    {
        domaindIDs.Sort();

        foreach(string id in domaindIDs)
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
