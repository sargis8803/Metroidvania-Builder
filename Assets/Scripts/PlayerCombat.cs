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

    private int comboStep = 0;
    private float lastAttackTime = 0f;
    public float comboResetTime = 0.5f;

    public GameObject projectilePrefab; // The projectile to spawn
    public Transform firePoint; // The point where the projectile is fired from
    public float projectileSpeed = 10f; // Speed of the projectile
    public float rangedCooldown = 1.0f; // Cooldown time between ranged attacks
    private float lastRangedAttackTime = 0f;
    public int maxAmmo = 10; // Maximum ammo count
    private int currentAmmo;

     void Start()
    {
        currentHealth = maxHealth;
        currentAmmo = maxAmmo;
    }

    // Called once per frame.
    void Update()
    {
        if (isDead) return;

        if (Input.GetMouseButtonDown(0)) // Left Click.
        {
            Debug.Log("Attack triggered!");
            PerformComboAttack();
        }
        else if (Input.GetMouseButtonDown(1)) // Right Click for ranged attack
        {

        if (Time.time >= lastRangedAttackTime + rangedCooldown && currentAmmo > 0)
        {
            RangedAttack();
            lastRangedAttackTime = Time.time;
        }
    }
}

    void RangedAttack()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point not assigned!");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * projectileSpeed; // Adjust based on player direction

        animator.SetTrigger("RangedAttack"); // Trigger ranged attack animation

        currentAmmo--; // Reduce ammo count
        Debug.Log("Ranged attack fired! Ammo left: " + currentAmmo);
    }

    public void RefillAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        Debug.Log("Ammo refilled! Current ammo: " + currentAmmo);
    }

    void PerformComboAttack()
    {
        float timeSinceLastAttack = Time.time - lastAttackTime;

        if (timeSinceLastAttack > comboResetTime)
        {
            comboStep = 0; // Reset combo if time between attacks is too long
        }

        comboStep++; // Move to next combo step

        if (comboStep == 1)
        {
            animator.SetTrigger("Attack");
        }
        else if (comboStep == 2)
        {
            animator.SetTrigger("Attack2");
        }

        lastAttackTime = Time.time; // Update last attack time
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