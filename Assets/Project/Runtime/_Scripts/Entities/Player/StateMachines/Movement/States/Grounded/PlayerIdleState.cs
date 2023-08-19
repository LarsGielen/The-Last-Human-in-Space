using UnityEngine;

namespace Project.StateMachine.Player
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(PlayerMovementStateMachine stateMachine, PlayerData playerData) : base(stateMachine, playerData)
        { }

        public override void Enter()
        {
            base.Enter();
      
            playerData.TargetMoveSpeed = 0;
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (playerInput.MoveInput == Vector2.zero) return;

            if (playerInput.Run) stateMachine.ChangeState(stateMachine.RunningState);
            else stateMachine.ChangeState(stateMachine.WalkingState);
        }
    }
}
