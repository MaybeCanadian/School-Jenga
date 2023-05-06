using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockDataManager
{
    #region Event Dispatchers
    public delegate void BlockDataEvent();
    public static BlockDataEvent OnDataLoaded;
    public static BlockDataEvent OnDataCleared;
    public static BlockDataEvent OnDataError;
    #endregion

    private const string API_URI = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    private static bool inited = false;

    private static BlockDataCluster blocksData = null;

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

    #region API
    public static void LoadBlocksData()
    {
        CheckInit();

        if (blocksData != null)
        {
            ClearBlocksData();
        }

        API_CallHandler.CreateAPIRequest(API_URI);

        ConnectAPIEvents();
    }
    private static void ConnectAPIEvents()
    {
        API_Call.OnSuccess += OnAPISuccessEvent;
        API_Call.OnConnectionError += OnAPIErrorEvent;
        API_Call.OnProtocalError += OnAPIErrorEvent;
        API_Call.OnDataProcessingError += OnAPIErrorEvent;
    }
    private static void DisconnectAPIEvents()
    {
        API_Call.OnSuccess -= OnAPISuccessEvent;
        API_Call.OnConnectionError -= OnAPIErrorEvent;
        API_Call.OnProtocalError -= OnAPIErrorEvent;
        API_Call.OnDataProcessingError -= OnAPIErrorEvent;
    }
    private static void OnAPISuccessEvent(string uri, string data)
    {
        if(uri != API_URI)
        {
            return;
        }

        DisconnectAPIEvents();

        ConvertJSONtoBlocks(data);

        OnDataLoaded?.Invoke();
    }
    private static void OnAPIErrorEvent(string uri, string error)
    {
        if (uri != API_URI)
        {
            return;
        }

        DisconnectAPIEvents();
    }
    #endregion

    #region Blocks
    private static string FixJSON(string data)
    {
        data = "{\"blocks\":" + data + "}";

        return data;
    }
    private static void ConvertJSONtoBlocks(string data)
    {
        data = FixJSON(data);

        blocksData = JsonUtility.FromJson<BlockDataCluster>(data);

        if(blocksData == null)
        {
            Debug.LogError("ERROR - Error converting block data from JSON.");
            return;
        }

        Debug.Log(blocksData.blocks.Length);
    }
    public static void ClearBlocksData()
    {
        blocksData = null;

        OnDataCleared?.Invoke();
    }
    public static BlockDataCluster GetBlockDataCluster()
    {
        return blocksData; 
    }
    #endregion
}
