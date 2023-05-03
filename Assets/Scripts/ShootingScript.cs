using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float baseFireRate = 0.5f;
    private float currentFireRate;
    private float nextFireTime;

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
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    public void IncreaseFireRate(float amount)
    {
        if (amount > 0)
        {
            currentFireRate = baseFireRate / (1 + amount);
        }
        else
        {
            currentFireRate = baseFireRate * (1 - amount);
        }
    }
}