using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDesk : MonoBehaviour
{
    public Animator animator;

    private bool playerInRange;
    private bool open;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("jugador en rango");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("jugador fuera de rango");
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Open");
        }
    }
}


