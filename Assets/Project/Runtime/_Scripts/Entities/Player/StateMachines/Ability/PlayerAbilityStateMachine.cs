using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerAbilityStateMachine : StateMachine
    {
        public Player Player { get; private set; }

        // States
        public PlayerListenState ListenState { get; }
        public PlayerAttackState Attack { get; }
        public PlayerJump Jump { get; }

        public PlayerAbilityStateMachine(Player player, EntityDataSO playerData, PlayerInput input, Animator animator)
        {
            Player = player;

            ListenState = new PlayerListenState(this, playerData, input, animator);
            Attack = new PlayerAttackState(this, playerData, input, animator, player.Ability);
            Jump = new PlayerJump(this, playerData, input, animator);
        }
    }
}
