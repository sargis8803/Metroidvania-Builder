using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public variables for movement speed and jump force, which is adjustable in Unity.
    public float speed = 5f;
    public float jumpForce = 7f;

    // Reference to the Rigidbody2D component for the physics interactions.
    private Rigidbody2D rb;

    // Boolean to check if the player is on the ground.
    private bool isGrounded;

    // Called once when the script starts.
    void Start()
    {
        // Gets and stores the Rigidbody2D component attached to the player.
        rb = GetComponent<Rigidbody2D>();
    }

    // Called once per frame.
    void Update()
    {
        // Gets the horizontal movement input (-1 for left, 1 for right).
        float moveInput = Input.GetAxis("Horizontal");

        // Apply movement by modifying the player's velocity.
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Checks if the jump button is pressed and the player is grounded.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Apply an upward force to make the player jump.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // Detects when the player collides with another object.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with an object tagged as "Ground", they are considered grounded.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Detects when the player leaves contact with an object.
    private void OnCollisionExit2D(Collision2D collision)
    {
        // If the player stops colliding with the ground, they are no longer grounded.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}