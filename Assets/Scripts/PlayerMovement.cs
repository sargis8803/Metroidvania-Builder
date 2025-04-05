using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference Player stats to get changeable attributes
    private PlayerStats playerStats;

    private Animator animator;

    // Reference to the Rigidbody2D component for the physics interactions.
    private Rigidbody2D rb;

    // Private variable that tracks how many jumps the player has left.
    private int jumpCount;

    // Boolean value to check if a player has jumped, used to prevent infinite jumps by unequipping and reequipping items.
    private bool hasJumped;

    // Called once when the script starts.
    void Start()
    {
        // Get the PlayerStats component attached to the same GameObject
        playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();

        // Gets and stores the Rigidbody2D component attached to the player.
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        jumpCount = playerStats.playerMaxJumps;

        // Initializes hasJumped to false.
        hasJumped = false;
    }

    // Called once per frame to handle player input.
    void Update()
    {
        // Gets the horizontal movement input (-1 for left, 1 for right).
        float moveInput = Input.GetAxis("Horizontal");

        bool isRunning = Input.GetKey(KeyCode.R);
        float currentSpeed = isRunning ? playerStats.playerRunSpeed : playerStats.playerWalkSpeed;

        // Apply movement by modifying the player's velocity.
        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);

        // Set animation parameters
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("IsRunning", Input.GetKey(KeyCode.R));

        // Flip player sprite based on direction
        if (moveInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(moveInput) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        // Checks for jump input (W key or Up Arrow key) and ensures the player has jumps remaining.
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCount > 0)
        {
            // Apply an upward force to make the player jump.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerStats.playerJumpForce);

            jumpCount--; // Reduce jump count when jumping.

            hasJumped = true;
        }
    }

    // Detects when the player collides with another object.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with an object tagged as "Ground", they are considered grounded.
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("tPlat") || collision.gameObject.CompareTag("platform") || collision.gameObject.CompareTag("Square"))
        {
            jumpCount = playerStats.playerMaxJumps;
            hasJumped = false;
        }
    }

    // Updates jumpCount variable after equipping or unequipping an item that changes the value of playerMaxJumps
    public void UpdateJumpCount()
    {
        if (!hasJumped) { jumpCount = playerStats.playerMaxJumps; }
    }
}
