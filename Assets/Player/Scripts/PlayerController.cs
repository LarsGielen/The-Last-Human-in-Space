using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerController : MonoBehaviour
{
    [Header("Player movement settings")]
    [SerializeField] 
    private float moveSpeed = 2.0f;

    [SerializeField] 
    private float SprintSpeed = 6.0f;

    [SerializeField] [Range(0.0f, 0.3f)]
    private float RotationSmoothTime = 0.12f;

    [SerializeField] 
    private float SpeedChangeRate = 10.0f;

    [Header("Player jump and gravity settings")]
    [SerializeField]
    private float jumpHeight = 1.2f;

    [SerializeField]
    private float gravity = -15.0f;

    [Space(10)]
    [SerializeField]
    private float jumpTimeOut = 0.50f;

    [SerializeField]
    private float fallTimeOut = 0.50f;

    [Header("Grounded settings")]
    [SerializeField]
    private float groundedOffset = -0.15f;

    [SerializeField]
    private float groundedRadius = 0.28f;

    [SerializeField]
    private LayerMask groundLayers;

    [Header("Cinemachine settings")]
    [SerializeField]
    private CinemachineVirtualCamera cinemachineFollowCamera;

    [SerializeField]
    private float cameraSpeed = 100.0f;

    // Player
    private float speed;
    private float animationBlend;
    private float targetRotation = 0.0f;
    private float rotationVelocity;
    private float verticalVelocity;
    private float terminalVelocity = 53.0f;
    private bool  grounded;

    // cinemachine
    private float cinemachineTargetYaw = 0.0f;
    private float cinemachineTargetPitch = 45.0f;

    // Timeouts
    private float jumpTimeoutDelta;
    private float fallTimeoutDelta;

    // Animation IDs
    private int animIDSpeed;
    private int animIDGrounded;
    private int animIDJump;
    private int animIDFreeFall;
    private int animIDMotionSpeed;

    // References
    private Animator animator;
    private CharacterController controller;
    private PlayerInputController input;
    private GameObject mainCamera;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInputController>();

        animIDSpeed = Animator.StringToHash("Speed");
        animIDGrounded = Animator.StringToHash("Grounded");
        animIDJump = Animator.StringToHash("Jump");
        animIDFreeFall = Animator.StringToHash("FreeFall");
        animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    private void Update()
    {
        JumpAndGravity();
        CheckGrounded();
        MovePlayer();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CheckGrounded()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

        // Update animation
        animator.SetBool(animIDGrounded, grounded);
    }

    private void JumpAndGravity()
    {
        if (grounded)
        {
            fallTimeoutDelta = fallTimeOut;

            if (verticalVelocity < 0.0f) verticalVelocity = -2f;

            animator.SetBool(animIDJump, false);
            animator.SetBool(animIDFreeFall, false);

            // Jump
            if (input.jump && jumpTimeoutDelta <=  0.0f)
            {
                // Goed bezig Newton
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

                animator.SetBool(animIDJump, true);
            }

            if (jumpTimeoutDelta >= 0.0f) jumpTimeoutDelta -= Time.deltaTime;
        }
        else
        {
            jumpTimeoutDelta = jumpTimeOut;

            if (fallTimeoutDelta  >= 0.0f) fallTimeoutDelta -= Time.deltaTime;
            else animator.SetBool(animIDFreeFall, true);

            input.jump = false;
        }

        // Apply gravity
        if (verticalVelocity < terminalVelocity) verticalVelocity += gravity * Time.deltaTime;
    }

    public void MovePlayer()
    {
        float targetSpeed = input.sprint ? SprintSpeed : moveSpeed;

        if (input.move == Vector2.zero) targetSpeed = 0f;

        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude;
        float inputMagnitude = input.analogMovement ? input.move.magnitude : 1f;

        if (currentHorizontalSpeed < targetSpeed - 0.1f || currentHorizontalSpeed > targetSpeed + 0.1f)
        {
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
            speed = Mathf.Round(speed * 1000) / 1000;
        }
        else
        {
            speed = targetSpeed;
        }

        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (animationBlend < 0.01f) animationBlend = 0f;

        Vector3 inputDirection = new Vector3(input.move.x, 0f, input.move.y).normalized;

        if (input.move != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, RotationSmoothTime);

            // Set player rotation
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        // Set player movement
        controller.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);

        // Update Animation
        animator.SetFloat(animIDSpeed, animationBlend);
        animator.SetFloat(animIDMotionSpeed, inputMagnitude);
    }

    private void CameraRotation()
    {
        if (input.look.sqrMagnitude > 0.01f)
        {
            cinemachineTargetYaw += input.look.x * Time.deltaTime * cameraSpeed;
        }
        
        // Keep angle between -180 and 180 degrees
        cinemachineTargetYaw = cinemachineTargetYaw - Mathf.CeilToInt(cinemachineTargetYaw / 360f) * 360f;
        if (cinemachineTargetYaw < 0) cinemachineTargetYaw += 360f;

        cinemachineTargetPitch = cinemachineTargetPitch - Mathf.CeilToInt(cinemachineTargetPitch / 360f) * 360f;
        if (cinemachineTargetPitch < 0) cinemachineTargetPitch += 360f;
        
        cinemachineFollowCamera.transform.rotation = Quaternion.Euler(cinemachineTargetPitch, cinemachineTargetYaw, 0);
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        /*
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (footstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, footstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(footstepAudioClips[index], transform.TransformPoint(controller.center), footstepAudioVolume);
            }
        }
        */
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        /*
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(landingAudioClip, transform.TransformPoint(controller.center), footstepAudioVolume);
        }
        */
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z),
            groundedRadius);
    }
}
