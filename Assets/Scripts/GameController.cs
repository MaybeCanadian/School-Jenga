using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Stacks")]
    [Tooltip("The number of blocks in each layer of the stacks")]
    public int layerSize = 3;
    public List<string> stackGradeIgnoreList;

    #region Init Functions
    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        InitSystems();

        BlockDataManager.LoadBlocksData();
    }
    private void OnEnable()
    {
        ConnectEvents();
    }
    private void OnDisable()
    {
        DisconnectEvents();
    }
    private void InitSystems()
    {
        BlockDataManager.OutSideInit();
    }
    private void ConnectEvents()
    {
        BlockDataManager.OnDataLoaded += OnBlockDataLoaded;

        BlockStackManager.OnStacksGenerated += OnBlockStacksGenerated;
    }
    private void DisconnectEvents()
    {
        BlockDataManager.OnDataLoaded -= OnBlockDataLoaded;

        BlockStackManager.OnStacksGenerated -= OnBlockStacksGenerated;
    }
    #endregion

    #region Event Recievers
    private void OnBlockDataLoaded()
    {
        BlockDataCluster cluster = BlockDataManager.GetBlockDataCluster();

        BlockStackManager.CreateStacksFromData(cluster, layerSize);
    }
    private void OnBlockStacksGenerated()
    {
        List<BlockStack> stacks = BlockStackManager.GetStacks();

        PhysicalBlockStackManager.CreateBlockStackVisuals(stacks, stackGradeIgnoreList);
    }
    #endregion
}
