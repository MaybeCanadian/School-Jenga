using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicalBlockStackManager
{
    private static bool inited = false;

    private static GameObject stackParent = null;

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

    private static void CheckParent()
    {
        if(stackParent != null)
        {
            return;
        }

        stackParent = new GameObject();
        stackParent.name = "[Stacks]";
    }

    public static void CreateBlockStackVisuals(List<BlockStack> stacks)
    {
        CheckInit();

        if (stacks == null)
        {
            Debug.LogError("ERROR - Could not make stack visuals as the stack list is null.");
            return;
        }

        CheckParent();

        foreach (BlockStack stack in stacks)
        {
            GameObject newPhysicalStack = new GameObject();
            newPhysicalStack.transform.parent = stackParent.transform;

        }
    }
}
