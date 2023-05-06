using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public GameObject moreInfoParent;

    public TMP_Text gradeText;
    public TMP_Text domainText;
    public TMP_Text clusterText;
    public TMP_Text standardidText;
    public TMP_Text standardDescriptionText;

    #region Init Functions
    private void Start()
    {
        moreInfoParent.SetActive(false);
    }
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
        BlockSelectorScript.OnBlockSelectionChanged += OnSelectedBlockChanged;
    }
    private void DisconnectEvents()
    {
        BlockSelectorScript.OnBlockSelectionChanged -= OnSelectedBlockChanged;
    }
    #endregion

    private void OnSelectedBlockChanged(PhysicalBlock block)
    {
        if(block == null)
        {
            moreInfoParent.SetActive(false);
            return;
        }

        BlockData data = block.blockData;

        if(data == null)
        {
            moreInfoParent.SetActive(false);
            return;
        }

        moreInfoParent.SetActive(true);

        gradeText.text = data.grade;
        domainText.text = data.domain;
        clusterText.text = data.cluster;
        standardidText.text = data.standardid;
        standardDescriptionText.text = data.standarddescription;
    }


    #region Button Events
    public void On6thGradePressed()
    {
        CameraController.instance.SetViewTarget("6th Grade");
    }
    public void On7thGradePressed()
    {
        CameraController.instance.SetViewTarget("7th Grade");
    }
    public void On8thGradePressed()
    {
        CameraController.instance.SetViewTarget("8th Grade");
    }
    #endregion
}
