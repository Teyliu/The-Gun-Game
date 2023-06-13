using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMainMenu : MonoBehaviour
{
    public void Menu(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
