using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerInputs : MonoBehaviour
{

    public const string MouseXString = "Mouse X";
    public const string MouseYString = "Mouse Y";
    public const string MouseScrollString = "Mouse Scrollwheel";

    public static float MouseXInput { get => UnityEngine.Input.GetAxis(MouseXInput);  }
    public static float MouseYInput { get => UnityEngine.Input.GetAxis(MouseYInput); }
    public static float MouseScrollString { get => UnityEngine.Input.GetAxis(MouseScrollString); }



}

