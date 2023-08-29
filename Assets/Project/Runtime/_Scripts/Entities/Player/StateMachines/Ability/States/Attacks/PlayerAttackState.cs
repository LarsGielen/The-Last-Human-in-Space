using UnityEngine;
using Project.Weapons;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerAttackState : PlayerAbilityState
    {
        private Weapon weapon;

        public PlayerAttackState(
            PlayerAbilityStateMachine stateMachine,
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

            if (!input.Attack) stateMachine.ChangeState(stateMachine.EmptyAbility);
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
