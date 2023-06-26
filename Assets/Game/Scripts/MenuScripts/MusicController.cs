using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public Button musicButton;

    private bool isMusicEnabled = true;

    private void Start()
    {
        musicButton.onClick.AddListener(ToggleMusic);
    }

    private void ToggleMusic()
    {
        isMusicEnabled = !isMusicEnabled;
        UpdateMusicButton();

        if (isMusicEnabled)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }
    }

    private void UpdateMusicButton()
    {
        if (isMusicEnabled)
        {
            musicButton.GetComponentInChildren<Text>().text = "Música Desactivada";
        }
        else
        {
            musicButton.GetComponentInChildren<Text>().text = "Música Activada";
        }
    }
}
