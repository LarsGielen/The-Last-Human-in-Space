using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : DamageableEntity
{
    public PlayerHealth() : base(100f, 100f){}

    // Om te testen:
    /*private void Update() {
        TakeDamage(0.5f);
    }
    */

    protected override void Die()
    {
        Debug.Log("The player is dead!");
    }
}

