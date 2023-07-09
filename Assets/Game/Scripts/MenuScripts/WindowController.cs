using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowController : MonoBehaviour
{
    public GameObject window;
    public Button open;
    public Button close;
    public Button quit;


    private void Start()
    {
        open.onClick.AddListener(OpenWindow);
        close.onClick.AddListener(CloseWindow);
        quit.onClick.AddListener(QuitGame);
        window.SetActive(false);
    }


    public void OpenWindow()
    {
        window.SetActive(true);
    }

    public void CloseWindow()
    {
        window.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}