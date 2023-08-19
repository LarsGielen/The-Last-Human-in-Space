
namespace Project.StateMachine.Player
{
    public class PlayerAirborneState : PlayerMovementState
    {
        public PlayerAirborneState(PlayerMovementStateMachine stateMachine, PlayerData playerData) : base(stateMachine, playerData)
        { }

        public override void Enter()
        {
            base.Enter();

            playerData.CurrentGravity = playerData.Gravity;
        }
    }
}
