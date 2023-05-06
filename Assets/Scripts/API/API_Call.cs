using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class API_Call : MonoBehaviour
{
    #region Event Dispatchers
    public delegate void API_SuccessEvent(string uri, string text);
    public static API_SuccessEvent OnSuccess;

    public delegate void API_ErrorEvent(string uri, string error);
    public static API_ErrorEvent OnConnectionError;
    public static API_ErrorEvent OnDataProcessingError;
    public static API_ErrorEvent OnProtocalError;
    #endregion

    #region API Requests
    public void CreateAPIRequest(string uri)
    {
        StartCoroutine(GetRequest(uri));
    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');

            int page = pages.Length - 1;

            switch(webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("ERROR - API Connection Error: " + webRequest.error);
                    OnConnectionError?.Invoke(uri, webRequest.error);
                    break;
                    
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("ERROR - API Data Processing Error: " + webRequest.error);
                    OnDataProcessingError?.Invoke(uri, webRequest.error);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("ERROR - API Protocal Error: " + webRequest.error);
                    OnProtocalError?.Invoke(uri, webRequest.error);
                    break;

                case UnityWebRequest.Result.Success:
                    Debug.Log("API Request Successful.");
                    OnSuccess?.Invoke(uri, webRequest.downloadHandler.text);
                    break;
            }
        }

        yield break;
    }
    #endregion
}

