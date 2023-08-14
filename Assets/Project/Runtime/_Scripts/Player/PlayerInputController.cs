using UnityEngine;
using UnityEngine.InputSystem;

namespace Project
{
    public class PlayerInputController : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public bool jump;
        public bool sprint;

        [Space(10)]

        public Vector2 cameraRotation;
        public float cameraZoom;

        [Header("Movement Settings")]
        public bool analogMovement;
        public bool moveWithMouse;
        public LayerMask rayCastLayerMask;

        [Header("Camera Settings")]
        public bool allowZoom;
        public bool allowRotate;
        public bool invertRotate;

        private bool updateMousePos = false;

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
            sprint = value.isPressed;
        }

        public void OnInteract(InputValue value)
        {
            Debug.Log("Pressed: " + value.isPressed);
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
                Debug.DrawRay(ray.origin, hitData.point, Color.green);
                Debug.DrawRay(hitData.point, Vector3.up, Color.red);

                Vector3 direction = hitData.point - gameObject.transform.position;
                move = new Vector2(direction.x, direction.z).normalized;

                Debug.DrawLine(hitData.point, Vector3.up);
            }
        }

        // For testing only!! -- Remove before Merge
        private void OnValidate()
        {
            PlayerInput input;
            if (TryGetComponent(out input))
            {
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
    }
}