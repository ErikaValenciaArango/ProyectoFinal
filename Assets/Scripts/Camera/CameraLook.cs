using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    // Sensibilidad del ratón
    public float mouseSensitivity = 80f;
    // Referencia al cuerpo del jugador
    public Transform playerBody;
    // Rotación en el eje X
    float xRotation = 0;
    // Rotación en el eje Y
    float yRotation = 0;

    // Límites de rotación en el eje X
    private const float MIN_X_ROTATION = -90f;
    private const float MAX_X_ROTATION = 90f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    public void CameraRotation()
    {
        // Obtener la entrada del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Actualizar la rotación en el eje X y limitarla
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, MIN_X_ROTATION, MAX_X_ROTATION);

        // Aplicar la rotación en el eje X a la cámara
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // Rotar el cuerpo del jugador en el eje Y
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Métodos públicos para obtener las rotaciones
    public float GetXRotation()
    {
        return xRotation;
    }

    public float GetYRotation()
    {
        return playerBody.eulerAngles.y;
    }
}
