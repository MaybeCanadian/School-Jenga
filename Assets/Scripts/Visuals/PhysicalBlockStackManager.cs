using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicalBlockStackManager
{
    private static GameObject stackParent = null;

    private static List<PhysicalStackSpawnMarker> spawnMarkers;

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

            newPhysicalStack.transform.position = FindAssoicatedMarkerPos(stack.grade);

            PhysicalBlockStack newPhysicalStackScript = newPhysicalStack.AddComponent<PhysicalBlockStack>();
            newPhysicalStackScript.SetStackData(stack);
            newPhysicalStackScript.GeneratePhysicalBlockStack();
        }
    }
    private static Vector3 FindAssoicatedMarkerPos(string grade)
    {
        foreach(PhysicalStackSpawnMarker marker in spawnMarkers)
        {
            if(marker.grade == grade)
            {
                return marker.transform.position;
            }
        }

        return Vector3.zero;
    }
    #endregion
}
