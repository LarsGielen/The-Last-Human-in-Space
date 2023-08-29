using Project.Player.Core;
using UnityEngine;

namespace Project.Player.Statemachine
{
    public abstract class PlayerMovementState : IState
    {
        // Core Components
        protected PlayerCore core;

        protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
        private Movement movement;

        protected Senses Senses { get => senses ??= core.GetCoreComponent<Senses>(); }
        private Senses senses;

        // protected References
        protected PlayerMovementStateMachine stateMachine;
        protected PlayerDataSO playerData;
        protected Animator animator;
        protected PlayerInput input;

        protected float stateStartTime;

        public PlayerMovementState(PlayerMovementStateMachine stateMachine, PlayerDataSO playerData, PlayerInput input, Animator animator)
        {
            this.stateMachine = stateMachine;
            this.playerData = playerData;
            this.input = input;
            this.animator = animator;

            core = stateMachine.Player.Core;
        }

        public virtual void Enter() => stateStartTime = Time.time;

        public virtual void StateUpdate() => animator.SetFloat("MoveSpeed", Movement.CurrentMoveSpeed);

        public virtual void Exit() { }

        public virtual void CheckTransitions() { }
    }
}
