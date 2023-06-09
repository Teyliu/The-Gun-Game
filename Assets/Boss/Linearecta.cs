using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linearecta : MonoBehaviour
{
    public string playerTag = "Player";        // Etiqueta del objeto del jugador
    public float detectionDelay = 0.5f;        // Retraso antes de detectar al jugador en segundos
    public float chaseDuration = 2f;           // Duración de la persecución en segundos
    public float stopDuration = 4f;            // Duración de la pausa entre persecuciones en segundos
    public float movementSpeed = 5f;           // Velocidad de movimiento del enemigo
    public int damageAmount = 10;              // Cantidad de daño infligido al jugador

    private Transform player;                  // Transform del jugador
    private bool isChasing = false;            // Flag para controlar si el enemigo está persiguiendo al jugador
    private bool canChase = true;              // Flag para controlar si el enemigo puede iniciar una persecución
    private int chaseCount = 0;                 // Recuento de persecuciones realizadas

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;   // Buscar el objeto del jugador por etiqueta
        if (player == null)
        {
            Debug.LogError("No se encontró un objeto con la etiqueta " + playerTag);
        }
        else
        {
            InvokeRepeating(nameof(ChasePlayer), detectionDelay, stopDuration + chaseDuration);
        }
    }

    private void Update()
    {
        if (isChasing && player != null)
        {
            // Moverse hacia la posición del jugador
            Vector3 direction = player.position - transform.position;
            transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);
        }
    }

    private void ChasePlayer()
    {
        if (canChase && player != null && chaseCount < 4)
        {
            // Iniciar la persecución después del retraso de detección
            Invoke("StartChase", detectionDelay);
        }
    }

    private void StartChase()
    {
        isChasing = true;
        canChase = false;

        // Detener la persecución después de la duración de persecución
        Invoke("StopChase", chaseDuration);
    }

    private void StopChase()
    {
        isChasing = false;

        // Permitir una nueva persecución después de la duración de pausa
        Invoke("EnableChase", stopDuration);

        // Incrementar el recuento de persecuciones
        chaseCount++;
    }

    private void EnableChase()
    {
        canChase = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
