using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerAbilityStateMachine : StateMachine
    {
        public Player Player { get; private set; }

        // States
        public PlayerEmptyAbilityState EmptyAbility { get; }
        public PlayerAttackState Attack { get; }
        public PlayerJump Jump { get; }

        public PlayerAbilityStateMachine(Player player, PlayerDataSO playerData, PlayerInput input, Animator animator)
        {
            Player = player;

            EmptyAbility = new PlayerEmptyAbilityState(this, playerData, input, animator);
            Attack = new PlayerAttackState(this, playerData, input, animator, player.Weapon);
            Jump = new PlayerJump(this, playerData, input, animator);
        }
    }
}
