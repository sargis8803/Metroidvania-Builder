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

    // Called once per frame.
    void Update()
    {
        // If the left mouse button is clicked, trigger an attack.
        if (Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("Attack triggered!");
            animator.SetTrigger("Attack"); // Plays the Attack animation.
        }
    }

    // Function to detect and damage enemies within the attack range.
    void Attack()
    {
        // Detect all enemies within the attack range.
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        Debug.Log("Enemies hit: " + hitEnemies.Length);

        // Loops through each detected enemy and applies damage.
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Enemy hit: " + enemy.gameObject.name);

            // Calls the TakeDamage method in the Enemy script.
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
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
}