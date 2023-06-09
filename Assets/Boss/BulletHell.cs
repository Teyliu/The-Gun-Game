using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : MonoBehaviour
{
    public GameObject bulletPrefab;         // Prefab de la bala
    public Transform firePoint;             // Punto de origen de las balas
    public float fireRate = 0.5f;           // Tasa de disparo en segundos
    public float rotationSpeed = 180f;      // Velocidad de rotaci�n en grados por segundo

    private float nextFireTime;             // Tiempo en el que se realizar� el pr�ximo disparo
    private bool isIncreasingRotation;       // Indica si la rotaci�n est� aumentando
    private float rotationChangeDuration = 3f; // Duraci�n del cambio de velocidad en segundos
    private float rotationStopDuration = 5f; // Duraci�n de la pausa en segundos
    private float targetRotationSpeed = 720f; // Velocidad de rotaci�n objetivo
    private float rotationTimer;             // Temporizador para el cambio de velocidad
    private int executionCount;              // Cantidad de veces que se ha ejecutado el ciclo completo

    private void Start()
    {
        nextFireTime = Time.time;           // Inicializar el pr�ximo tiempo de disparo
        isIncreasingRotation = true;        // Comenzar aumentando la velocidad de rotaci�n
        rotationTimer = rotationChangeDuration;
        executionCount = 0;
    }

    private void Update()
    {
        if (executionCount >= 2)
            return;

        RotateEnemy();                       // Girar el enemigo continuamente

        if (Time.time >= nextFireTime)
        {
            FireBullet();                   // Disparar una bala
            nextFireTime = Time.time + fireRate;   // Actualizar el pr�ximo tiempo de disparo
        }

        UpdateRotationSpeed();
    }

    private void RotateEnemy()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);   // Rotar el enemigo alrededor del eje Z
    }

    private void FireBullet()
    {
        // Crear una instancia de la bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Obtener el componente Rigidbody2D de la bala
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        // Aplicar velocidad a la bala en la direcci�n hacia adelante del enemigo
        bulletRigidbody.velocity = transform.up;
    }

    private void UpdateRotationSpeed()
    {
        if (isIncreasingRotation)
        {
            rotationTimer -= Time.deltaTime;

            if (rotationTimer <= 0f)
            {
                rotationSpeed = targetRotationSpeed;
                rotationTimer = rotationStopDuration;
                isIncreasingRotation = false;
            }
            else
            {
                rotationSpeed = Mathf.Lerp(rotationSpeed, targetRotationSpeed, 1f - (rotationTimer / rotationChangeDuration));
            }
        }
        else
        {
            rotationTimer -= Time.deltaTime;

            if (rotationTimer <= 0f)
            {
                rotationSpeed = 180f;
                rotationTimer = rotationChangeDuration;
                isIncreasingRotation = true;
                nextFireTime = Time.time + rotationStopDuration;
                executionCount++;

                if (executionCount >= 5)
                {
                    // Detener el script aqu�
                    enabled = false;
                }
            }
        }
    }
}
