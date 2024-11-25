using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Controlador de personaje
    public CharacterController characterController;

    // Velocidad de movimiento del jugador
    public float speed = 10f;

    // Variables de gravedad
    private float gravity = -9.81f;
    Vector3 velocity;

    // Comprobación de si el jugador está en el suelo
    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;

    public bool isGrounded;

    // Variables de salto
    public float jumpHeight = 1.5f;

    void Start()
    {

    }

    void Update()
    {
        CheckGroundStatus();
        Move();
        // El salto se habilita solo en caso de definir si nuestro personaje va a saltar
        // Jump();
    }

    // Método que comprueba si el jugador está en el suelo.
    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
    // Método que maneja el movimiento del jugador basado en la entrada del teclado.
    private void Move()
    {
        // Obtener la entrada del teclado
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento
        Vector3 move = transform.right * x + transform.forward * z;

        // Mover el personaje
        characterController.Move(move * speed * Time.deltaTime);

        // Manejo de la gravedad
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}
