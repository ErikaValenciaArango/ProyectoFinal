using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    private bool pauseGame = false;

    private ZombieSound[] zombieSounds; // Referencias a los sonidos de los zombies

    private void Start()
    {
        Time.timeScale = 1f;
        zombieSounds = FindObjectsOfType<ZombieSound>(); // Encuentra todos los scripts ZombieSound en la escena
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        pauseGame = true;
        Cursor.lockState = CursorLockMode.None;

        // Pausar todos los sonidos de los zombies
        foreach (var zombieSound in zombieSounds)
        {
            zombieSound.SetPaused(true);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        pauseGame = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Reanudar todos los sonidos de los zombies
        foreach (var zombieSound in zombieSounds)
        {
            zombieSound.SetPaused(false);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void goMainMenu()
    {
        Time.timeScale = 1; // Reinicia el tiempo si fue pausado
        SceneManager.LoadScene("MenuInicial");
        PlayerPrefs.DeleteAll();
    }
}

