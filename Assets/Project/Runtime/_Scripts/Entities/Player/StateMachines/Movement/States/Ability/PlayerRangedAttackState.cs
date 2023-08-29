using UnityEngine;
using Project.Weapons;

namespace Project.Player.Statemachine
{
    public class PlayerRangedAttackState : PlayerAbilityState
    {
        private Weapon weapon;

        public PlayerRangedAttackState(
            PlayerMovementStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator,
            Weapon weapon) : base(stateMachine, playerData, input, animator)
        { 
            this.weapon = weapon;
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (!input.Attack) stateMachine.ChangeState(stateMachine.IdleState);
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
