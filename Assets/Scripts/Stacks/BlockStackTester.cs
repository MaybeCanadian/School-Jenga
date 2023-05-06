using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStackTester : MonoBehaviour
{
    public List<BlockStack> stacks;

    #region Init Functions
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
        BlockStackManager.OnStacksGenerated += OnStacksGenerated;
    }
    private void DisconnectEvents()
    {
        BlockStackManager.OnStacksGenerated-= OnStacksGenerated;
    }
    #endregion

    #region Event Recievers
    private void OnStacksGenerated()
    {
        stacks = BlockStackManager.GetStacks();
    }
    #endregion
}
