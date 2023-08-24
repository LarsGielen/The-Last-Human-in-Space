using UnityEngine;
using UnityEngine.InputSystem;

namespace Project
{
    public class PlayerInputController : MonoBehaviour
    {
        [Header("Character Input Values")]
        [SerializeField] private Vector2 move;
        [SerializeField] private bool jump;
        [SerializeField] private bool run;
        [SerializeField] private bool attack;

        [Space(10)]

        [SerializeField] private Vector2 cameraRotation;
        [SerializeField] private float cameraZoom;

        [Header("Movement Settings")]
        [SerializeField] private bool analogMovement;
        [SerializeField] private bool moveWithMouse;
        [SerializeField] private LayerMask rayCastLayerMask;

        [Header("Camera Settings")]
        [SerializeField] private bool allowZoom;
        [SerializeField] private bool allowRotate;
        [SerializeField] private bool invertRotate;

        private bool updateMousePos = false;

        // properties
        public Vector2 MoveInput => move;
        public bool Jump { get => jump; set => jump = value; }
        public bool Run => run;
        public bool Attack => attack;
        public Vector2 CameraRotation => cameraRotation;
        public float CameraZoom => cameraZoom;
        public bool AnalogMovement => analogMovement;
        public bool MoveRelativeToCamera { get; private set; }

        // Methods
        private void Update()
        {
            if (!moveWithMouse) return;

            if (updateMousePos) UpdateMousePos();
            else move = Vector2.zero;
        }

        public void OnMove(InputValue value)
        {
            if (moveWithMouse)
            {
                updateMousePos = value.isPressed;
                
            }
            else move = value.Get<Vector2>();
        }

        public void OnJump(InputValue value)
        {
            jump = value.isPressed;
        }

        public void OnSprint(InputValue value)
        {
            run = value.isPressed;
        }

        public void OnAttack(InputValue value)
        {
            attack = value.isPressed;
        }

        public void OnCameraRotate(InputValue value)
        {
            if (!allowRotate) return;

            if (invertRotate) cameraRotation = value.Get<Vector2>();
            else cameraRotation = -value.Get<Vector2>();
        }

        public void OnCameraZoom(InputValue value)
        {
            if (!allowZoom) return;
            cameraZoom = value.Get<float>();
        }

        private void UpdateMousePos()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, 50, rayCastLayerMask))
            {
                Vector3 direction = hitData.point - gameObject.transform.position;
                move = new Vector2(direction.x, direction.z).normalized;
            }
        }

        private void switchMovementWithMouse(bool status)
        {
            PlayerInput input;
            if (TryGetComponent(out input))
            {
                moveWithMouse = status;
                MoveRelativeToCamera = !status;

                InputActionMap PlayerMap = input.actions.FindActionMap("Player");
                InputActionMap PlayerMouseMap = input.actions.FindActionMap("PlayerMouse");

                if (moveWithMouse)
                {
                    PlayerMap.Disable();
                    PlayerMouseMap.Enable();

                    input.defaultActionMap = "PlayerMouse";
                }
                else
                {
                    PlayerMap.Enable();
                    PlayerMouseMap.Disable();

                    input.defaultActionMap = "Player";
                }
            }
        }

        // For testing only!! -- Remove before Merge
        private void OnValidate()
        {
            switchMovementWithMouse(moveWithMouse);
        }
    }
}