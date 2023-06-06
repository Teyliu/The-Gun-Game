using UnityEngine;
public abstract class TutorialStepBase : MonoBehaviour
{
    public abstract bool IsStepComplete();

    public abstract void ShowStep();
    public abstract void HideStep();
}