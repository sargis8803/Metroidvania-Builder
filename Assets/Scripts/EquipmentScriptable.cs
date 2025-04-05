using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentScriptable", menuName = "Scriptable Objects/EquipmentScriptable")]
public class EquipmentScriptable : ScriptableObject
{
    public string itemName;
    public int playerAtkDamage, playerMaxHealth, playerMaxJumps;
    public float playerAtkRange, playerSpeed, playerJumpForce;

    /* List other player attributes here
     * MaxAmmo?
     * AtkSpeed?
     * 
     */

    //Updates all player stats when new item is equipped
    public void EquipItem()
    {
        PlayerStats playerstats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerstats.playerAtkDamage += playerAtkDamage;
        playerstats.playerMaxHealth += playerMaxHealth;
        playerstats.playerMaxJumps += playerMaxJumps;
        playerstats.playerAtkRange += playerAtkRange;
        playerstats.playerWalkSpeed += playerSpeed;        
        playerstats.playerJumpForce += playerJumpForce;
    }

    public void UnEquipItem()
    {
        PlayerStats playerstats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerstats.playerAtkDamage -= playerAtkDamage;
        playerstats.playerMaxHealth -= playerMaxHealth;
        playerstats.playerMaxJumps -= playerMaxJumps;
        playerstats.playerAtkRange -= playerAtkRange;
        playerstats.playerWalkSpeed -= playerSpeed;
        playerstats.playerJumpForce -= playerJumpForce;
    }

}
