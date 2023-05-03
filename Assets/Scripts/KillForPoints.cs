using UnityEngine;

public class KillForPoints : MonoBehaviour
{
    public int points;

    private PointSystem pointSystem;

    private void Start()
    {
        pointSystem = GameObject.FindObjectOfType<PointSystem>();
    }

    private void OnDestroy()
    {
        if (pointSystem != null)
        {
            pointSystem.AddPoints(points);
        }
    }
}