using UnityEngine;

using Project.Entity.CoreSystem;

namespace Project.Entity.Player.Statemachine
{
    public abstract class PlayerMovementState : IState
    {
        // Core Components
        protected CoreSystem.Core core;
        protected Movement movement;
        protected Senses senses;

        // protected References
        protected PlayerMovementStateMachine stateMachine;
        protected EntityDataSO playerData;
        protected Animator animator;
        protected PlayerInput input;

        public PlayerMovementState(
            PlayerMovementStateMachine stateMachine,
            EntityDataSO playerData,
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

        public virtual void Enter() { }

        public virtual void StateUpdate() { }

        public virtual void Exit() { }

        public virtual void CheckTransitions() { }
    }
}
