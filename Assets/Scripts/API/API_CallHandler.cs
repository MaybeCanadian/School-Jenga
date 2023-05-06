using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class API_CallHandler
{
    static API_Call manager = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(manager == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        CreateManager();
    }
    private static void CreateManager()
    {
        if(manager == null)
        {
            GameObject managerOBJ = new GameObject();

            managerOBJ.name = "[API Call Manager]";
            GameObject.DontDestroyOnLoad(managerOBJ);

            manager = managerOBJ.AddComponent<API_Call>();
        }
    }
    #endregion

    #region API Requests
    public static void CreateAPIRequest(string uri)
    {
        CheckInit();

        manager.CreateAPIRequest(uri);
    }
    #endregion

    /// <summary>
    /// This will remove the old api call manager. This will remove all active requests.
    /// </summary>
    public static void ResetHandler()
    {
        if(manager == null)
        {
            return;
        }

        GameObject.Destroy(manager);
        manager = null;
    }
}
