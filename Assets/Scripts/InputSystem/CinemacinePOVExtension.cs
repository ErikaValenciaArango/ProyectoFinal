using UnityEngine;
using Cinemachine;

public class CineMachinePOVExtension : CinemachineExtension
{
    // Sensibilidad 
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    // Angulo de rotacion
    [SerializeField] private float clampAngle = 80f;

    private InputManager inputManager;
    private Vector3 startingRotation;
    private bool isStartingRotationInitialized = false;

    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        if (inputManager == null)
        {
            Debug.LogError("InputManager.Instance es null en CineMachinePOVExtension.Awake");
        }
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (!isStartingRotationInitialized)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                    isStartingRotationInitialized = true;
                }

                if (inputManager != null)
                {
                    Vector2 deltaInput = inputManager.GetMouseDelta();
                    startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                }
                else
                {
                    Debug.LogWarning("inputManager es null en CineMachinePOVExtension.PostPipelineStageCallback");
                }
            }
        }
    }
}
