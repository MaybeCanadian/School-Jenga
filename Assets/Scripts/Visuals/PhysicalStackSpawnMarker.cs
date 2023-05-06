using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalStackSpawnMarker : MonoBehaviour
{
    public string grade = "";

    private void Awake()
    {
        PhysicalBlockStackManager.AddSpawnMarker(this);
    }
    private void OnDestroy()
    {
        PhysicalBlockStackManager.RemoveSpawnMarker(this);
    }
}
