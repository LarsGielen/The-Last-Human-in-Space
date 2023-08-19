
namespace Project.StateMachine.Player
{
    public class PlayerLandingState : PlayerGroundedState
    {
        public PlayerLandingState(PlayerMovementStateMachine stateMachine, PlayerData playerData) : base(stateMachine, playerData)
        { }

        public override void Enter()
        {
            base.Enter();

            animator.SetTrigger("LandingState");
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
