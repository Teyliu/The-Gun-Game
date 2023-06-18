using UnityEngine;
using TMPro;

public class TutorialStepArrive : TutorialStepBase
{
    public string tutorialMessage;
    public Transform arrivalArea;
    public float arrivalRadius = 2f;
    public LayerMask playerLayerMask;

    private TextMeshProUGUI tutorialText;
    private bool hasArrived = false;

    private void Start()
    {
        tutorialText = GetComponentInChildren<TextMeshProUGUI>();
        HideStep();
    }

    public override void ShowStep()
    {
        tutorialText.text = tutorialMessage;
        gameObject.SetActive(true);
        hasArrived = false;
    }

    public override void HideStep()
    {
        gameObject.SetActive(false);
    }

    public override bool IsStepComplete()
    {
        if (!hasArrived)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(arrivalArea.position, arrivalRadius, playerLayerMask);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    hasArrived = true;
                    break;
                }
            }
        }

        return hasArrived;
    }

    private void OnDrawGizmosSelected()
    {
        if (arrivalArea != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(arrivalArea.position, arrivalRadius);
        }
    }
}
