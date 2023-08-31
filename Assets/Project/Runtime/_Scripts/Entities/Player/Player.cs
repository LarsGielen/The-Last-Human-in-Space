using System.Linq;
using UnityEngine;

using Project.Entity.CoreSystem;
using Project.Entity.Player.Statemachine;
using Project.AbilitySystem;
using System.Collections.Generic;

namespace Project.Entity.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public EntityDataSO PlayerData { get; private set; }

        // Core Components
        public Core Core { get; private set; }
        private Movement movement;
        
        // Properties
        public Ability Ability { get; private set; } // Go to private when ability stateMachine is made

        private PlayerInput input;
        private Animator animator;

        // State Machines
        private Dictionary<StateMachine, bool> stateMachinesLockedDictionary;

        private void Awake()
        {
            if (PlayerData == null) Debug.LogError("Player has no PlayerDataSO");

            Core = GetComponentInChildren<Core>();
            movement = Core.GetCoreComponent<Movement>();

            Ability = GetComponentInChildren<Ability>();
            Ability.Core = Core;

            input = GetComponent<PlayerInput>();
            animator = GetComponent<Animator>();

            // Create Statemachines
            PlayerMovementStateMachine movementStateMachine = new PlayerMovementStateMachine(this, PlayerData, input, animator);
            PlayerAbilityStateMachine abilityStateMachine = new PlayerAbilityStateMachine(this, PlayerData, input, animator);

            // Set start State
            movementStateMachine.ChangeState(movementStateMachine.IdleState);
            abilityStateMachine.ChangeState(abilityStateMachine.ListenState);

            // Store in dictionary
            stateMachinesLockedDictionary = new Dictionary<StateMachine, bool>()
            {
                {movementStateMachine , false},
                {abilityStateMachine , false},
            };
        }

        private void Update()
        {
            if (PlayerData == null) return;

            // Update statemachines
            foreach (StateMachine stateMachine in stateMachinesLockedDictionary.Keys)
                if (!stateMachinesLockedDictionary[stateMachine]) stateMachine.StateUpdate();

            animator.SetFloat("MoveSpeed", movement.MoveSpeed);

            movement.ApplyMovement();
        }

        public T LockStateMachine<T>(bool locked) where T : StateMachine
        {
            var statemachine = stateMachinesLockedDictionary.Keys.OfType<T>().FirstOrDefault();

            if (statemachine != null)
            {
                stateMachinesLockedDictionary[statemachine] = locked;
                return statemachine;
            }

            Debug.LogWarning($"Statemachine {typeof(T).Name} not found in {this.GetType().Name}");
            return null;
        }
    }
}
