using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public Button musicButton;

    private bool isMusicMuted = false;

    private void Start()
    {
        musicButton.onClick.AddListener(ToggleMusic);
    }

    public void ToggleMusic()
    {
        isMusicMuted = !isMusicMuted;

        if (isMusicMuted)
        {
            musicSource.mute = true;
        }
        else
        {
            musicSource.mute = false;
        }
    }
}
