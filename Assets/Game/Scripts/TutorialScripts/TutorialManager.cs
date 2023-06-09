using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public TutorialStepBase[] tutorialSteps;
    private int currentStepIndex = -1; // Start with -1 to indicate the tutorial hasn't started yet
    private bool isTutorialComplete = false;
    private bool isWaitingForStepCompletion = false;

    public float stepPauseDuration = 1f; // Adjust the duration as desired

    private void Start()
    {
        MoveToNextStep();
    }

    private void Update()
    {
        if (isTutorialComplete)
        {
            return;
        }

        if (isWaitingForStepCompletion)
        {
            if (tutorialSteps[currentStepIndex].IsStepComplete())
            {
                isWaitingForStepCompletion = false;
                if (currentStepIndex < tutorialSteps.Length - 1)
                {
                    Invoke(nameof(MoveToNextStep), stepPauseDuration); // Pause for a duration before moving to the next step
                }
                else
                {
                    isTutorialComplete = true;
                    // Wait for a short delay before loading the scene
                    Invoke(nameof(LoadNextScene), 1f);
                }
                Time.timeScale = 1f; // Resume game time
            }
            else
            {
                return;
            }
        }
    }

    private void MoveToNextStep()
    {
        if (currentStepIndex >= 0)
        {
            tutorialSteps[currentStepIndex].HideStep();
        }

        currentStepIndex++;

        if (currentStepIndex >= tutorialSteps.Length)
        {
            return;
        }

        ShowCurrentStep();
        Time.timeScale = 0f; // Pause game time
    }

    private void ShowCurrentStep()
    {
        tutorialSteps[currentStepIndex].ShowStep();
        isWaitingForStepCompletion = true;
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("Level1");
    }
}

