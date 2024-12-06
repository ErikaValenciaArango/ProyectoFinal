using UnityEngine;
using TMPro;

public class NewsPaper : MonoBehaviour
{
    public GameObject panel; // El panel que se abrirá
    public float distanciaDeInteraccion = 5f; // Distancia para la interacción

    private Transform jugador;
    private bool enRangoDeInteraccion = false;
    public TextMeshProUGUI textoInteraccion;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        if (textoInteraccion != null)
        {
            textoInteraccion.gameObject.SetActive(false); 
        }

    }

    void Update()
    {
        // Calcula la distancia entre el jugador y el sprite
        float distancia = Vector3.Distance(jugador.position, transform.position);

        // Si el jugador está cerca del sprite
        if (distancia <= distanciaDeInteraccion)
        {
            enRangoDeInteraccion = true;
            if (Input.GetKeyDown(KeyCode.F)) 
            {
                AbrirPanel(); 
            }
        }
        else
        {
            enRangoDeInteraccion = false;
        }

        if (panel.activeSelf && Input.GetKeyDown(KeyCode.Q)) 
        {
            CerrarPanel(); 
        }
    }

    void OnGUI()
    {
        if (enRangoDeInteraccion && !panel.activeSelf)
        {
            textoInteraccion.text = "Press F to interact"; // Establece el texto
            textoInteraccion.gameObject.SetActive(true); // Muestra el texto
        }
        else
        {
            textoInteraccion.gameObject.SetActive(false); // Oculta el texto cuando no está en rango o el panel está abierto
        }
    }
    void AbrirPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true); // Activa el panel
        }
    }

    void CerrarPanel()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Desactiva el panel
        }
    }
}


