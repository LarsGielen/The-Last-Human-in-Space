using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerEmptyAbilityState : PlayerAbilityState
    {
        public PlayerEmptyAbilityState(
            PlayerAbilityStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void Enter()
        {
            base.Enter();

            input.Jump = false;
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (input.Attack) stateMachine.ChangeState(stateMachine.Attack);
            else if (input.Jump && senses.CheckGrounded()) stateMachine.ChangeState(stateMachine.Jump);
        }
    }
}
