using UnityEngine;
using TMPro;

public class TutorialStepMovement : TutorialStepBase
{
    public string tutorialMessage;
    public KeyCode[] requiredKeys;

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
        foreach (KeyCode key in requiredKeys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }
}
