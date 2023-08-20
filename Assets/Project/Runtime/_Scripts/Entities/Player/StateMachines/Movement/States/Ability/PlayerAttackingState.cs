using UnityEngine;

namespace Project.StateMachine.Player
{
    public class PlayerAttackingState : PlayerAbilityState
    {
        public PlayerAttackingState(PlayerMovementStateMachine stateMachine, PlayerData playerData, Weapon weapon) : base(stateMachine, playerData)
        { }
    }
}
