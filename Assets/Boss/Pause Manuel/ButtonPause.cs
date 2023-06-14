using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPause : MonoBehaviour
{
    public Button pauseButton;
    public GameObject pauseMenu;

    private bool isPaused = false;
    private bool isGameStarted = false;

    private void Start()
    {
        pauseButton.onClick.AddListener(TogglePause);
        pauseMenu.SetActive(false); // Desactiva la interfaz de pausa al inicio
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameStarted)
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pausa el tiempo de la escena
            pauseMenu.SetActive(true); // Activa el menú de pausa
        }
        else
        {
            Time.timeScale = 1f; // Restaura el tiempo de la escena
            pauseMenu.SetActive(false); // Desactiva el menú de pausa
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
    }
}
