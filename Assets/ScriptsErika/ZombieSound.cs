using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    [SerializeField] private AudioClip zombieSound;  // El AudioClip con el sonido del zombie
    [SerializeField] private Transform player;       // Referencia al jugador (por ejemplo, la cámara o el jugador mismo)
    [SerializeField] private float maxDistance = 10f; // Distancia máxima para que el sonido sea escuchado
    [SerializeField] private float minDistance = 0.5f;  // Distancia mínima para sonido a volumen máximo

    private AudioSource audioSource; // El AudioSource que va a reproducir el sonido
    private bool isPaused = false; // Controla si el juego está pausado

    private void Start()
    {
        // Obtener o añadir un AudioSource al zombie
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Añadir un nuevo AudioSource si no tiene uno
        }

        audioSource.clip = zombieSound;  // Asignar el sonido del zombie al AudioSource
        audioSource.loop = true; // Hacer que el sonido se repita mientras esté cerca del jugador
    }

    private void Update()
    {
        if (isPaused)
        {
            // Detén el audio si el juego está pausado
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
            return; // Sal del Update si está pausado
        }

        // Calcula la distancia entre el zombie y el jugador
        float distance = Vector3.Distance(transform.position, player.position);

        // Controlar el volumen en función de la distancia
        if (distance <= maxDistance)
        {
            // El volumen se ajusta según la distancia
            float volume = Mathf.Clamp01(1 - (distance - minDistance) / (maxDistance - minDistance));

            // Reducir el volumen general
            volume *= 0.05f; // Ajusta el volumen a la mitad
            audioSource.volume = volume;

            // Reproducir el sonido si no se está reproduciendo
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Detener el sonido si el jugador está fuera del rango
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    public void SetPaused(bool paused)
    {
        isPaused = paused;
        if (paused && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else if (!paused && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }
}




