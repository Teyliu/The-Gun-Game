using UnityEngine;
using TMPro;

public class TutorialStepChangeWeapon : TutorialStepBase
{
    public string tutorialMessage;

    private TextMeshProUGUI tutorialText;
    private bool isWeaponChangeComplete = false;

    private void Start()
    {
        tutorialText = GetComponentInChildren<TextMeshProUGUI>();
        HideStep();
    }

    public override void ShowStep()
    {
        tutorialText.text = tutorialMessage;
        gameObject.SetActive(true);
        isWeaponChangeComplete = false;
    }

    public override void HideStep()
    {
        gameObject.SetActive(false);
    }

    public override bool IsStepComplete()
    {
        if (!isWeaponChangeComplete)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isWeaponChangeComplete = true;
            }
        }

        return isWeaponChangeComplete;
    }
}
