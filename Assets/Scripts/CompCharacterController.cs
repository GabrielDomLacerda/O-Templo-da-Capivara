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
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private LayerMask groundMask = -1;


    private PergaminhoController _scroll;
    private float targetSpeed;
    private Rigidbody _rb;
    private Quaternion targetRotation;
    private Quaternion newRotation;
    private Vector3 newVelocity;
    private float jumpForce;


    void Start()
    {
        jumpForce = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
        _scroll = GetComponent<PergaminhoController>();
        _animator = GetComponent<Animator>();
        _inputs = GetComponent<PlayerInputs>();
        cameraController = GetComponent<CameraController>();
        _rb = GetComponent<Rigidbody>();

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

    void FixedUpdate()
    {
        if (_inputs.Jump.Pressed() && isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _animator.SetTrigger("JumpOn");
        }
    }

    void OnCollisionEnter(Collision x)
    {
        if (x.gameObject.tag == "Coletavel" && x.gameObject.GetComponentInChildren<MeshRenderer>().enabled)
        {
            _scroll.SetNext();
        }
    }

}