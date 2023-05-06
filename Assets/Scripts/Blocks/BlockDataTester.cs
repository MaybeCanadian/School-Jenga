using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDataTester : MonoBehaviour
{

    public BlockDataCluster data = null;

    private void OnEnable()
    {
        ConnectEvents();
    }
    private void OnDisable()
    {
        DisconnectEvents();
    }
    private void ConnectEvents()
    {
        BlockDataManager.OnDataLoaded += OnBlockDataLoaded;
    }
    private void DisconnectEvents() 
    {
        BlockDataManager.OnDataLoaded -= OnBlockDataLoaded;
    }

    private void OnBlockDataLoaded()
    {
        data = BlockDataManager.GetBlockDataCluster();
    }
}
