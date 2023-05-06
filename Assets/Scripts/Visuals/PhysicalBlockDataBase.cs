using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicalBlockDataBase
{
    private static Dictionary<BlockTypes, GameObject> blocksDict = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(blocksDict == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        LoadBlocks();
    }
    private static void LoadBlocks()
    {
        blocksDict = new Dictionary<BlockTypes, GameObject>();

        foreach(int name in Enum.GetValues(typeof(BlockTypes)))
        {
            GameObject block = Resources.Load<GameObject>("Prefabs/Blocks/" + (BlockTypes)name);

            if(block == null)
            {
                Debug.LogError("ERROR - Skipped loading of " + (BlockTypes)name);
                continue;
            }

            blocksDict.Add((BlockTypes)name, block);
        }
    }
    #endregion

    public static GameObject GetBlock(BlockTypes type)
    {
        CheckInit();

        if(!blocksDict.ContainsKey(type))
        {
            Debug.LogError("ERROR - Block data base does not have a block for " + type);
            return null;
        }

        return blocksDict[type];
    }
}

[System.Serializable]
public enum BlockTypes
{
    GLASS,
    WOOD,
    STONE
}
