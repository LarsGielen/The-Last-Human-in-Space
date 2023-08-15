using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace Project
{
    public class PlayerCameraController : MonoBehaviour
    {
        [Header("Cinemachine settings")]
        [SerializeField]
        private CinemachineVirtualCamera cinemachineFollowCamera;

        [Space(10)]
        [SerializeField]
        [Range(-180f, 180f)]
        private float baseCameraYaw = 0.0f;

        [SerializeField]
        [Range(-90f, 90f)]
        private float baseCameraPitch = 45.0f;

        [Space(10)]
        [SerializeField]
        private float cameraRotateSensitivity = 100.0f;

        [Space(10)]
        [SerializeField]
        private float cameraZoomSensitivity = 100.0f;

        [SerializeField]
        private float cameraZoomSmoothTime = 0.12f;

        [SerializeField]
        private float cameraMinZoom = -10.0f;

        [SerializeField]
        private float cameraMaxZoom = 10.0f;

        private float cinemachineTargetYaw;
        private float cinemachineTargetPitch;
        private float cinemachineTargetZoom = 0.0f;
        private float cinemachineZoomVelocity;

        // References
        private PlayerInputController input;


        void Start ()
        {   
            input = GetComponent<PlayerInputController>();

            cinemachineTargetYaw = baseCameraYaw;
            cinemachineTargetPitch = baseCameraPitch;
        }

        private void LateUpdate()
        {
            CameraRotation();
            CameraZoom();
        }

        private void CameraRotation()
        {
            if (input.cameraRotation.sqrMagnitude > 0.01f)
            {
                cinemachineTargetYaw += input.cameraRotation.x * Time.deltaTime * cameraRotateSensitivity;
            }

            // Keep angle between -180 and 180 degrees
            cinemachineTargetYaw = cinemachineTargetYaw - Mathf.CeilToInt(cinemachineTargetYaw / 360f) * 360f;
            if (cinemachineTargetYaw < 0) cinemachineTargetYaw += 360f;

            cinemachineTargetPitch = cinemachineTargetPitch - Mathf.CeilToInt(cinemachineTargetPitch / 360f) * 360f;
            if (cinemachineTargetPitch < 0) cinemachineTargetPitch += 360f;

            cinemachineFollowCamera.transform.rotation = Quaternion.Euler(cinemachineTargetPitch, cinemachineTargetYaw, 0);
        }

        private void CameraZoom()
        {
            CinemachineCameraOffset cameraOffset;
            if (cinemachineFollowCamera.TryGetComponent(out cameraOffset) == false)
            {
                Debug.LogError("Follow camera needs a CinemachineCameraOffset component");
                return;
            }

            cinemachineTargetZoom += input.cameraZoom * Time.deltaTime * cameraZoomSensitivity;
            cinemachineTargetZoom = Mathf.Clamp(cinemachineTargetZoom, cameraMinZoom, cameraMaxZoom);

            float currentCameraZoom = cameraOffset.m_Offset.z;

            if (currentCameraZoom < cinemachineTargetZoom - 0.01f || currentCameraZoom > cinemachineTargetZoom + 0.01f)
            {
                float zoom = Mathf.SmoothDamp(cameraOffset.m_Offset.z, cinemachineTargetZoom, ref cinemachineZoomVelocity, cameraZoomSmoothTime);
                cameraOffset.m_Offset.z = zoom;
            }
            else
            {
                cameraOffset.m_Offset.z = cinemachineTargetZoom;
            }
        }

        private void OnValidate()
        {
            if (cinemachineFollowCamera != null) cinemachineFollowCamera.transform.rotation = Quaternion.Euler(baseCameraPitch, baseCameraYaw, 0);
        }
    }
}
