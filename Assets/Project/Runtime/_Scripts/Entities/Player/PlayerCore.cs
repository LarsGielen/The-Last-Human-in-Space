using UnityEngine;

using Project.StateMachine.Player;
using Project.Weapons;

namespace Project
{
    [RequireComponent(typeof(PlayerInputController))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerCore : MonoBehaviour
    {
        [SerializeField] PlayerData playerData;

        // State Machines
        private PlayerMovementStateMachine movementStateMachine;

        // Properties
        public PlayerInputController Input { get; private set; }
        public CharacterController controller { get; private set; }
        public Animator Anim { get; private set; }

        public Weapon Weapon { get; private set; }

        private void Awake()
        {
            Input = GetComponent<PlayerInputController>();
            controller = GetComponent<CharacterController>();
            Anim = GetComponent<Animator>();

            Weapon = GetComponentInChildren<Weapon>();

            movementStateMachine = new PlayerMovementStateMachine(this, playerData);
        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdleState);
        }

        private void Update() => movementStateMachine.StateUpdate();
    }
}
