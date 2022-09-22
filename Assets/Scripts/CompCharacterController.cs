using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompCharacterController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float newSpeed;
    private Animator _animator;
    private PlayerInputs _inputs;
    private CameraController cameraController;

    [Header("Sharpness")]
    [SerializeField] private float rotationSharpness = 10f;
    [SerializeField] private float moveSharpness = 10f;

    //GRAVITY
    [Header("Gravity")]
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;

    //If you want to change gravity manually
    [SerializeField] private float gravity;
    private const float DEFAULT_GRAVITY = 9.81f;

    [SerializeField] private float jumpHeight;
    [SerializeField] private Vector3 downVector;


    private float targetSpeed;
    private Quaternion targetRotation;
    private Quaternion newRotation;
    private Vector3 newVelocity;


    void Start()
    {
        if (gravity < 0) { gravity *= -1; }
        else if (gravity == 0) { gravity = DEFAULT_GRAVITY; }

        _animator = GetComponent<Animator>();
        _inputs = GetComponent<PlayerInputs>();
        cameraController = GetComponent<CameraController>();

        _animator.applyRootMotion = false;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        Vector3 moveInputVector = new Vector3(_inputs.MoveAxisRightRaw, 0, _inputs.MoveAxisForwardRaw).normalized;
        Vector3 cameraPlanarDirection = cameraController.CameraPlanarDirection;
        Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection);

        moveInputVector = cameraPlanarRotation * moveInputVector;

        if (_inputs.Sprint.Pressed()) { targetSpeed = moveInputVector != Vector3.zero ? runSpeed : 0f; }
        else { targetSpeed = moveInputVector != Vector3.zero ? walkSpeed : 0f; }

        newSpeed = Mathf.Lerp(newSpeed, targetSpeed, Time.deltaTime * moveSharpness);

        newVelocity = moveInputVector * newSpeed;
        transform.Translate(newVelocity * Time.deltaTime, Space.World);

        if (targetSpeed != 0)
        {
            targetRotation = Quaternion.LookRotation(moveInputVector);
            newRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSharpness);
            transform.rotation = newRotation;
        }



        _animator.SetFloat("Forward", newSpeed);
    }

}
