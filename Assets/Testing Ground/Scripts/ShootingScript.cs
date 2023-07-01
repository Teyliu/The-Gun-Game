using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shotgunbulletPrefab;
    public float bulletSpeed = 10f;
    public float baseFireRate = 0.5f;
    private float currentFireRate;
    private float nextFireTime;
    public int numberOfBullets = 5;
    public ChangeWeapon changeWeapon;

    private void Start()
    {
        currentFireRate = baseFireRate;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + currentFireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (changeWeapon.hadShotgun == true)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//la direccion del maouse para disparar
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;//la posicion del maouse hacia donde el jugador esta disparando


            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Calcula un ángulo único para cada bala
                float bulletAngleOffset = 5f; // Separación angular entre las balas
                float currentAngle = angle + (i - (numberOfBullets - 1) / 2) * bulletAngleOffset; //Calcula un ángulo único para cada bala en función del ángulo base y el desplazamiento angular.

                // Calcula la dirección correspondiente al ángulo de la bala actual
                Vector2 currentDirection = new(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));  // Calcula la dirección correspondiente al ángulo de la bala actual utilizando las funciones trigonométricas Mathf.Cos y Mathf.Sin.

                GameObject bullet = Instantiate(shotgunbulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, currentAngle)));
                bullet.GetComponent<Rigidbody2D>().velocity = currentDirection * bulletSpeed;
            }
        }
        else
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }

    public void IncreaseFireRate(float modifier, float duration)
    {
        if (modifier > 0)
        {
            currentFireRate = baseFireRate / (1 + modifier);
        }
        else
        {
            currentFireRate = baseFireRate * (1 - modifier);
        }

        StartCoroutine(ResetFireRateAfterDelay(duration));
    }

    private IEnumerator ResetFireRateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentFireRate = baseFireRate;
    }
}