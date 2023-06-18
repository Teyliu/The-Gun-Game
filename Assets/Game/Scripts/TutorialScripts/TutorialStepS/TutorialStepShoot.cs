using UnityEngine;
using TMPro;

public class TutorialStepShoot : TutorialStepBase
{
    public string tutorialMessage;
    public KeyCode shootKey;

    public TextMeshProUGUI tutorialText;

    private void Start()
    {
        tutorialText = GetComponentInChildren<TextMeshProUGUI>();
        HideStep();
    }

    public override void ShowStep()
    {
        tutorialText.text = tutorialMessage;
        gameObject.SetActive(true);
    }

    public override void HideStep()
    {
        gameObject.SetActive(false);
    }

    public override bool IsStepComplete()
    {
        if (Input.GetKeyDown(shootKey))
        {
            return true;
        }
        return false;
    }
}