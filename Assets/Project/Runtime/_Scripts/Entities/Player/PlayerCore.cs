using UnityEngine;

using Project.StateMachine.Player;

namespace Project
{
    [RequireComponent(typeof(PlayerInputController))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerCore : MonoBehaviour
    {
        [SerializeField] PlayerData playerData;

        private PlayerMovementStateMachine movementStateMachine;

        public PlayerInputController Input { get; private set; }
        public CharacterController controller { get; private set; }
        public Animator Anim { get; private set; }

        private void Awake()
        {
            Input = GetComponent<PlayerInputController>();
            controller = GetComponent<CharacterController>();
            Anim = GetComponent<Animator>();

            movementStateMachine = new PlayerMovementStateMachine(this, playerData);
        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdleState);
        }

        private void Update() => movementStateMachine.StateUpdate();
    }
}
