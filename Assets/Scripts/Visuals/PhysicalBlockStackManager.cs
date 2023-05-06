using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicalBlockStackManager
{
    private static GameObject stackParent = null;
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

    private static void CheckParent()
    {
        if(stackParent != null)
        {
            return;
        }

        stackParent = new GameObject();
        stackParent.name = "[Stacks]";
    }
    public static void CreateBlockStackVisuals(List<BlockStack> stacks, List<string> ignore)
    {
        CheckInit();

        if (stacks == null)
        {
            Debug.LogError("ERROR - Could not make stack visuals as the stack list is null.");
            return;
        }

        CheckParent();

        Debug.Log(stacks.Count);

        foreach (BlockStack stack in stacks)
        {
            if(ignore.Contains(stack.grade))
            {
                continue;
            }

            GameObject newPhysicalStack = new GameObject();
            newPhysicalStack.transform.parent = stackParent.transform;
            newPhysicalStack.name = stack.grade;

            PhysicalBlockStack newPhysicalStackScript = newPhysicalStack.AddComponent<PhysicalBlockStack>();
            newPhysicalStackScript.SetStackData(stack);
            newPhysicalStackScript.GeneratePhysicalBlockStack();
        }
    }
}
