using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Entity.CoreSystem
{
    public class Movement : CoreComponent
    {
        private float currentGravity;

          // For moving -> not acurate
        private Vector2 targetMoveVelocity;
        private Vector2 currentMoveVelocity;
        private float currentMoveSpeed;

          // For rotating
        private float targetRotation;
        private float currentRotation;
        private float currentRotationVelocity;

          // For acurate velocity calculations
        private Vector3 currentVelocity;
        private Vector3 targetVelocity;

          // Accumulates all new velocities to apply when calculating velocities
        private Vector3 newVelocitySum;

        // References
        private CharacterController controller;
        private EntityDataSO entityData;

        // Properties
        public float MoveSpeed { get => currentMoveSpeed; }
        public float VerticalVelocity { get => currentVelocity.y; }
        public float Gravity { get => currentGravity; }
        public float facingRotation { get => core.parentTransform.eulerAngles.y; }

        // Methods
        protected override void Awake()
        {
            base.Awake();

            entityData = core.EntityData;
            controller = GetComponentInParent<CharacterController>();
        }

        public void SimpleMove(Vector2 moveDirection, float targetVelocity, bool relativeToCamera = true)
        {
            if (moveDirection == Vector2.zero) targetVelocity = 0f;

            SetTargetRotation(moveDirection, relativeToCamera);
            SetTargetMoveVelocity(moveDirection, targetVelocity, relativeToCamera);
        }
        public void SimpleJump(float height)
        {
            float timeToApex = entityData.JumpTime / 4.0f;
            float modifiedGravity = (height) / Mathf.Pow(timeToApex, 2);
            float jumpVelocity = (height) / timeToApex;

            SetGravity(modifiedGravity);
            SetVerticalVelocity(jumpVelocity);
        }

        public void SetTargetMoveVelocity(Vector2 targetDirection, float value, bool relativeToCamera)
        {
            targetDirection = targetDirection.normalized;
            float rotation = 0;

            if (targetDirection != Vector2.zero)
            {
                float cameraOffset = relativeToCamera ? Camera.main.transform.eulerAngles.y : 0;
                rotation = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg + cameraOffset;
            }

            Vector3 dir = Quaternion.Euler(0.0f, rotation, 0.0f) * Vector3.forward;
            targetDirection = new Vector2 (dir.x, dir.z);

            targetMoveVelocity = targetDirection * value;
        }
        public void SetTargetRotation(Vector2 targetDirection, bool relativeToCamera)
        {
            // Move Direction
            if (targetDirection != Vector2.zero)
            {
                float cameraOffset = relativeToCamera ? Camera.main.transform.eulerAngles.y : 0;
                targetRotation = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg + cameraOffset;
            }
        }

        public void AddVelocity(Vector3 direction, float value) => newVelocitySum += direction.normalized * value;
        public void AddHorizontalVelocity(Vector2 direction, float value) => AddVelocity(new Vector3(direction.x, 0f, direction.y), value);
        public void AddHorizontalVelocityByDistance(Vector2 direction, float distance)
        {
            float velocity = Mathf.Sqrt(distance * -entityData.Deacceleration * -2);
            AddHorizontalVelocity(direction, velocity);
        }

        public void SetGravity(float gravityValue) => currentGravity = gravityValue;
        public void SetVerticalVelocity(float value) => currentVelocity.y = value;

        public void HandleGravity()
        {
            float previousYVelocity = currentVelocity.y;
            float newVelocity = currentVelocity.y - (currentGravity * Time.deltaTime);
            currentVelocity.y = Mathf.Max((previousYVelocity + newVelocity) * 0.5f, -entityData.TerminalVelocity);
        }
        private void HandleVelocity()
        {
            Vector3 velocity = currentVelocity + newVelocitySum;

            if ((velocity - targetVelocity).magnitude > 0.05)
            {
                Vector2 vel = new Vector2(velocity.x, velocity.z);
                Vector2 targetVel = new Vector2(targetVelocity.x, targetVelocity.z);

                Vector2 newHorizontalVelocity = Vector2.MoveTowards(vel, targetVel, entityData.Deacceleration * Time.deltaTime);
                velocity = new Vector3(newHorizontalVelocity.x, velocity.y, newHorizontalVelocity.y);
            }
            else
            {
                velocity = new Vector3(targetVelocity.x, velocity.y, targetVelocity.z); ;
            }

            currentVelocity.x = velocity.x;
            currentVelocity.z = velocity.z;

            newVelocitySum = Vector3.zero;
        }
        private void HandleMove()
        {
            Vector2 velocity = currentMoveVelocity;

            if ((velocity - targetMoveVelocity).magnitude > 0.1)
            {
                velocity = Vector2.Lerp(velocity, targetMoveVelocity, Time.deltaTime * entityData.SpeedChangeRate);
            }
            else
            {
                velocity = targetMoveVelocity;
            }

            currentMoveVelocity = velocity;
            currentMoveSpeed = velocity.magnitude;

            targetMoveVelocity = Vector2.zero;
        }
        private void HandleRotation()
        {
            currentRotation = Mathf.SmoothDampAngle(core.parentTransform.eulerAngles.y, targetRotation, ref currentRotationVelocity, entityData.RotationSmoothTime);
            core.parentTransform.rotation = Quaternion.Euler(0.0f, currentRotation, 0.0f);
        }

        public void ApplyMovement()
        {
            HandleRotation();
            HandleMove();
            HandleVelocity();
            HandleGravity();

            controller.Move((currentVelocity + new Vector3(currentMoveVelocity.x, 0f, currentMoveVelocity.y)) * Time.deltaTime);
        }
    }
}
