using Project.Entity.Player.Core;
using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public abstract class PlayerAbilityState : IState
    {
        // Core Components
        protected PlayerCore core;
        protected Movement movement;
        protected Senses senses;

        // protected References
        protected PlayerAbilityStateMachine stateMachine;
        protected PlayerDataSO playerData;
        protected Animator animator;
        protected PlayerInput input;

        protected float stateStartTime;

        public PlayerAbilityState(
            PlayerAbilityStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator)
        {
            this.stateMachine = stateMachine;
            this.playerData = playerData;
            this.input = input;
            this.animator = animator;

            core = stateMachine.Player.Core;

            movement = core.GetCoreComponent<Movement>();
            senses = core.GetCoreComponent<Senses>();
        }

        public virtual void Enter() => stateStartTime = Time.time;

        public virtual void StateUpdate() { }

        public virtual void Exit() { }

        public virtual void CheckTransitions() { }
    }
}
