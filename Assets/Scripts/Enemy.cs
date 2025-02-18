using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy health value.
    public int health = 50;
    private Animator animator;

    private bool isDead = false;

    void Start() 
    {
        animator = GetComponent<Animator>(); // Gets the enemy's Animator component.
    }

    // Function to apply damage to the enemy.
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        // Reduces health by the given damage value.
        health -= damage;
        Debug.Log("Enemy took damage. Health left: " + health);

        // If the health drops to 0 or below, call the Die function.
        if (health > 0)
        {
            animator.SetTrigger("Hurt"); // Plays the hurt animation.
            Invoke(nameof(ResetHurtState), 0.3f); // Reset Hurt after delay
        }
        else 
        {
            Die();
        }
    }

    void ResetHurtState()
    {
        animator.SetBool("Hurt", false);
    }

    // Function that handles the enemies death.
    void Die()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("Die"); // Plays the death animation.
        Invoke(nameof(DisableCollider), 0.5f); 

        // Destroy the enemy GameObject upon death.
        Destroy(gameObject, 1f);
    }

    void DisableCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}