using UnityEngine;

namespace Project.StateMachine.Player
{
    public class PlayerRunningState : PlayerGroundedState
    {
        public PlayerRunningState(PlayerMovementStateMachine stateMachine, PlayerData playerData) : base(stateMachine, playerData)
        { }

        public override void Enter()
        {
            base.Enter();

            playerData.TargetMoveSpeed = playerData.RunSpeed;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (playerInput.MoveInput == Vector2.zero && playerData.CurrentMoveSpeed == 0) stateMachine.ChangeState(stateMachine.IdleState);
            else if (!playerInput.Run) stateMachine.ChangeState(stateMachine.WalkingState);
        }
    }
}
