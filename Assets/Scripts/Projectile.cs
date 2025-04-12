using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Amount of damage the projectile deals.
    public int damage = 10;

    // Time before the projectile is destroyed.
    public float lifetime = 3f;

    void Start()
    {
        // Destroy the projectile after 'lifetime' seconds to prevent it from persisting in the scene.
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the tag "Enemy".
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Projectile hit: " + other.name);

            Debug.Log("Hit an enemy!"); 

            // Try to get the Enemy component from the collided object.
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Apply damage to enemy.
            }
            Destroy(gameObject);
        }
    }
}
