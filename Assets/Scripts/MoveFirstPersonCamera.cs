using Unity.VisualScripting;
using UnityEngine;
//Ensures a CapsuleCollider and a Rigidbody component is attached to the GameObject
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class MoveFirstPersonCamera : MonoBehaviour
{
    // Reference to the Rigidbody component
    Rigidbody rb;
    // Grouping movement-related settings in the Inspector, movement speed and jump force
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    // Grouping ground check-related settings in the Inspector. Height of player for ground detection and layer mask for ground 
    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    // Tracks whether the player is currently grounded and if allowed to jump
    bool grounded;
    bool canJump;

    void Start()
    {
        // Get the Rigidbody component attached to the GameObject and prevent rotation of rb for player stability 
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Get player input for horizontal and vertical movement
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        // Calculate movement vector based on input
        Vector3 moveVector = (transform.forward * vInput) + (transform.right * hInput);
        // Normalize movement vector if its magnitude is greater than 1
        if (moveVector.magnitude > 1f)
            moveVector = moveVector.normalized;
        // Scale the movement vector by the movement speed
        moveVector *= moveSpeed;
        // Perform a ground check using a raycast to determine if the player is grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight * 0.5f) + 0.2f, whatIsGround);
        // Handle movement based on whether the player is grounded
        if (grounded)
        {
            // Allow jumping if grounded, and horizontal movement while maintaining vertical velocity
            canJump = true;
            rb.linearVelocity = new Vector3(moveVector.x, rb.linearVelocity.y, moveVector.z);
        }
        else
        {
            // Apply horizontal movement in the air
            rb.linearVelocity = new Vector3(moveVector.x, rb.linearVelocity.y, moveVector.z);
        }

        // Handle jumping input
        if (grounded && canJump && Input.GetButtonDown("Jump"))
        {
            // Apply an upward force to jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Prevent further jumps until grounded
            canJump = false;
        }
    }
}
