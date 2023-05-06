using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicalBlockStackManager
{
    #region Event Dispatchers
    public delegate void PhysicalBlockStackEvent();
    public static PhysicalBlockStackEvent OnPhysicalStacksGenerated;
    public static PhysicalBlockStackEvent OnPhysicalStacksCleared;
    #endregion

    private static GameObject stackParent = null;
    private static List<PhysicalStackSpawnMarker> spawnMarkers;
    private static Dictionary<string, PhysicalBlockStack> stacksDict;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(spawnMarkers == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        spawnMarkers = new List<PhysicalStackSpawnMarker>();
        stacksDict = new Dictionary<string, PhysicalBlockStack>();
    }
    #endregion

    #region Markers
    public static void AddSpawnMarker(PhysicalStackSpawnMarker marker)
    {
        CheckInit();

        spawnMarkers.Add(marker);
    }
    public static void RemoveSpawnMarker(PhysicalStackSpawnMarker marker)
    {
        CheckInit();

        spawnMarkers.Remove(marker);
    }
    #endregion

    #region Stacks
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

            Transform spawnTrans = FindAssoicatedMarkerPos(stack.grade);

            Vector3 spawnPos = Vector3.zero;

            if (spawnTrans != null)
            {
                spawnPos = spawnTrans.position;
            }

            newPhysicalStack.transform.position = spawnPos;

            PhysicalBlockStack newPhysicalStackScript = newPhysicalStack.AddComponent<PhysicalBlockStack>();
            newPhysicalStackScript.SetStackData(stack);
            newPhysicalStackScript.GeneratePhysicalBlockStack();

            stacksDict.Add(stack.grade, newPhysicalStackScript);
        }

        OnPhysicalStacksGenerated?.Invoke();
    }
    private static Transform FindAssoicatedMarkerPos(string grade)
    {
        foreach(PhysicalStackSpawnMarker marker in spawnMarkers)
        {
            if(marker.grade == grade)
            {
                return marker.transform;
            }
        }

        return null;
    }
    public static Transform GetStackCenter(string grade)
    {
        CheckInit();

        if(!stacksDict.ContainsKey(grade))
        {
            Debug.LogError("ERROR - Could not locate stack for given name.");
            return null;
        }

        return stacksDict[grade].GetStackCenter();
    }
    public static void ClearPhysicalStacks()
    {
        GameObject.Destroy(stackParent);

        OnPhysicalStacksCleared?.Invoke();
    }
    #endregion
}
