using UnityEngine;

namespace Project.Player.Statemachine
{
    public abstract class PlayerAbilityState : PlayerMovementState
    {
        protected bool isAbilityDone;

        public PlayerAbilityState(
            PlayerMovementStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void Enter()
        {
            base.Enter();

            isAbilityDone = false;
        }

        public override void CheckTransitions()
        {
            if (!isAbilityDone) return;

            if (senses.CheckGrounded()) stateMachine.ChangeState(stateMachine.LandingState);
            else stateMachine.ChangeState(stateMachine.FallingState);

            base.CheckTransitions();
        }
    }
}
