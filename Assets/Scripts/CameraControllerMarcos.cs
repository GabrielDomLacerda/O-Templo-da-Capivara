using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController(Marcos) : MonoBehaviour
{
    [Header("Framing")]
    [SerializeField] private Camera _camera = null;
    [SerializeField] private Transform _followTransform = null;

    //Privates  

    private Vector3 _planarDirection; //Camera Foward on de x,z plane
    private Quartenion _targetRotation;

    private void Start()
    {
        //important
        _planarDirection = followTransform.foward;
        cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;
        //Handle Inputs
        float _mouseX = Comp_PlayerInputs.MouseXInput;
        _planarDirection = Quartenion.Euler(0, _mouseX, 0) * _planarDirection;
        Debug.DrawLine(_camera.transform.position, _camera.transform.position + _planarDirection, Color.red);

        //final Targets
        _targetRotation = Quartenion.LookRotation(_planarDirection);

        //Apply
        _camera.transform.rotation = _targetRotation;


    }

}
