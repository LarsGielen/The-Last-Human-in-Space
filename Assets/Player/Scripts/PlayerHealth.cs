using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player health settings")]
    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private float deathThresholdValue = 0f;

    private bool isDead;

    // Om te testen:
    /* private void Update() {
        if(!isDead){
            Debug.Log("" + playerHealth);
            TakeDamage(0.5f);
        }
    }
    */
    public float CurrentPlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        playerHealth -= damageAmount;

        if (playerHealth <= deathThresholdValue) Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("The player is dead!");
    }
}
