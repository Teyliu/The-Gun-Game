using UnityEngine;
using TMPro;

public class TutorialStepKill : TutorialStepBase
{
    public string tutorialMessage;
    public GameObject objective;

    private TextMeshProUGUI tutorialText;
    private bool isObjectiveDestroyed = false;

    private void Start()
    {
        tutorialText = GetComponentInChildren<TextMeshProUGUI>();
        HideStep();
    }

    public override void ShowStep()
    {
        tutorialText.text = tutorialMessage;
        gameObject.SetActive(true);
        isObjectiveDestroyed = false;
    }

    public override void HideStep()
    {
        gameObject.SetActive(false);
    }

    public override bool IsStepComplete()
    {
        if (!isObjectiveDestroyed && objective != null && objective.activeSelf == false)
        {
            isObjectiveDestroyed = true;
        }

        return isObjectiveDestroyed;
    }
}
