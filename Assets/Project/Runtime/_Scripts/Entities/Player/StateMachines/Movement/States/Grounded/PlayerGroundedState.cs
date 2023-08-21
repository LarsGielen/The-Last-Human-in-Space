using UnityEngine;

namespace Project.StateMachine.Player
{
    public abstract class PlayerGroundedState : PlayerMovementState
    {
        public PlayerGroundedState(PlayerMovementStateMachine stateMachine, PlayerData playerData) : base(stateMachine, playerData)
        { }

        public override void Enter()
        {
            base.Enter();

            // Set gravity to 0 and set small constant down force
            playerData.CurrentGravity = 0;
            playerData.CurrentYVelocity = -playerData.GroundedGravity;
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (playerInput.Jump) stateMachine.ChangeState(stateMachine.JumpingState);
            else if (CheckGrounded() == false) stateMachine.ChangeState(stateMachine.FallingState);

            // Abilities
            else if (playerInput.Attack) stateMachine.ChangeState(stateMachine.AttackingState); 
        }
    }
}
