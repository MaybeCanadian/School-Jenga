using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
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
}
