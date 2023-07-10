using UnityEngine;
using TMPro;

public class TutorialStepInteract : TutorialStepBase
{
    public string tutorialMessage;
    public OpenDesk openDeskScript;

    private TextMeshProUGUI tutorialText;
    private bool isInteractionComplete = false;

    private void Start()
    {
        tutorialText = GetComponentInChildren<TextMeshProUGUI>();
        HideStep();
    }

    public override void ShowStep()
    {
        tutorialText.text = tutorialMessage;
        gameObject.SetActive(true);
        isInteractionComplete = false;
    }

    public override void HideStep()
    {
        gameObject.SetActive(false);
    }

    public override bool IsStepComplete()
    {
        if (Input.GetKeyDown(KeyCode.E) && openDeskScript != null && openDeskScript.isOpened)
        {
            isInteractionComplete = true;
        }

        return isInteractionComplete;
    }
}
