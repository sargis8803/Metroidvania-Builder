using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public variables for movement speed and jump force, and max jumps, which is adjustable in Unity.
    public float speed = 5f;
    
    public float jumpForce = 7f;
    
    public int maxJumps = 2;

    // Reference to the Rigidbody2D component for the physics interactions.
    private Rigidbody2D rb;

    // Private variable that tracks how many jumps the player has left.
    private int jumpCount;

    // Called once when the script starts.
    void Start()
    {
        // Gets and stores the Rigidbody2D component attached to the player.
        rb = GetComponent<Rigidbody2D>();
        jumpCount = maxJumps; // Initializes jump count.
    }

    // Called once per frame.
    void Update()
    {
        // Gets the horizontal movement input (-1 for left, 1 for right).
        float moveInput = Input.GetAxis("Horizontal");

        // Apply movement by modifying the player's velocity.
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Checks for jump input (W key or Up Arrow key) and ensures the player has jumps remaining.
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCount > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Upward force to make the player jump.
            jumpCount--; // Reduce jump count when jumping.
        }
    }

    // Detects when the player collides with another object.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with an object tagged as "Ground", they are considered grounded.
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = maxJumps;
        }
    }
}
