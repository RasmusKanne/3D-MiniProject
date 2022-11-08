using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;

    public Transform orientation;

    // Variables used to ground check
    public float playerHeight;
    public float groundDrag;
    public LayerMask groundMask;
    bool grounded;

    // Variables used for jumping
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    // Internal user input
    float horizontalInput;
    float verticalInput;

    private Animator animator;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        // Finding the players rigidbody component and freezing its rotation
        animator = GetComponentInChildren(typeof(Animator)) as Animator;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Implementing a groundcheck using raycast that detects if the player is on the ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);
        
        PlayerInput();
        SpeedControl();

        // Implementing drag when player is touching the ground
        if (grounded)
        {
            rb.drag = groundDrag;
        } else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Defining a function for getting the players keyboard inputs in the horizontal and vertical axis
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Initializing jump
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            // Using invoke it can queue a jump to happen right when it is able to, making it more responsive to the players input
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    // MovePlayer function takes the player input from the PlayerInput function and creates a movement vector based upon the orientation of the player
    // This insures that the movement always is based on where the player is looking
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        } else
        {
            animator.SetBool("isMoving", false);
        }

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    // SpeedControl function is used to limit the player characters speed to a certain limit, making it easier to define
    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    // Function used to make the player character jump
    private void Jump()
    {
        // resets the y velocity by setting it to 0, making sure the jump is always the same amount
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // The player character jumps by applying a force to the rigidbody in the upwards direction, using the forcemode impulse because it only happens once
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        animator.SetTrigger("Jump");
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
