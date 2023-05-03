using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Transform cameraTransform;

    private void Update()
    {
        transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z);
    }
}