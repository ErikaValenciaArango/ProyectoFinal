using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
   

    public Transform objective; // El objetivo al que la cámara debe seguir
    private CameraLook cameraMovement; // Referencia al script CameraLook para obtener las rotaciones
    public Vector3 offset;
    // Variables para almacenar los valores de rotación de la cámara
    private float xCamRotation, yCamRotation;
    void Start()
    { 
    // Encuentra el objetivo en la escena (el jugador) si no se ha asignado manualmente
    if (objective == null) { objective = GameObject.Find("Player").transform; } 
    // Obtiene el script CameraLook del objeto principal de la cámara
    cameraMovement = GetComponent<CameraLook>(); 
    
    } 
    void Update() { 
        // Obtiene las rotaciones de la cámara desde CameraLook
        xCamRotation = cameraMovement.GetXRotation(); yCamRotation = cameraMovement.GetYRotation(); } 
    void LateUpdate() { 
        // La cámara sigue al jugador y rota junto con él. 
        // También se aplica la rotación de la cámara para mirar hacia arriba y hacia abajo.
        transform.position = objective.position+offset; 
        transform.rotation = Quaternion.Euler(xCamRotation, yCamRotation, 0); }
}
