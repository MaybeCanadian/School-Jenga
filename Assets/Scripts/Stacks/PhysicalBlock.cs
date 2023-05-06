using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBlock : MonoBehaviour
{
    public BlockData blockData;

    public void SetBlockData(BlockData data)
    {
        blockData = data;
    }
}
