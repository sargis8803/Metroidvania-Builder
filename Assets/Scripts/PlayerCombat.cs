using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Reference to the Animator component for handling attack animations.
    public Animator animator;

    // Reference to the attack point where the attack will be detected.
    public Transform attackPoint;

    // Radius of the attack hitbox.
    public float attackRange = 0.5f;

    // LayerMask to determine which objects should be detected as enemies.
    public LayerMask enemyLayers;

    // Damage value inflicted on enemies.
    public int attackDamage = 20;

    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

     void Start()
    {
        currentHealth = maxHealth;
    }

    // Called once per frame.
    void Update()
    {
        if (isDead) return;

        if (Input.GetMouseButtonDown(0)) // Left Click.
        {
            Debug.Log("Attack triggered!");
            animator.SetTrigger("Attack"); // Plays the Attack animation.
        }
    }

    // Function to handle attacking mechanics.
    void Attack()
    {
        // Detect all enemies within the attack range.
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Log out the number of enemies detected.
        Debug.Log("Enemies hit: " + hitEnemies.Length);

        // Loops through each detected enemy and applies damage.
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Enemy hit: " + enemy.gameObject.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Player took damage. Health left: " + currentHealth);

        animator.SetTrigger("Hurt"); // Plays hurt animation.

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Player has died!");
        animator.SetTrigger("Die"); // Plays death animation.

        GetComponent<PlayerMovement>().enabled = false;
        this.enabled = false;

    }

    // Visual representation of the attack range in Unity.
    void OnDrawGizmosSelected()
    {
        // If the attackPoint is not assigned, do nothing.
        if (attackPoint == null)
            return;
        
        // Draws a wireframe sphere in the editor to show the attack range.
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public bool IsDead()
    {

    return isDead;
    
    }
    
}