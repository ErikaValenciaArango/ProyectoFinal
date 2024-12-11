using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    [SerializeField] private AudioClip zombieSound;  // El AudioClip con el sonido del zombie
    [SerializeField] private Transform player;       // Referencia al jugador
    [SerializeField] private float maxDistance = 10f; // Distancia máxima para que el sonido sea escuchado
    [SerializeField] private float minDistance = 0.5f;  // Distancia mínima para sonido a volumen máximo

    private AudioSource audioSource; // El AudioSource que va a reproducir el sonido
    private bool isPaused = false; // Controla si el juego está pausado
    private bool isDead = false; // Controla si el zombie está muerto

    private void Start()
    {
        // Obtener o añadir un AudioSource al zombie
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = zombieSound;  // Asignar el sonido del zombie al AudioSource
        audioSource.loop = true; // Hacer que el sonido se repita mientras esté cerca del jugador
    }

    private void Update()
    {
        if (isPaused || isDead)
        {
            // Detener el audio si el juego está pausado o el zombie está muerto
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            return; // Sal del Update
        }

        // Calcula la distancia entre el zombie y el jugador
        float distance = Vector3.Distance(transform.position, player.position);

        // Controlar el volumen en función de la distancia
        if (distance <= maxDistance)
        {
            // El volumen se ajusta según la distancia
            float volume = Mathf.Clamp01(1 - (distance - minDistance) / (maxDistance - minDistance));
            volume *= 0.05f; // Ajusta el volumen general
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

    public void OnEnemyDeath()
    {
        // Marcar al zombie como muerto y detener el sonido
        isDead = true;
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}





