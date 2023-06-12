using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamaraChange : MonoBehaviour
{
    public CinemachineVirtualCamera targetCamera;
    private CinemachineVirtualCamera originalCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            originalCamera = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as CinemachineVirtualCamera;
            targetCamera.gameObject.SetActive(true);
            originalCamera.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetCamera.gameObject.SetActive(false);
            originalCamera.gameObject.SetActive(true);
        }
    }
}
