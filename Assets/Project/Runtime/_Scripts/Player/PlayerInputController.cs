using UnityEngine;
using UnityEngine.InputSystem;

namespace Project
{
    public class PlayerInputController : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public float zoom;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Look Settings")]
        public bool allowZoom;
        public bool allowRotate;
        public bool invertRotate;

        public void OnMove(InputValue value)
        {
            move = value.Get<Vector2>();
        }

        public void OnJump(InputValue value)
        {
            jump = value.isPressed;
        }

        public void OnSprint(InputValue value)
        {
            sprint = value.isPressed;
        }

        public void OnLook(InputValue value)
        {
            if (!allowRotate) return;

            if (invertRotate) look = -value.Get<Vector2>();
            else look = value.Get<Vector2>();
        }

        public void OnZoom(InputValue value)
        {
            if (!allowZoom) return;
            zoom = value.Get<float>();
        }
    }
}