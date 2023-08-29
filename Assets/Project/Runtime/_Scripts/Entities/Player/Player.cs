using UnityEngine;

using Project.Entity.Player.Statemachine;
using Project.Entity.Player.Core;
using Project.Weapons;

namespace Project.Entity.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public PlayerDataSO PlayerData { get; private set; }

        // Core Components
        public PlayerCore Core { get; private set; }
        private Movement movement;
        
        // Properties
        public Weapon Weapon { get; private set; } // Go to private when ability stateMachine is made

        private PlayerInput input;
        private Animator animator;

        // State Machines
        private PlayerMovementStateMachine movementStateMachine;
        private PlayerAbilityStateMachine abilityStateMachine;

        private void Awake()
        {
            if (PlayerData == null) Debug.LogError("Player has no PlayerDataSO");

            Core = GetComponentInChildren<PlayerCore>();
            movement = Core.GetCoreComponent<Movement>();

            Weapon = GetComponentInChildren<Weapon>();

            input = GetComponent<PlayerInput>();
            animator = GetComponent<Animator>();

            movementStateMachine = new PlayerMovementStateMachine(this, PlayerData, input, animator);
            abilityStateMachine = new PlayerAbilityStateMachine(this, PlayerData, input, animator);
        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdleState);
            abilityStateMachine.ChangeState(abilityStateMachine.EmptyAbility);
        }

        private void Update()
        {
            if (PlayerData == null) return;

            movementStateMachine.StateUpdate();
            abilityStateMachine.StateUpdate();

            movement.HandleGravity();
            movement.ApplyMovement();
        }
    }
}
