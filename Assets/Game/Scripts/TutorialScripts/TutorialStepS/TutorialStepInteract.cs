using UnityEngine;
using TMPro;

public class TutorialStepInteract : TutorialStepBase
{
    public string tutorialMessage;
    public Collider2D interactionCollider;

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
        if (!isInteractionComplete && Input.GetKeyDown(KeyCode.E) && interactionCollider != null && interactionCollider.bounds.Contains(transform.position))
        {
            PlayerScript playerScript = GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                isInteractionComplete = true;
            }
        }

        return isInteractionComplete;
    }
}
