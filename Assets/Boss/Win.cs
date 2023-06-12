using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Win : MonoBehaviour
{
    public TextMeshProUGUI WinnerText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Win")
        {
            WinnerText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
