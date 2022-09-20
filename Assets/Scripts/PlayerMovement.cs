using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* variables */
    //Player speed
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Vector3 velocity;

    /* references */
    private CharacterController controller;
    private KeyCode runningKey = KeyCode.LeftShift;
    private KeyCode jumpingKey = KeyCode.Space;

    //GRAVITY
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;

    //If you want to change gravity manually
    //[SerializeField] private float gravity;
    private const float gravity = -9.81f;

    [SerializeField] private float jumpHeight;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            //stops apply gravity if grounded
            velocity.y = -2f;
        }

        float zMove = Input.GetAxis("Vertical");
        float xMove = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(0, 0, zMove);

        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(runningKey))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(runningKey))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }


            if (Input.GetKey(jumpingKey))
            {
                Jump();
            }
        }

        moveDirection *= moveSpeed;
        // controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move((velocity + moveDirection) * Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
    }
    private void Run()
    {
        moveSpeed = runSpeed;
    }
    private void Idle()
    {

    }
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
}
