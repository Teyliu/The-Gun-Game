using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Button returnButton;

    private void Start()
    {
        returnButton.onClick.AddListener(ReturnToGame);
    }

    private void ReturnToGame()
    {
        Time.timeScale = 1f; // Restaura el tiempo de la escena
        gameObject.SetActive(false); // Desactiva el menú de pausa
    }
}
