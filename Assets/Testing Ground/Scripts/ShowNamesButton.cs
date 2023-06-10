using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowNamesButton : MonoBehaviour
{
    public Button showNamesButton;
    public GameObject namesGroup;

    private bool namesVisible;

    private string[] memberNames = {
        "1. Adrian Pacheco",
        "2. Alexander Inostroza",
        "3. Jorge Lezama",
        "4. Manuel Salas",
        "5. Piero Alessandro",
        "6. Marcelo Salerno"
    };

    private void Start()
    {
        showNamesButton.onClick.AddListener(ToggleNames);
        HideNames();
    }

    private void ToggleNames()
    {
        namesVisible = !namesVisible;

        if (namesVisible)
        {
            ShowNames();
        }
        else
        {
            HideNames();
        }
    }

    private void ShowNames()
    {
        namesGroup.SetActive(true);

        TextMeshProUGUI textMeshPro = namesGroup.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = string.Join("\n", memberNames);
    }

    private void HideNames()
    {
        namesGroup.SetActive(false);
    }
}
