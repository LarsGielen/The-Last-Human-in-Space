using UnityEngine;

namespace Project.Player.Statemachine
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public Player Player { get; }

        // Cashed Movement States
        public PlayerIdleState IdleState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerRunningState RunningState { get; }
        public PlayerJump JumpingState { get; }
        public PlayerFallingState FallingState { get; }
        public PlayerLandingState LandingState { get; }

        // Attacking states
        public PlayerRangedAttackState AttackingState { get; }

        public PlayerMovementStateMachine(Player player, PlayerDataSO playerData, PlayerInput input, Animator animator)
        {
            Player = player;

            // Movement States
            IdleState = new PlayerIdleState(this, playerData, input, animator);
            WalkingState = new PlayerWalkingState(this, playerData, input, animator);
            RunningState = new PlayerRunningState(this, playerData, input, animator);
            JumpingState = new PlayerJump(this, playerData, input, animator);
            FallingState = new PlayerFallingState(this, playerData, input, animator);
            LandingState = new PlayerLandingState(this, playerData, input, animator);

            // Attacking States
            AttackingState = new PlayerRangedAttackState(this, playerData, input, animator, player.Weapon);
        }
    }
}
