using UnityEngine;
using static Enemy;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private InputManager inputManager;
    private Transform cameraTransform;

    [SerializeField] private AudioClip stepsClip;
    [SerializeField] private float footstepInterval = 0.4f;
    private float footstepTimer = 0f;

    // Animator de el player
    private Animator animPlayer;
    private WeaponSwitching activeArm;

    private void Start()
    {
        animPlayer = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        activeArm = GetComponent<WeaponSwitching>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        //Aca verifico si hay armas en el holder
        if (activeArm.ActiveWeapon() != null)
        {
            ChangeState(activeArm.ActiveWeapon());
        }



        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        SetAnimation(movement);
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move.magnitude > 0f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                AudioManager.Instance.PlaySFX(stepsClip, 1f);
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }

        // Hace que el jugador salte
        /*
        if (inputManager.PlayerJumped() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }*/

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Actualiza la rotación del jugador para que siga la cámara
        RotatePlayerWithCamera();
    }

    private void RotatePlayerWithCamera()
    {
        Vector3 cameraRotation = cameraTransform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
    }


    /// <summary>
    /// Section of animator
    /// </summary>

    public void SetAnimation(Vector2 e)
    {

        // Solo actualizamos los valores del Animator si hay un cambio significativo
        animPlayer.SetFloat("moveXFloat", e.x, 0.1f, Time.deltaTime);
        animPlayer.SetFloat("moveYFloat", e.y, 0.1f, Time.deltaTime);
    }

    public void ChangeState(GameObject weapon)
    {
        string weaponName = weapon.name; // Suponiendo que los nombres de las armas están configurados
        switch (weaponName)
        {
            case "Knife":
                animPlayer.SetBool("KnifeBool",true);
                animPlayer.SetBool("PistolBool", false);
                animPlayer.SetBool("HandsBool", false);
                break;
            case "Pistol":
                animPlayer.SetBool("KnifeBool", false);
                animPlayer.SetBool("PistolBool", true);
                animPlayer.SetBool("HandsBool", false);
                break;

            default:
                Debug.Log("Arma no encontrada");
                break;
        }
    }
}

