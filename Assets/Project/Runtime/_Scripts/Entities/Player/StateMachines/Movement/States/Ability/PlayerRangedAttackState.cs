using UnityEngine;
using Project.Weapons;

namespace Project.StateMachine.Player
{
    public class PlayerRangedAttackState : PlayerAbilityState
    {
        private Weapon weapon;

        public PlayerRangedAttackState(PlayerMovementStateMachine stateMachine, PlayerData playerData, Weapon weapon) : base(stateMachine, playerData)
        { 
            this.weapon = weapon;
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (!playerInput.Attack) stateMachine.ChangeState(stateMachine.IdleState);
        }

        public override void Enter()
        {
            base.Enter();

            weapon.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            weapon.Exit();
        }
    }
}
