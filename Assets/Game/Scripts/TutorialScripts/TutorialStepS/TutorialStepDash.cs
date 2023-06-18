using UnityEngine;
using TMPro;

public class TutorialStepDash : TutorialStepBase
{
    public string tutorialMessage;

    private TextMeshProUGUI tutorialText;
    private bool isDashComplete = false;

    private void Start()
    {
        tutorialText = GetComponentInChildren<TextMeshProUGUI>();
        HideStep();
    }

    public override void ShowStep()
    {
        tutorialText.text = tutorialMessage;
        gameObject.SetActive(true);
        isDashComplete = false;
    }

    public override void HideStep()
    {
        gameObject.SetActive(false);
    }

    public override bool IsStepComplete()
    {
        if (!isDashComplete)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDashComplete = true;
            }
        }

        return isDashComplete;
    }
}
