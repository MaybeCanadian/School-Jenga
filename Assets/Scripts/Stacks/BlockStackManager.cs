using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockStackManager
{
    #region Event Dispatchers
    public delegate void BlockStackEvent();
    public static BlockStackEvent OnStacksGenerated;
    public static BlockStackEvent OnStacksCleared;
    #endregion

    static Dictionary<string, BlockStack> blockStacks = null;
    private static bool inited = false;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(inited == false)
        {
            Init();
        }
    }
    private static void Init()
    {
        inited = true;
    }
    #endregion

    #region Stack Control
    /// <summary>
    /// Genrates block stacks based on grade.
    /// </summary>
    /// <param name="dataCluster"></param>
    /// <param name="layerSize"></param>
    public static void CreateStacksFromData(BlockDataCluster dataCluster, int layerSize) 
    {
        if (blockStacks != null)
        {
            ClearStacks();
        }

        blockStacks = new Dictionary<string, BlockStack>();

        if(dataCluster == null)
        {
            Debug.LogError("ERROR - Cannot create stacks, data cluster is null.");
            return;
        }

        if (dataCluster.blocks == null)
        {
            Debug.LogError("ERROR - Cannot create stacks, data cluster blocks are null.");
            return;
        }

        foreach (BlockData block in dataCluster.blocks)
        {
            if (blockStacks.ContainsKey(block.grade))
            {
                blockStacks[block.grade].AddBlockToStack(block);
                continue;
            }

            BlockStack newStack = new BlockStack(block.grade, layerSize);

            blockStacks.Add(block.grade, newStack);

            newStack.AddBlockToStack(block);
        }

        foreach(string key in blockStacks.Keys)
        {
            if(!blockStacks.ContainsKey(key))
            {
                Debug.LogError("ERROR - Had to skip key " + key + " when generating stacks.");
                continue;
            }

            blockStacks[key].GenerateLayers();

        }

        OnStacksGenerated?.Invoke();
    }

    /// <summary>
    /// Clears all stacks.
    /// </summary>
    public static void ClearStacks()
    {
        foreach(string key in blockStacks.Keys)
        {
            if(!blockStacks.ContainsKey(key))
            {
                Debug.LogError("ERROR - Had to skip key " + key + " when clearing stacks.");
                continue;
            }

            blockStacks[key].ClearBlockStack();
        }

        blockStacks.Clear();
        blockStacks = null;

        OnStacksCleared?.Invoke();
    }

    /// <summary>
    /// Returns a list of the current stacks.
    /// </summary>
    /// <returns></returns>
    public static List<BlockStack> GetStacks()
    {
        List<BlockStack> stacks = new List<BlockStack>();

        foreach(string key in blockStacks.Keys)
        {
            if(!blockStacks.ContainsKey(key))
            {
                Debug.LogError("ERROR - Had to skip key " + key + " when getting stack list.");
                continue;
            }

            stacks.Add(blockStacks[key]);
        }

        return stacks;
    }
    #endregion
}
