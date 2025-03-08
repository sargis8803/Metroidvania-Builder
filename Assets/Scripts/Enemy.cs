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

    public Transform attackPoint;

    public LayerMask playerLayer;

    // Called when the script instance is being loaded.
    void Start() 
    {
        // Gets the Animator component attached to this GameObject for the animations.
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (!isDead && canAttack && Time.time >= nextAttackTime)
        {
            TryAttackPlayer();
        }
    }

    void TryAttackPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

    if (playerCollider)
    {
        PlayerCombat player = playerCollider.GetComponent<PlayerCombat>();

        // Check if the player exists and is NOT dead
        if (player != null && !player.IsDead())  
        {
            StartCoroutine(Attack());
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}

    private IEnumerator Attack()
    {
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.75f);

        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (player)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
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

    // Coroutine that handles the cooldown after the enemy takes damage
    private IEnumerator HandleDamageCooldown()
    {
        canAttack = false; // Prevent attack
        yield return new WaitForSeconds(takeDamageCooldown); // Wait for cooldown period
        canAttack = true; // Enable attack again
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
