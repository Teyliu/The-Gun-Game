using UnityEngine;
using TMPro;

public class TutorialStepGetShot : TutorialStepBase
{
    public string tutorialMessage;
    private bool isPlayerShot = false;
    private PlayerHealth playerHealth;
    private TextMeshProUGUI tutorialText;

    private void Start()
    {
        tutorialText = GetComponentInChildren<TextMeshProUGUI>();
        HideStep();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public override void ShowStep()
    {
        tutorialText.text = tutorialMessage;
        Debug.Log(tutorialMessage);
        gameObject.SetActive(true);
        isPlayerShot = false;
    }

    public override void HideStep()
    {
        gameObject.SetActive(false);
    }

    public override bool IsStepComplete()
    {
        if (!isPlayerShot && playerHealth != null && playerHealth.currentHealth < playerHealth.maxHealth)
        {
            isPlayerShot = true;
        }

        return isPlayerShot;
    }
}
