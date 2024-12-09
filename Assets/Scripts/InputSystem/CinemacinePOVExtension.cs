using UnityEngine;
using Cinemachine;

public class CineMachinePOVExtension : CinemachineExtension
{
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    [SerializeField] private float clampAngle = 80f;

    [SerializeField] private Transform upperBody;

    private InputManager inputManager;
    private Vector3 startingRotation;
    private bool isStartingRotationInitialized = false;

    protected override void Awake()
    {
        inputManager = InputManager.Instance;
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

                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                float ClampRootX = Mathf.Clamp(startingRotation.y, -28, 6);

                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);


                // Rotar el torso hacia la misma dirección
                if (upperBody != null)
                {
                    float ClampRootBodyY = Mathf.Clamp(upperBody.localRotation.eulerAngles.y, -6, 6);


                    Quaternion torsoRotation = Quaternion.Euler(-ClampRootX, ClampRootBodyY, 0f);
                    upperBody.localRotation = torsoRotation;
                }
            }
        }
    }
}
