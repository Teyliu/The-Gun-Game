using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueArea : MonoBehaviour
{
    public float attackRadius = 5f;      // Radio del ataque en área
    public int damageAmount = 10;        // Cantidad de daño infligido al jugador
    public float attackCooldown = 2f;    // Tiempo de espera entre ataques en segundos

    private GameObject player;           // Referencia al objeto del jugador
    private bool canAttack = true;       // Flag para controlar si el enemigo puede atacar
    private int attackCount = 0;         // Recuento de ataques realizados

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   // Buscar el objeto del jugador por etiqueta al inicio
    }

    private void Update()
    {
        if (canAttack && player != null && attackCount < 4)
        {
            // Calcular la distancia entre el enemigo y el jugador
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance <= attackRadius)
            {
                // Realizar el ataque al jugador
                AttackPlayer();
            }
        }
    }

    private void AttackPlayer()
    {
        canAttack = false;

        // Reducir la vida del jugador
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }

        // Incrementar el recuento de ataques
        attackCount++;

        // Esperar el tiempo de espera entre ataques
        Invoke("EnableAttack", attackCooldown);
    }

    private void EnableAttack()
    {
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reducir la vida del jugador al colisionar
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar una esfera en el editor para representar el radio del ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
