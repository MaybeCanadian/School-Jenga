using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BlockStack
{
    #region Event Dispatchers
    public delegate void BlockStackEvent();
    public BlockStackEvent OnStackGenerated;
    public BlockStackEvent OnStackCleared;
    #endregion

    #region Member Variables
    public string grade = "Default Grade";
    int layerSize = 1;

    //domainID, than cluster name, than standard id
    private Dictionary<string, Dictionary<string, List<BlockData>>> stackBlocks;

    private List<BlockStackLayer> layers;
    private List<string> domaindIDs;
    private List<string> clusterIDs;
    #endregion

    public BlockStack(string grade, int layerSize)
    {
        this.grade = grade;
        this.layerSize = layerSize;

        layers = new List<BlockStackLayer>();

        domaindIDs = new List<string>();
        clusterIDs = new List<string>();

        stackBlocks = new Dictionary<string, Dictionary<string, List<BlockData>>>();
    }

    #region Block Control
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
    private void PlaceBlockInLayers(BlockData data)
    {
        BlockStackLayer currentLayer = layers.Last();

        if (currentLayer.PlaceBlockNext(data))
        {
            return;
        }

        BlockStackLayer newLayer = new BlockStackLayer(layerSize);

        layers.Add(newLayer);

        if (!newLayer.PlaceBlockNext(data))
        {
            Debug.LogError("ERROR - Could not place a block in a new layer, layer should be empty.");
            return;
        }
    }
    public void ClearBlockStack()
    {
        stackBlocks.Clear();
        layers.Clear();

        OnStackCleared?.Invoke();
    }
    #endregion

    #region Stack Generation
    /// <summary>
    /// Generates the block stack using the sorted blocks, clears the stored sorted blocks.
    /// </summary>
    public void GenerateLayers()
    {
        domaindIDs.Sort();
        clusterIDs.Sort();

        SortThroughDomainIDS();

        stackBlocks.Clear();

        OnStackGenerated?.Invoke();
    }
    private void SortThroughDomainIDS()
    {
        foreach(string domainID in domaindIDs)
        {
            if(!stackBlocks.ContainsKey(domainID))
            {
                Debug.LogError("ERROR - Skipped domainid: " + domainID);
                continue;
            }

            SortThroughClusterIDS(domainID);

            stackBlocks[domainID].Clear();
        }
    }
    private void SortThroughClusterIDS(string domainID)
    {
        foreach (string clusterID in clusterIDs)
        {
            if (!stackBlocks[domainID].ContainsKey(domainID))
            {
                Debug.LogError("ERROR - Skipped cluserid: " + clusterID + " in domainid: "+ domainID);
                continue;
            }

            SortThroughStandardIDS(domainID, clusterID);

            stackBlocks[domainID][clusterID].Clear();
        }
    }
    private void SortThroughStandardIDS(string domainID, string clusterID)
    {
        List<BlockData> blocks = stackBlocks[domainID][clusterID];

        List<string> standardIDS = new List<string>();

        foreach(BlockData block in blocks)
        {
            standardIDS.Add(block.standardid);
        }

        standardIDS.Sort();

        foreach(string standardID in standardIDS)
        {
            int index = -1;

            for(int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].standardid == standardID)
                {
                    index = i;
                    PlaceBlockInLayers(blocks[i]);
                    break;
                }
            }

            blocks.RemoveAt(index);
        }

        blocks.Clear();
        standardIDS.Clear();
    }
    #endregion
}

[System.Serializable]
public class BlockStackLayer
{
    public BlockData[] blocks = new BlockData[3];
    public int layerSize = 3;

    public BlockStackLayer(int size)
    {
        layerSize = (size > 0) ? size : 1;
        blocks = new BlockData[layerSize];
    }

    /// <summary>
    /// Places a block in the layer in the next available spot. Returns if it placed a block.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="replace"></param>
    /// <returns></returns>
    public bool PlaceBlockNext(BlockData data) 
    {
        for(int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i] == null)
            {
                blocks[i] = data;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Places a block in the layer at given index. Returns if it placed a block.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="index"></param>
    /// <param name="replace"></param>
    /// <returns></returns>
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
