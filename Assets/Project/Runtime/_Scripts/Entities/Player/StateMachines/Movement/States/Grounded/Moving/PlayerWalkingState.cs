using UnityEngine;

namespace Project.StateMachine.Player
{
    public class PlayerWalkingState : PlayerGroundedState
    {
        public PlayerWalkingState(PlayerMovementStateMachine stateMachine, PlayerData playerData) : base(stateMachine, playerData)
        { }

        public override void Enter()
        {
            base.Enter();

            playerData.TargetMoveSpeed = playerData.WalkSpeed;
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (playerInput.MoveInput == Vector2.zero && playerData.CurrentMoveSpeed == 0) stateMachine.ChangeState(stateMachine.IdleState);
            else if (playerInput.Run) stateMachine.ChangeState(stateMachine.RunningState);
        }
    }
}
