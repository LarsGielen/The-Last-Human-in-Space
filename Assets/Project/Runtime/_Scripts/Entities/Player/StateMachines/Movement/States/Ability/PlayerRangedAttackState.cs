using UnityEngine;

namespace Project.StateMachine.Player
{
    public class PlayerRangedAttackState : PlayerAbilityState
    {
        private Weapon weapon;

        public PlayerRangedAttackState(PlayerMovementStateMachine stateMachine, PlayerData playerData, Weapon weapon) : base(stateMachine, playerData)
        { 
            this.weapon = weapon;
        }

        public override void Enter()
        {
            base.Enter();

            weapon.Enter();
        }
    }
}
