using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    // Reference Player stats to get changeable attributes
    private PlayerStats playerStats;

    // Reference to the Animator component for handling attack animations.
    public Animator animator;

    // Reference to the attack point where the attack will be detected.
    public Transform attackPoint;

    // LayerMask to determine which objects should be detected as enemies.
    public LayerMask enemyLayers;

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

    public bool isBlocking = false;  

    public KeyCode blockKey = KeyCode.E;

    public float blockDuration = 0.5f; 

    private bool canBlock = true; 

     void Start()
    {
        // Get the PlayerStats component attached to the same GameObject
        playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();

        currentHealth = playerStats.playerMaxHealth;
        currentAmmo = maxAmmo;
    }

    // Called once per frame.
    void Update()
    {
        if (isDead) return;

        // Handle block input
        if (Input.GetKey(blockKey) && canBlock)
        {
            if (!isBlocking) // Start blocking only if not already blocking.
            {
                StartCoroutine(Block());
            }
        }
        else if (isBlocking) // Stop blocking if the key is released.
        {
            isBlocking = false;
        }

        if (Input.GetMouseButtonDown(0)) // Left Click.
        {
            Debug.Log("Attack triggered!");
            PerformComboAttack();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (Time.time >= lastRangedAttackTime + rangedCooldown && currentAmmo > 0)
            {
                animator.SetTrigger("Shooting"); // Triggers the shooting animation.
                lastRangedAttackTime = Time.time;
        }
    }
}

    private IEnumerator Block()
    {
        isBlocking = true;
        animator.SetTrigger("Block"); // Trigger block animation.
        animator.SetBool("IsBlocking", true); // Set IsBlocking to true for continuous blocking state.

        // Prevent multiple blocks in quick succession.
        canBlock = false;

        // Loop while the player is holding the block key.
        while (Input.GetKey(blockKey)) 
        {
            yield return null; // Wait for the next frame to check the key.
        }

        // When the player releases the block key, stop blocking.
        animator.SetBool("IsBlocking", false); 

        // Cooldown for blocking.
        yield return new WaitForSeconds(1f);
        canBlock = true;
        isBlocking = false;
}

    void RangedAttack()
    {
        // Checks if the projectile prefab and fire point are properly assigned.
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point not assigned!");
            return;
        }

        // Instantiates a new projectile at the fire point's position and rotation.
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        
        // Determine shooting direction based on where the player is facing.
        float direction = transform.localScale.x > 0 ? 1f : -1f;

        rb.linearVelocity = new Vector2(direction * projectileSpeed, 0f); // Adds velocity to the projectile.


        currentAmmo--; // Reduce ammo count.
        Debug.Log("Ranged attack fired! Ammo left: " + currentAmmo);
    }

    public void RefillAmmo(int amount)
    {
        // Adds ammo but doesnt exceed maxAmmo limit.
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        Debug.Log("Ammo refilled! Current ammo: " + currentAmmo);
    }

    void PerformComboAttack()
    {
        // Calculates time since the last attack was performed.
        float timeSinceLastAttack = Time.time - lastAttackTime;

        if (timeSinceLastAttack > comboResetTime)
        {
            comboStep = 0; // Reset combo if time between attacks is too long.
        }

        comboStep++; // Move to next combo step.

        if (comboStep == 1)
        {
            animator.SetTrigger("Attack");
        }
        else if (comboStep == 2)
        {
            animator.SetTrigger("Attack2");
        }

        lastAttackTime = Time.time; // Update last attack time.
    }

    // Function to handle attacking mechanics.
    void Attack()
    {

        // Detect all enemies within the attack range.
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, playerStats.playerAtkRange, enemyLayers);

        Debug.Log("Enemies hit: " + hitEnemies.Length);

        // Loops through each detected enemy and applies damage.
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Enemy hit: " + enemy.gameObject.name);
            enemy.GetComponent<Enemy>().TakeDamage(playerStats.playerAtkDamage); // Calls the TakeDamge method in the Enemy script.
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        if (isBlocking)
        {
            damage = 0;  // Blocks the damage.
            Debug.Log("Player blocked the attack!");
        }

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
        Gizmos.DrawWireSphere(attackPoint.position, playerStats.playerAtkRange);
    }

    public bool IsDead()
    {

    return isDead;
    
    }
    
}