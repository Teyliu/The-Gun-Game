using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    public GameObject trailPointPrefab;
    public int maxTrailPoints = 20;

    private List<Vector3> trailPoints;
    public int alliesCount = 0;
    private int nextAllyIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        trailPoints = new List<Vector3>();
    }

    private void Update()
    {
        GenerateTrailPoint();

        UpdateAlliesTrailFollowing();
    }

    private void GenerateTrailPoint()
    {
        if (trailPoints.Count >= maxTrailPoints)
        {
            trailPoints.RemoveAt(0);
        }

        trailPoints.Add(transform.position);
    }

    private void UpdateAlliesTrailFollowing()
    {
        foreach (var ally in FindObjectsOfType<AllyScript>())
        {
            if (ally.allyIndex < alliesCount)
            {
                ally.StartFollowingTrail(trailPoints);
                ally.SetOrderIndex(nextAllyIndex);
                ally.SetOrdered(true);
                nextAllyIndex++;
            }
            else
            {
                ally.SetOrdered(false);
            }
        }
    }

    public void AddAlly()
    {
        alliesCount++;
        nextAllyIndex = Mathf.Clamp(nextAllyIndex, 0, alliesCount);
    }

    public void RemoveAlly()
    {
        alliesCount--;
        nextAllyIndex = Mathf.Clamp(nextAllyIndex, 0, alliesCount);
    }
}