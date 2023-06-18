using UnityEngine;
using TMPro;

public class TutorialStepMovement : TutorialStepBase
{
    public string tutorialMessage;

    private TextMeshProUGUI tutorialText;
    private bool isActionComplete = false;

    private readonly KeyCode[] requiredKeys = {
        KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D,
        KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow
    };

    private void Start()
    {
        tutorialText = GetComponentInChildren<TextMeshProUGUI>();
        HideStep();
    }

    public override void ShowStep()
    {
        tutorialText.text = tutorialMessage;
        gameObject.SetActive(true);
        isActionComplete = false; 
    }

    public override void HideStep()
    {
        gameObject.SetActive(false);
    }

    public override bool IsStepComplete()
    {
        if (!isActionComplete)
        {
            foreach (KeyCode key in requiredKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    isActionComplete = true;
                    break;
                }
            }
        }

        return isActionComplete;
    }
}