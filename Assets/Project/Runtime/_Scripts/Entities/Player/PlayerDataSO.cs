using UnityEngine;

namespace Project.Entity.Player
{
    [CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player Data")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float walkSpeed = 2f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float speedChangeRate = 10f;
        [SerializeField] private float rotationSmoothTime = 0.12f;

        [Header("Jump")]
        [SerializeField] private float jumpHeight = 1.2f;
        [SerializeField] private float jumpTime = 0.5f;
        [SerializeField] private float jumpTimeOut = 0.50f;

        [Header("Gravity")]
        [SerializeField] private float gravity = 15f;
        [SerializeField] private float groundedGravity = 5f;
        [SerializeField] private float gravityMultiplierAfterJump = 5f;
        [SerializeField] private float terminalVelocity = 53f;
        [SerializeField] private float fallTimeout = 0.5f;

        [Header("Grounded Check")]
        [SerializeField] private float groundedOffset = -0.15f;
        [SerializeField] private float groundedRadius = 0.28f;
        [SerializeField] private LayerMask groundLayers;

        // Properties

        // Movement
        public float WalkSpeed { get => walkSpeed; }
        public float RunSpeed { get => runSpeed; }
        public float SpeedChangeRate { get => speedChangeRate; }
        public float RotationSmoothTime { get => rotationSmoothTime; }

        // Jump
        public float JumpHeight { get => jumpHeight; }
        public float JumpTime { get => jumpTime; }
        public float GravityMultiplierAfterJump { get => gravityMultiplierAfterJump; }
        public float JumpTimeout { get => jumpTimeOut; }

        // Gravity
        public float Gravity { get => gravity; }
        public float GroundedGravity { get => groundedGravity; }
        public float TerminalVelocity { get => terminalVelocity; }
        public float FallTimeout { get => fallTimeout; }

        // Grounded Check
        public float GroundedOffset { get => groundedOffset; }
        public float GroundedRadius { get => groundedRadius; }
        public LayerMask GroundLayers { get => groundLayers; }
    }
}