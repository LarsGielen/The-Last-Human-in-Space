
namespace Project.StateMachine.Player
{
    public class PlayerFallingState : PlayerAirborneState
    {
        public PlayerFallingState(PlayerMovementStateMachine stateMachine, PlayerData playerData) : base(stateMachine, playerData)
        { }

        public override void Enter()
        {
            base.Enter();

            animator.SetBool("FallingState", true);
        }

        public override void Exit()
        {
            base.Exit();

            animator.SetBool("FallingState", false);
        }

        public override void CheckTransitions()
        {
            if (CheckGrounded() && playerData.CurrentYVelocity < 0.01f) stateMachine.ChangeState(stateMachine.LandingState);

            base.CheckTransitions();
        }
    }
}
