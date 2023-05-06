using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Event Dispatchers
    public delegate void GamePlayEvent();
    public static GamePlayEvent OnTestMyStack;
    public static GamePlayEvent OnReset;
    #endregion

    public static GameController instance;

    [Header("Stacks")]
    [Tooltip("The number of blocks in each layer of the stacks")]
    public int layerSize = 3;
    public float blockOffset = 1.1f;
    public float layerOffset = 0.25f;
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

        BlockStackManager.CreateStacksFromData(cluster, layerSize, blockOffset, layerOffset);
    }
    private void OnBlockStacksGenerated()
    {
        List<BlockStack> stacks = BlockStackManager.GetStacks();

        PhysicalBlockStackManager.CreateBlockStackVisuals(stacks, stackGradeIgnoreList);
    }
    #endregion

    public void TestMyStack()
    {
        OnTestMyStack?.Invoke();
    }
    public void ResetStack()
    {
        OnReset?.Invoke();
    }
}
