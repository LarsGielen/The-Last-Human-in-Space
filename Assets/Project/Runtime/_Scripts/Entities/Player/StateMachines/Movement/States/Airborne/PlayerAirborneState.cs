using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public abstract class PlayerAirborneState : PlayerMovementState
    {
        public PlayerAirborneState(
            PlayerMovementStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }
    }
}
