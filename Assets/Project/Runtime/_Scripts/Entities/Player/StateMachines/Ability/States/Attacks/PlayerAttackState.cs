using UnityEngine;
using Project.AbilitySystem;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerAttackState : PlayerAbilityState
    {
        private Ability ability;

        public PlayerAttackState(
            PlayerAbilityStateMachine stateMachine,
            EntityDataSO playerData,
            PlayerInput input,
            Animator animator,
            Ability ability) : base(stateMachine, playerData, input, animator)
        { 
            this.ability = ability;
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (!input.Attack) stateMachine.ChangeState(stateMachine.ListenState);
        }

        public override void Enter()
        {
            base.Enter();

            ability.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            ability.Exit();
        }
    }
}
