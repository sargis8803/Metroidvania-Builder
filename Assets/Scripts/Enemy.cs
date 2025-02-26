using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy health value.
    public int health = 50;
    
    // Reference to the Animator component for the enemy animations.
    private Animator animator;

    // Indicates whether the enemy is dead; prevents further damage or actions.
    private bool isDead = false;

    // Called when the script instance is being loaded.
    void Start() 
    {
        // Gets the Animator component attached to this GameObject for the animations.
        animator = GetComponent<Animator>(); 
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
            Invoke(nameof(ResetHurtState), 0.3f); // Resets the hurt animation after a delay.
        }
        else 
        {
            // If the health is zero or below, it calls the death method.
            Die();
        }
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
}