using UnityEngine;

namespace Project.StateMachine.Player
{
    public abstract class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;
        protected PlayerData playerData;
        protected PlayerInputController playerInput;
        protected Animator animator;

        protected float stateStartTime;

        private CharacterController controller;

        private float moveSpeed;
        private float targetRotation;

        private Vector3 currentMovement;
        private float rotationVelocity;

        public PlayerMovementState(PlayerMovementStateMachine stateMachine, PlayerData playerData)
        {
            this.stateMachine = stateMachine;
            this.playerData = playerData;

            playerInput = stateMachine.Player.Input;
            controller = stateMachine.Player.controller;
            animator = stateMachine.Player.Anim;
        }

        public virtual void Enter()
        {
            stateStartTime = Time.time;

            Debug.Log($"State: {GetType().Name}");
        }

        public virtual void Exit() { }

        protected bool CheckGrounded()
        {
            Transform playerTransform = stateMachine.Player.transform;

            Vector3 spherePosition = new Vector3(playerTransform.position.x, playerTransform.position.y - playerData.GroundedOffset, playerTransform.position.z);
            return Physics.CheckSphere(spherePosition, playerData.GroundedRadius, playerData.GroundLayers, QueryTriggerInteraction.Ignore);
        }

        public virtual void StateUpdate()
        {
            HandleHorizontalMove();
            HandleGravity();

            ApplyMovement();

            animator.SetFloat("MoveSpeed", playerData.CurrentMoveSpeed);
        }

        public virtual void CheckTransitions() { }

        private void HandleHorizontalMove()
        {
            Vector2 moveDirection = playerInput.MoveInput;
            float targetSpeed = playerData.TargetMoveSpeed;

            float currentRotation = stateMachine.Player.transform.eulerAngles.y;

            if (moveDirection == Vector2.zero) targetSpeed = 0f;

            float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude;
            float inputMagnitude = playerInput.AnalogMovement ? playerInput.MoveInput.magnitude : 1f;

            if (currentHorizontalSpeed < targetSpeed - 0.1f || currentHorizontalSpeed > targetSpeed + 0.1f)
            {
                moveSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * playerData.SpeedChangeRate);
                moveSpeed = Mathf.Round(moveSpeed * 1000) / 1000;
            }
            else
            {
                moveSpeed = targetSpeed;
            }

            Vector3 inputDirection = new Vector3(moveDirection.x, 0f, moveDirection.y).normalized;

            if (moveDirection != Vector2.zero)
            {
                float cameraOffset = playerInput.MoveRelativeToCamera ? Camera.main.transform.eulerAngles.y : 0;

                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraOffset;
                float rotation = Mathf.SmoothDampAngle(currentRotation, targetRotation, ref rotationVelocity, playerData.RotationSmoothTime);

                // Set player rotation
                stateMachine.Player.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            Vector3 moveVelocity = targetDirection.normalized * moveSpeed;
            currentMovement = new Vector3(moveVelocity.x, currentMovement.y, moveVelocity.z);
            playerData.CurrentMoveSpeed = moveSpeed;
        }

        private void HandleGravity()
        {
            float previousYVelocity = playerData.CurrentYVelocity;
            playerData.CurrentYVelocity = playerData.CurrentYVelocity - (playerData.CurrentGravity * Time.deltaTime);
            currentMovement.y = Mathf.Max((previousYVelocity + playerData.CurrentYVelocity) * 0.5f, -playerData.TerminalVelocity);
        }

        private void ApplyMovement()
        {
            controller.Move(currentMovement * Time.deltaTime);
        }
    }
}
