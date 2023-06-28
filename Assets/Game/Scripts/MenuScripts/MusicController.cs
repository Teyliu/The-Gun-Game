using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public Button musicButton;

    public bool isMusicEnabled = true;

    public void Start()
    {
        musicButton.onClick.AddListener(ToggleMusic);
    }

    public void ToggleMusic()
    {
        isMusicEnabled = !isMusicEnabled;
        UpdateMusicButton();

        if (isMusicEnabled)
        {
            musicSource.Stop();
        }
        else
        {
            musicSource.Play();
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
