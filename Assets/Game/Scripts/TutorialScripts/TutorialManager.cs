using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialStepBase[] tutorialSteps;
    private int currentStepIndex = 0;
    private bool isTutorialComplete = false;

    private void Start()
    {
        ShowCurrentStep();
    }

    private void Update()
    {
        if (isTutorialComplete)
        {
            // Tutorial is already complete, no need to proceed
            return;
        }

        // Check if the current tutorial step is complete
        if (tutorialSteps[currentStepIndex].IsStepComplete())
        {
            // Move to the next step
            MoveToNextStep();
        }
    }

    private void MoveToNextStep()
    {
        // Hide the current step
        tutorialSteps[currentStepIndex].HideStep();

        // Increment the step index
        currentStepIndex++;

        if (currentStepIndex >= tutorialSteps.Length)
        {
            // Reached the end of the tutorial
            isTutorialComplete = true;
            return;
        }

        // Show the next step
        ShowCurrentStep();
    }

    private void ShowCurrentStep()
    {
        // Show the current step
        tutorialSteps[currentStepIndex].ShowStep();
    }
}