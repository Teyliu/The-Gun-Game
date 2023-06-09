using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : MonoBehaviour
{
    public GameObject bulletPrefab;         // Prefab de la bala
    public Transform firePoint;             // Punto de origen de las balas
    public float fireRate = 0.5f;           // Tasa de disparo en segundos
    public float rotationSpeed = 180f;      // Velocidad de rotación en grados por segundo

    private float nextFireTime;             // Tiempo en el que se realizará el próximo disparo
    private bool isIncreasingRotation;       // Indica si la rotación está aumentando
    private float rotationChangeDuration = 3f; // Duración del cambio de velocidad en segundos
    private float rotationStopDuration = 5f; // Duración de la pausa en segundos
    private float targetRotationSpeed = 720f; // Velocidad de rotación objetivo
    private float rotationTimer;             // Temporizador para el cambio de velocidad
    private int executionCount;              // Cantidad de veces que se ha ejecutado el ciclo completo

    private void Start()
    {
        nextFireTime = Time.time;           // Inicializar el próximo tiempo de disparo
        isIncreasingRotation = true;        // Comenzar aumentando la velocidad de rotación
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
            nextFireTime = Time.time + fireRate;   // Actualizar el próximo tiempo de disparo
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
        // Aplicar velocidad a la bala en la dirección hacia adelante del enemigo
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
                    // Detener el script aquí
                    enabled = false;
                }
            }
        }
    }
}
