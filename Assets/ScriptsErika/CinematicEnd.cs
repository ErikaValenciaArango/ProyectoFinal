using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CinematicEnd : MonoBehaviour
{
    public PlayableDirector director;

    void Start()
    {
        // Obtiene el componente PlayableDirector
        director = GetComponent<PlayableDirector>();

        // A�ade un listener para cuando termina el Timeline
        director.stopped += OnCinematicEnd;
    }

    void OnCinematicEnd(PlayableDirector pd)
    {
        // Cambia a la escena del men� principal cuando termine la cinem�tica
        if (pd == director)
        {
            SceneManager.LoadScene("MenuInicial");
        }
    }

    void OnDestroy()
    {
        // Elimina el listener al destruir el objeto para evitar errores
        if (director != null)
        {
            director.stopped -= OnCinematicEnd;
        }
    }

    public void Skip()
    {

        if (director != null)
        {
            director.Stop();
        }


        SceneManager.LoadScene("MainMenu");
    }
}


