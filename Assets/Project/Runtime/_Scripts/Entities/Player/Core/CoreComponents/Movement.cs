using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Core
{
    public class Movement : CoreComponent
    {
        private float targetRotation;
        private float currentRotationVelocity;

        private float currentMoveSpeed;
        private Vector3 currentVelocity;
        private float currentGravity;

        // References
        private CharacterController controller;
        private PlayerDataSO playerData;

        // Properties
        public float CurrentMoveSpeed { get => currentMoveSpeed; }
        public Vector3 CurrentVelocity { get => currentVelocity; }
        public float CurrentVerticalVelocity { get => currentVelocity.y; }

        // Methods
        protected override void Awake()
        {
            base.Awake();

            playerData = core.PlayerData;
            controller = GetComponentInParent<CharacterController>();
        }

        public void SetHorizontalMove(Vector2 moveDirection, float targetSpeed, bool relativeToCamera = true)
        {
            float currentRotation = core.parentTransform.eulerAngles.y;
            if (moveDirection == Vector2.zero) targetSpeed = 0f;

            float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude;

            if (currentHorizontalSpeed < targetSpeed - 0.1f || currentHorizontalSpeed > targetSpeed + 0.1f)
            {
                currentMoveSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * playerData.SpeedChangeRate);
                currentMoveSpeed = Mathf.Round(currentMoveSpeed * 1000) / 1000;
            }
            else
            {
                currentMoveSpeed = targetSpeed;
            }

            Vector3 inputDirection = new Vector3(moveDirection.x, 0f, moveDirection.y).normalized;

            if (moveDirection != Vector2.zero)
            {
                float cameraOffset = relativeToCamera ? Camera.main.transform.eulerAngles.y : 0;

                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraOffset;
                float rotation = Mathf.SmoothDampAngle(currentRotation, targetRotation, ref currentRotationVelocity, playerData.RotationSmoothTime);

                // Set player rotation
                core.parentTransform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            Vector3 moveVelocity = targetDirection.normalized * currentMoveSpeed;
            currentVelocity = new Vector3(moveVelocity.x, currentVelocity.y, moveVelocity.z);
        }

        public void SetVerticalVelocity(float velocityValue)
        {
            currentVelocity.y = velocityValue;
        }

        public void SetGravity(float gravityValue) => currentGravity = gravityValue;

        public void HandleGravity()
        {
            float previousYVelocity = currentVelocity.y;
            float newVelocity = currentVelocity.y - (currentGravity * Time.deltaTime);
            currentVelocity.y = Mathf.Max((previousYVelocity + newVelocity) * 0.5f, -playerData.TerminalVelocity);
        }

        public void ApplyMovement()
        {
            controller.Move(currentVelocity * Time.deltaTime);
        }
    }
}
