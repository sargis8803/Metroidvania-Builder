using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int playerAtkDamage;
    public float playerAtkRange;
    public int playerMaxHealth;
    public float playerSpeed;
    public float playerJumpForce;
    public int playerMaxJumps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAtkDamage = 20;
        playerAtkRange = 0.5f;
        playerMaxHealth = 100;
        playerSpeed = 5f;
        playerJumpForce = 7f;
        playerMaxJumps = 2;
    }
}
