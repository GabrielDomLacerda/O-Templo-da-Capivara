using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private float mouseScroll;

    [Header("Framing")]
    [SerializeField] private Camera _camera = null;
    [SerializeField] private Transform _followTransform = null;

    void Start()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mouseScroll = Input.GetAxis("Mouse ScrollWheel");
    }
}
