using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy health value.
    public int health = 50;

    // Function to apply damage to the enemy.
    public void TakeDamage(int damage)
    {
        // Reduces health by the given damage value.
        health -= damage;

        // If the health drops to 0 or below, call the Die function.
        if (health <= 0)
        {
            Die();
        }
    }

    // Function that handles the enemies death.
    void Die()
    {
        // Destroy the enemy GameObject upon death.
        Destroy(gameObject);
    }
}