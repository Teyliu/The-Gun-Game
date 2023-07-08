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

    public int shotgunAmmo = 0; // New variable to track shotgun ammo count

    private void Start()
    {
        currentFireRate = baseFireRate;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + currentFireRate;

            if (changeWeapon.hadShotgun == true && shotgunAmmo > 0) // Check if shotgun is equipped and has ammo
            {
                ShootShotgun();
            }
            else if (changeWeapon.hadPistol == true) // Check if pistol is equipped
            {
                ShootNormal();
            }
        }
    }


    private void ShootShotgun()
    {
        // Reduce shotgun ammo count
        shotgunAmmo--;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            float bulletAngleOffset = 5f;
            float currentAngle = angle + (i - (numberOfBullets - 1) / 2) * bulletAngleOffset;

            Vector2 currentDirection = new(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(shotgunbulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, currentAngle)));
            bullet.GetComponent<Rigidbody2D>().velocity = currentDirection * bulletSpeed;
        }
    }

    private void ShootNormal()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
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

    public void AddShotgunAmmo(int amount)
    {
        shotgunAmmo += amount;
    }
}
