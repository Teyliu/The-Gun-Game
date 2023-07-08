using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding;

public class EnemyMovementScript : MonoBehaviour
{
    [Header("Navigator options")]
    [SerializeField] private float gridSize = 0.5f;
    [SerializeField] private float speed = 0.05f;
    public LayerMask obstacles;
    [SerializeField] private bool searchShortcut = false;
    [SerializeField] private bool snapToGrid = false;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float pathRecalculateDelay = 5f;

    [Header("Distance Keeping Options")]
    [SerializeField] private bool keepDistance = false;
    [SerializeField] private float distanceToKeep = 2f;
    [SerializeField] private float minMovementThreshold = 0.1f;

    private Pathfinder<Vector2> pathfinder;
    private List<Vector2> pathLeftToGo = new();

    private GameObject playerPrefab;
    private Transform playerTransform;
    private bool playerInRange = false;
    private Vector2 previousPosition;

    private float pathRecalculateTimer = 0f;

    private void Start()
    {
        pathfinder = new Pathfinder<Vector2>(GetDistance, GetNeighbourNodes, 1000);

        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerPrefab.GetComponent<Transform>();
        previousPosition = transform.position;
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (pathLeftToGo.Count > 0)
            {
                Vector3 dir = (Vector3)pathLeftToGo[0] - transform.position;
                transform.position += dir.normalized * speed;
                if (((Vector2)transform.position - pathLeftToGo[0]).sqrMagnitude < speed * speed)
                {
                    transform.position = pathLeftToGo[0];
                    pathLeftToGo.RemoveAt(0);
                }
            }
            else
            {
                // If no path is available, move directly towards the player
                Vector3 direction = playerTransform.position - transform.position;

                if (keepDistance && direction.magnitude < distanceToKeep)
                {
                    // Keep distance from the player
                    direction = -direction.normalized;
                }

                transform.position += direction.normalized * speed;
            }

            // Check if the enemy has moved a certain distance
            if (Vector2.Distance(transform.position, previousPosition) < minMovementThreshold)
            {
                // If the enemy hasn't moved, set the flag for path recalculation
                pathRecalculateTimer += Time.deltaTime;
                if (pathRecalculateTimer >= pathRecalculateDelay)
                {
                    pathRecalculateTimer = 0f;
                    GetMoveCommand(playerTransform.position);
                }
            }

            previousPosition = transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (!playerInRange)
        {
            // Check if the player is within detection range
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance <= detectionRange)
            {
                playerInRange = true;
                GetMoveCommand(playerTransform.position);
            }
        }
    }

    private void GetMoveCommand(Vector2 target)
    {
        Vector2 closestNode = GetClosestNode(transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out List<Vector2> path))
        {
            if (searchShortcut && path.Count > 0)
                pathLeftToGo = ShortenPath(path);
            else
            {
                pathLeftToGo = new List<Vector2>(path);
                if (!snapToGrid) pathLeftToGo.Add(target);
            }
        }
    }

    private Vector2 GetClosestNode(Vector2 target)
    {
        return new Vector2(Mathf.Round(target.x / gridSize) * gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    private float GetDistance(Vector2 A, Vector2 B)
    {
        return (A - B).sqrMagnitude;
    }

    private Dictionary<Vector2, float> GetNeighbourNodes(Vector2 pos)
    {
        Dictionary<Vector2, float> neighbours = new();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2 dir = new Vector2(i, j) * gridSize;
                if (!Physics2D.Linecast(pos, pos + dir, obstacles))
                {
                    neighbours.Add(GetClosestNode(pos + dir), dir.magnitude);
                }
            }
        }
        return neighbours;
    }

    private List<Vector2> ShortenPath(List<Vector2> path)
    {
        List<Vector2> newPath = new();
        for (int i = 0; i < path.Count; i++)
        {
            newPath.Add(path[i]);
            for (int j = path.Count - 1; j > i; j--)
            {
                if (!Physics2D.Linecast(path[i], path[j], obstacles))
                {
                    i = j;
                    break;
                }
            }
            newPath.Add(path[i]);
        }
        newPath.Add(path[^1]);
        return newPath;
    }
}
