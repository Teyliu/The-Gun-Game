using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamaraChange : MonoBehaviour
{
    public string targetTag = "Player";
    public CinemachineVirtualCamera originalCamera;
    public CinemachineVirtualCamera newCamera;

    private bool isInNewCameraZone = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Debug.Log("Cambio a Camara 2");
            isInNewCameraZone = true;
            SwitchCamera(newCamera);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Debug.Log("Cambio a Camara 1");
            isInNewCameraZone = false;
            SwitchCamera(originalCamera);
        }
    }

    private void SwitchCamera(CinemachineVirtualCamera targetCamera)
    {
        originalCamera.gameObject.SetActive(false);
        newCamera.gameObject.SetActive(false);
        targetCamera.gameObject.SetActive(true);
    }
}
