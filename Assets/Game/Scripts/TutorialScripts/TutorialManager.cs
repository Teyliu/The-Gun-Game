using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

[System.Serializable]
public class TutorialStepData
{
    public MonoBehaviour[] scriptsToDisable;
    public MonoBehaviour[] scriptsToEnable;
}

public class TutorialManager : MonoBehaviour
{
    public TutorialStepBase[] tutorialSteps;
    public TutorialStepData[] tutorialStepData;
    public GameObject playerPrefab; // Reference to the player prefab
    private GameObject playerObject; // Reference to the instantiated player object
    private int currentStepIndex = -1; // Start with -1 to indicate the tutorial hasn't started yet
    private bool isTutorialComplete = false;
    private bool isWaitingForStepCompletion = false;

    public float stepPauseDuration = 1f; // Adjust the duration as desired

    private void Start()
    {
        InstantiatePlayer();
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
            ApplyScriptChanges(tutorialStepData[currentStepIndex].scriptsToDisable, tutorialStepData[currentStepIndex].scriptsToEnable);
            ResetPlayer();
        }

        currentStepIndex++;

        if (currentStepIndex >= tutorialSteps.Length)
        {
            return;
        }

        ShowCurrentStep();
        ApplyScriptChanges(tutorialStepData[currentStepIndex].scriptsToDisable, tutorialStepData[currentStepIndex].scriptsToEnable);
        Time.timeScale = 0.5f; // Pause game time
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

    private void InstantiatePlayer()
    {
        playerObject = GameObject.FindObjectOfType<PlayerScript>().gameObject;

        if (playerObject == null)
        {
            Debug.LogWarning("Player object not found in the scene.");
        }
    }


    private void ApplyScriptChanges(MonoBehaviour[] scriptsToDisable, MonoBehaviour[] scriptsToEnable)
    {
        if (playerObject != null)
        {
            // Disable scripts to be disabled
            if (scriptsToDisable != null)
            {
                foreach (MonoBehaviour script in scriptsToDisable)
                {
                    Behaviour behaviour = script as Behaviour;
                    if (behaviour != null)
                    {
                        behaviour.enabled = false;
                    }
                }
            }

            // Enable scripts to be enabled
            if (scriptsToEnable != null)
            {
                foreach (MonoBehaviour script in scriptsToEnable)
                {
                    Behaviour behaviour = script as Behaviour;
                    if (behaviour != null)
                    {
                        behaviour.enabled = true;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Player object is null. Make sure to instantiate the player object before applying script changes.");
        }
    }

    private void ResetPlayer()
    {
        if (playerObject == null)
        {
            // Player object is null or destroyed, nothing to reset
            return;
        }

        // Disable all the scripts on the player object
        MonoBehaviour[] allScripts = playerObject.GetComponentsInChildren<MonoBehaviour>(includeInactive: true);
        ApplyScriptChanges(allScripts, null);
        // Enable the player script itself
        PlayerScript playerScript = playerObject.GetComponent<PlayerScript>();
        if (playerScript != null)
        {
            playerScript.enabled = true;
        }
    }
}
