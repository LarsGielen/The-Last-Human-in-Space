using Project.Entity.Player.Core;
using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public abstract class PlayerMovementState : IState
    {
        // Core Components
        protected PlayerCore core;
        protected Movement movement;
        protected Senses senses;

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

            movement = core.GetCoreComponent<Movement>();
            senses = core.GetCoreComponent<Senses>();
        }

        public virtual void Enter() => stateStartTime = Time.time;

        public virtual void StateUpdate() => animator.SetFloat("MoveSpeed", movement.CurrentMoveSpeed);

        public virtual void Exit() { }

        public virtual void CheckTransitions() { }
    }
}
