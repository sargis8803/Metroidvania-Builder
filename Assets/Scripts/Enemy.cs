using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // Enemy health value.
    public int health = 50;

    public int attackDamage = 10;

    public float attackRange = 0.5f;

    public float attackCooldown = 3f;

    private float nextAttackTime = 0f;

    public float takeDamageCooldown = 1f;

    private bool canAttack = true;

    // Indicates whether the enemy is dead; prevents further damage or actions.
    private bool isDead = false;

   // Reference to the Animator component for the enemy animations.
    private Animator animator;

   // The point from where the enemy checks for players to attack.
    public Transform attackPoint;

    // The player layer mask to detect collisions only with the player.
    public LayerMask playerLayer;

    // Reference to the player transform to track position.
    private Transform player;

    // Called when the script instance is being loaded.
    void Start() 
    {
        // Gets the Animator component attached to this GameObject for the animations.
        animator = GetComponent<Animator>(); 

        
        // Find the player in the scene by tag and store its transform.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Face the player based on their position relative to the enemy.
        if (player != null)
        {
            // Flips the enemy's sprite to face the player.
            Vector3 scale = transform.localScale;
            if (player.position.x < transform.position.x)
                scale.x = Mathf.Abs(scale.x); // Faces left.
            else
                scale.x = -Mathf.Abs(scale.x); // Faces right.
            transform.localScale = scale;
        }

         // Checks if enemy can attack.
        if (!isDead && canAttack && Time.time >= nextAttackTime)
        {
            TryAttackPlayer();
        }
    }

    // Tries to detect and attack the player if its within range.
    void TryAttackPlayer()
    {
        // Check if a player is within attack range using a circle overlap.
        Collider2D playerCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

    if (playerCollider)
    {
        // Gets the PlayerCombat script to apply damage.
        PlayerCombat player = playerCollider.GetComponent<PlayerCombat>();

        // Checks if the player exists and is not dead.
        if (player != null && !player.IsDead())  
        {
            // Starts the attack animation and logic.
            StartCoroutine(Attack());
            nextAttackTime = Time.time + attackCooldown; // Sets the next time the enemy is allowed to attack.
        }
    }
}

    // Coroutine that handles the actual attack animation and damage logic.
    private IEnumerator Attack()
    {
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.75f); // Wait until the attack animation is done.

        // Re-checks if the player is still within attack range at the moment of impact.
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (player)
        {
            PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();

            // Only apply damage if the player exists, is alive and not blocking.
            if (playerCombat != null && !playerCombat.IsDead() && !playerCombat.isBlocking)  
            {
                playerCombat.TakeDamage(attackDamage); // Deals damage.
            }
            else
            {
                Debug.Log("Enemy attack blocked!");
            }
        }
    }

    // Method that inflicts damage to the enemy.
    public void TakeDamage(int damage)
    {
        // If the enemy is already dead, no nothing.
        if (isDead) return;

        // Reduces health by the given damage value.
        health -= damage;
        Debug.Log("Enemy took damage. Health left: " + health);

        // If the health is still above zero, trigger a hurt animation. 
        if (health > 0)
        {
            animator.SetTrigger("Hurt"); // Plays the hurt animation.
            Invoke(nameof(ResetHurtState), 0.3f); // Reset the hurt animation after delay.

            // Start the cooldown after being hit
            StartCoroutine(HandleDamageCooldown());
        }
        else 
        {
            // If the health is zero or below, it calls the death method.
            Die();
        }
    }

    // Coroutine that handles the cooldown after the enemy takes damage.
    private IEnumerator HandleDamageCooldown()
    {
        canAttack = false; // Prevents attack.
        yield return new WaitForSeconds(takeDamageCooldown); // Waits for the cooldown period.
        canAttack = true; // Enable the attack again.
    }
    
    // Resets the hurt animation state.
    void ResetHurtState()
    {
        animator.SetBool("Hurt", false);
    }

    // Method that handles the enemies death.
    void Die()
    {
        // If already dead do nothing.
        if (isDead) return;

        // Set the isDead flag to true to prevent further actions.
        isDead = true;

        animator.SetTrigger("Die"); // Plays the death animation.

        // Disables the enemy's collider to prevent further interactions.
        Invoke(nameof(DisableCollider), 0.5f); 

        // Destroy the enemy GameObject upon death.
        Destroy(gameObject, 1f);
    }

    // Disables the collider to prevent interactions after the enemy dies.
    void DisableCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }

     void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
