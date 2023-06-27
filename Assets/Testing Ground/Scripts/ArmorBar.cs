using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private Image foregroundImage;

    private void Start()
    {
        foregroundImage = GetComponent<Image>();
    }

    private void Update()
    {
        foregroundImage.fillAmount = (float)playerHealth.currentArmor / playerHealth.maxArmor;
    }
}