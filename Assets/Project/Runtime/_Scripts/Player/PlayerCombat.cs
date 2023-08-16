using Project.Weapons;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(PlayerInputController))]
    public class PlayerCombat : MonoBehaviour
    {
        private bool isAttacking;

        // References
        private Weapon weapon;
        private PlayerInputController input;

        // Methods
        private void Start()
        {
            weapon = GetComponentInChildren<Weapon>();
            input = GetComponent<PlayerInputController>();
        }

        private void Update() => Attack();

        private void Attack()
        {
            if (input.Attack && !isAttacking)
            {
                isAttacking = true;
                weapon.Trigger();
            }
        }
    }
}
