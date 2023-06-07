using UnityEngine;
using UnityEngine.SceneManagement;

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
            return;
        }

        if (tutorialSteps[currentStepIndex].IsStepComplete())
        {
            MoveToNextStep();
        }
    }

    private void MoveToNextStep()
    {
        tutorialSteps[currentStepIndex].HideStep();

        currentStepIndex++;

        if (currentStepIndex >= tutorialSteps.Length)
        {
            isTutorialComplete = true;

            SceneManager.LoadScene("Level1");
            return;
        }

        ShowCurrentStep();
    }

    private void ShowCurrentStep()
    {
        tutorialSteps[currentStepIndex].ShowStep();
    }
}