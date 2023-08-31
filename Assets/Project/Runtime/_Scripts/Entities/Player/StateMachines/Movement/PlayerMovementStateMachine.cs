using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public Player Player { get; }

        // Cashed Movement States
        public PlayerIdleState IdleState { get; }
        public PlayerMoveState MoveState { get; }
        public PlayerInAirState InAirState { get; }
        public PlayerLandingState LandingState { get; }        

        public PlayerMovementStateMachine(Player player, EntityDataSO playerData, PlayerInput input, Animator animator)
        {
            Player = player;

            // Movement States
            IdleState = new PlayerIdleState(this, playerData, input, animator);
            MoveState = new PlayerMoveState(this, playerData, input, animator);
            InAirState = new PlayerInAirState(this, playerData, input, animator);
            LandingState = new PlayerLandingState(this, playerData, input, animator);
        }
    }
}
