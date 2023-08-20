using UnityEngine;

namespace Project.StateMachine.Player
{
    public abstract class PlayerAbilityState : PlayerMovementState
    {
        protected bool isAbilityDone;

        public PlayerAbilityState(PlayerMovementStateMachine stateMachine, PlayerData playerData) : base(stateMachine, playerData)
        { }

        public override void Enter()
        {
            base.Enter();

            isAbilityDone = false;
        }

        public override void CheckTransitions()
        {
            if (!isAbilityDone) return;

            if (CheckGrounded()) stateMachine.ChangeState(stateMachine.LandingState);
            else stateMachine.ChangeState(stateMachine.FallingState);

            base.CheckTransitions();
        }
    }
}
