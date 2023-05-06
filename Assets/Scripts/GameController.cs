using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

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
    private void InitSystems()
    {
        BlockDataManager.OutSideInit();
    }
    #endregion
}
