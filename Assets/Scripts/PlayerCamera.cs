using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothTime = 0.3f;
    public float zOffset = -10f;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPosition = playerTransform.position;
        targetPosition.z = zOffset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}