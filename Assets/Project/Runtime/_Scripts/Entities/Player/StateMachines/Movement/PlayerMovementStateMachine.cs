
namespace Project.StateMachine.Player
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public PlayerCore Player { get; }

        // Cashed Movement States
        public PlayerIdleState IdleState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerRunningState RunningState { get; }
        public PlayerJump JumpingState { get; }
        public PlayerFallingState FallingState { get; }
        public PlayerLandingState LandingState { get; }

        // Attacking states
        public PlayerRangedAttackState AttackingState { get; }

        public PlayerMovementStateMachine(PlayerCore player, PlayerData playerData)
        {
            Player = player;

            // Movement States
            IdleState = new PlayerIdleState(this, playerData);
            WalkingState = new PlayerWalkingState(this, playerData);
            RunningState = new PlayerRunningState(this, playerData);
            JumpingState = new PlayerJump(this, playerData);
            FallingState = new PlayerFallingState(this, playerData);
            LandingState = new PlayerLandingState(this, playerData);

            // Attacking States
            AttackingState = new PlayerRangedAttackState(this, playerData, player.Weapon);
        }
    }
}
