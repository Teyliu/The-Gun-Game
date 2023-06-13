using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTutorial : MonoBehaviour
{
    public void Tutorial(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}