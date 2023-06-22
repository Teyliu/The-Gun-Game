using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject PGameOver;
    private PlayerHealth PlayerHealth;

    private void Start()
    {
        PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        PlayerHealth.Died += AddMenu;
    }
    private void AddMenu(object sender, EventArgs e)
    {
        PGameOver.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Salir()
    {
         Application.Quit();
    }
}
