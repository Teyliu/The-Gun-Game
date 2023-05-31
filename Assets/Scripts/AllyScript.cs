using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyScript : MonoBehaviour
{
    public float followDistance = 1f;
    public float movementSpeed = 2f;
    public int allyIndex = 0;

    private bool isFollowing = false;
    private bool isOrdered = false;
    private List<Vector3> trailPoints;
    private int currentPointIndex;
    private SpriteRenderer spriteRenderer;
    private Transform target;
    private AllyScript previousAlly;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentPointIndex = allyIndex;
    }

    private void Update()
    {
        if (isFollowing && trailPoints != null && trailPoints.Count > 0)
        {
            Vector3 targetPosition = trailPoints[currentPointIndex];
            Vector3 direction = targetPosition - transform.position;

            if (isOrdered)
            {
                float orderOffset = followDistance * allyIndex;
                int targetIndex = Mathf.Clamp(currentPointIndex - (int)orderOffset, 0, trailPoints.Count - 1);
                targetPosition = trailPoints[targetIndex];
                direction = targetPosition - transform.position;
            }

            if (direction.magnitude > followDistance)
            {
                Vector3 desiredPosition = targetPosition - direction.normalized * followDistance;

                // Add a random offset to the desired position
                float offsetX = Random.Range(-followDistance, followDistance);
                float offsetY = Random.Range(-followDistance, followDistance);
                desiredPosition += new Vector3(offsetX, offsetY, 0f);

                Vector3 moveDirection = desiredPosition - transform.position;
                float moveDistance = movementSpeed * Time.deltaTime;
                if (moveDistance > moveDirection.magnitude)
                {
                    moveDistance = moveDirection.magnitude;
                }
                transform.Translate(moveDirection.normalized * moveDistance, Space.World);
            }

            spriteRenderer.flipX = (direction.x < 0);
        }
    }

    public void StartFollowingTrail(List<Vector3> points)
    {
        trailPoints = points;
        isFollowing = true;

        currentPointIndex = Mathf.Clamp(allyIndex, 0, trailPoints.Count - 1);

        if (previousAlly != null)
        {
            SetTarget(previousAlly.transform);
        }
    }

    public void SetOrderIndex(int index)
    {
        allyIndex = index;
    }

    public void SetOrdered(bool ordered)
    {
        isOrdered = ordered;
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    public void SetPreviousAlly(AllyScript previousAllyScript)
    {
        previousAlly = previousAllyScript;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerScript.Instance.AddAlly();

            AllyScript previousAllyScript = collision.GetComponent<AllyScript>();
            if (previousAllyScript != null)
            {
                SetPreviousAlly(previousAllyScript);
                previousAllyScript.SetOrdered(true);
                previousAllyScript.SetOrderIndex(allyIndex - 1);
                previousAllyScript.SetTarget(transform);
            }
        }
        else if (collision.CompareTag("Ally"))
        {
            AllyScript otherAlly = collision.GetComponent<AllyScript>();
            if (otherAlly != null && otherAlly.isFollowing && otherAlly.isOrdered)
            {
                SetOrderIndex(otherAlly.allyIndex + 1);
                SetTarget(otherAlly.transform);
                SetOrdered(true);
            }
        }
    }
}