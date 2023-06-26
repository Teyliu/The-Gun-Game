using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public Button soundButton;

    private bool isSoundEnabled = true;

    private void Start()
    {
        soundButton.onClick.AddListener(ToggleSound);
    }

    private void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;
        UpdateSoundButton();

        audioSource.enabled = isSoundEnabled;
    }

    private void UpdateSoundButton()
    {
        if (isSoundEnabled)
        {
            soundButton.GetComponentInChildren<Text>().text = "Sonido Desactivado";
        }
        else
        {
            soundButton.GetComponentInChildren<Text>().text = "Sonido Activado";
        }
    }
}
